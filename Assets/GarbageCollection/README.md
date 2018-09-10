# GarbageCollection

- [Unity 자동 메모리 관리 이해](https://docs.unity3d.com/kr/current/Manual/UnderstandingAutomaticMemoryManagement.html)
- [Unity 최적화에 대한 이해](https://docs.unity3d.com/kr/current/Manual/BestPracticeUnderstandingPerformanceInUnity.html)

- [유니티 가비지 컬레션 최적화하기 - 1 번역](http://ronniej.sfuh.tk/optimizing-garbage-collection-in-unity-games-1/)
- [유니티 가비지 컬레션 최적화하기 - 2 번역](http://ronniej.sfuh.tk/optimizing-garbage-collection-in-unity-games-2/)
- [유니티 가비지 컬레션 최적화하기 - 3 번역](http://ronniej.sfuh.tk/optimizing-garbage-collection-in-unity-games-3/)
- [유니티 가비지 컬레션 최적화하기 - 4 번역](http://ronniej.sfuh.tk/optimizing-garbage-collection-in-unity-games-4/)
- [유니티 가비지 컬레션 최적화하기 - 5 번역](http://ronniej.sfuh.tk/optimizing-garbage-collection-in-unity-games-5/)
- [유니티 가비지 컬레션 최적화하기 - 6 번역](http://ronniej.sfuh.tk/optimizing-garbage-collection-in-unity-games-6/)

- [Garbage Collector In Unity](https://hrmrzizon.github.io/2017/04/23/garbage-collector-in-unity/)

Unity가 돌아가는 것은 MOno기반의 가상머신에서 돌아가는 것이다.이렇게 실제 runtime 상에서 돌아가는 가상머신을 Mono-runtime 이라 칭하는데 Mono 는 C# 을 주로 타게팅하고 만들어진 프레임워크이기 때문에 Mono-runtime 은 GC 를 탑재해야 했다. Mono 2.8 이하 버젼에서는 Boehm-Demers-Weiser(이하 Boehm) 라는 이름의 GC 알고리즘을 택했었는데, 이는 1988년에 처음 릴리즈되었고 (license) C/C++ 를 타겟으로 만들어진 GC 라이브러리로써(Github) 당시 쓸만한 GC 였던 것 같다. SGen Introduction 에서는 안정성과 이식성이 좋아 쓰였다고 한다. 하지만 Boehm GC 는 C/C++ 을 타겟으로 구현되었다. 그래서 여러 문제와 한계가 있어 Mono-runtime 은 다른 대안이 필요했다. 결국 Mono 에서는 직접 GC 를 개발했다. 주로 칭하는 이름은 SGen 길게 풀면 Simple Generational 이다. Mono 2.8 버젼부터는 SGen 으로 GC 를 통채로 바꾸었다.

하지만 지금 Unity 에서 쓰는 Mono 의 버젼은 2.8 을 넘지 못한다. 또한 직접 파일을 확인해 Mono 의 정보를 보면 아래와 같이 command line 에서 확인할 수 있다.

```
D:\2018.2.6f1\Editor\Data\Mono\bin>mono.exe --version
Mono JIT compiler version 2.0 (Visual Studio built mono)
Copyright (C) 2002-2010 Novell, Inc and Contributors. www.mono-project.com
        TLS:           normal
        GC:            Included Boehm (with typed GC)
        SIGSEGV:       normal
        Notification:  Thread + polling
        Architecture:  x86
        Disabled:      none
```
보다시피 GC 항목에는 Include Boehm 이라고 쓰여 있다. 

현재 Unity 에서 쓰이는 Mono-runtime 에 대해서 알아보았다. 아래에서는 언제가 될지 모르는 Mono 프레임워크 업데이트에 대비해 SGen 의 간단한 동작방식과 쓰이는 여러 알고리즘에 대해서 알아볼것이다.

# SGen 에서 쓰이는 GC 알고리즘
SGen 에서는 전통적으로 많이 쓰이는 여러 알고리즘을 사용한다. 대부분 대중적으로 많이 알려진 알고리즘을 채용해 알아두면 꽤 많은 도움이 될것이다.

## mark-and-sweep GC
mark-and-sweep 은 GC 알고리즘 중에서도 시초가 되는 알고리즘이며, 가장 간단한 GC 방법이다. 이름만 살펴보면 표시하고(mark) 쓸어담기(sweep) 로 알 수 있는데 조금 더 풀어보면, 메모리가 부족하거나 안쓰는 메모리를 없에야 할 때 사용하는 메모리를 표시하고(mark) 표시가 해제된 메모리 영역을 쓸어담아(sweep) 청소하는 방식이라 할 수 있다. 그림으로 표현하자면
![첫번째이미지](https://hrmrzizon.github.io/images/mark_and_sweep_0.png)