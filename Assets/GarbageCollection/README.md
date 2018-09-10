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

Mark And Sweep

가장 오래됐으며, 가장 구현이 간단한 GC알고리즘이다.

방식은 이름따라 메모리를 Mark(표시)하고 쓸모 없어졌을 때 Sweep(쓸어담기)하는 식으로 메모리를 해제한다.

주로 보통 1Bit 크기의 플래그를 사용하여 처리를 한다.



장점

- 구현이 간편하다.



단점

- 느리다. (해제할 대상이 많아지면 순회, 해제의 시간이 오래걸린다.)

- 메모리 단편화에 대한 대책이 없다.



메모리 단편화는 Mark Compact알고리즘으로 해결이 됐긴 했지만, 여전히 똥이다.



Copying GC

메모리 파편화 현상을 두개의 메모리 공간을 사용해서 해결한 방법이다.

메모리를 크게 이등분 하여 나눈다. 편의상 A공간, B공간이 있다고 가정하겠다.

A공간에 할당을 하다가 GC를 한번 돌릴 때, 쓸모있는 메모리들만 B공간에 복사를 한다.

그 후, A공간에 남아있는 메모리들을 전부 해제한다.



장점

- 메모리 파편화가 없다.



단점

- 느리다 (아무리 이등분된 메모리 영역이라 하더라도, 메모리가 크기가 크고 많은 메모리가 있다면 순회, 해제 하는데 오래걸린다.)

- 메모리 공간을 이등분하기 때문에 메모리 공간이 많이 부족해진다.



이 그림을 보면 이해가 쉽게 될 것이다.



Generational GC

우리가 오늘 알아볼 SGen (Simple Generational GC)도 여기에 속한다.

세대별 GC는 그 종류가 매우 다양하므로(세대를 어떻게 구현하는가에 따라 종류가 매우 많이 나뉜다.) 우리는 SGen만 알아보도록 하겠다.



SGen은 단 2개의 세대만 존재한다. Nursery와 Major Heap이다.

객체 할당은 Nursery에서만 일어나며 일정횟수의 GC에서 살아남은 메모리들을 Major Heap으로 승진시킨다.

2개의 세대 외에 매우 큰 메모리(기본적으로는 8000byte)들을 위해 Large Object Space라는 공간이 또 따로 있다.



하지만 여기서 몇몇 사람은 아래와 의문이 들것이다.

"만약 Major Heap의 메모리가 Nursery의 메모리를 참조하고 있다면?"

위와 같은 상황이 온다면 Nursery의 그 객체를 해제한다. 참조중인데도 말이다.

그 이유는 다른 논리적 공간에서 한 참조를 GC가 체크할 수 없기 때문이다.



이러한 이유 때문에 Generational GC는 Write Barrier라는게 존재한다. (SGen만의 특징이 아니다.)

Write Barrier인데 Major Heap -> Nursery를 참조하고 있다면 Write Barrier에 Nursery에 있는 그 객체를 미리 적어두고 GC시에 Wrtie Barrier에 있는 메모리들은 해제 대상에서 제외한다.



장점

- 비교적 빠르다. (Nursery라는 작은 공간에서만 GC가 빈번하게 일어나기 때문)

- 파편화가 적다. (없지는 않다.)



단점

- 파편화가 존재한다.

- 공간중 한 공간만 꽉차도 터진다. (근데 그럴일은 거의 없다.)
