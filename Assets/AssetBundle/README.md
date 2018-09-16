# Asset Bundle

[번역] 에셋번들과 리소스에 대한 가이드(http://code-kooc.tistory.com/entry/%EB%B2%88%EC%97%AD-%EC%97%90%EC%85%8B%EB%B2%88%EB%93%A4%EA%B3%BC-%EB%A6%AC%EC%86%8C%EC%8A%A4%EC%97%90-%EB%8C%80%ED%95%9C-%EA%B0%80%EC%9D%B4%EB%93%9C?category=717775)

에셋번들이 2019버전때 Addressable Asset System 요걸로 대체 될 수 있다.

https://docs.unity3d.com/Packages/com.unity.addressables@0.2/manual/AddressableAssetsGettingStarted.html

https://youtu.be/u3K86nnzwc4

현재사용하려면 패키지 매니페스트에 아래것을 추가해야한다.

{
    "dependencies": {
        "com.unity.addressables": "0.1.2-preview"
    }
}

- [애셋번들과 리소스에 대한 가이드 번역](http://ronniej.sfuh.tk/%EC%95%A0%EC%85%8B%EB%B2%88%EB%93%A4%EA%B3%BC-%EB%A6%AC%EC%86%8C%EC%8A%A4%EC%97%90-%EB%8C%80%ED%95%9C-%EA%B0%80%EC%9D%B4%EB%93%9C-%EB%B2%88%EC%97%AD/)
- [애셋, 오브젝트 그리고 직렬화 - 1 번역](http://ronniej.sfuh.tk/asset-object-and-serialization1/)
- [애셋, 오브젝트 그리고 직렬화 - 2 번역](http://ronniej.sfuh.tk/%EC%95%A0%EC%85%8B-%EC%98%A4%EB%B8%8C%EC%A0%9D%ED%8A%B8-%EA%B7%B8%EB%A6%AC%EA%B3%A0-%EC%A7%81%EB%A0%AC%ED%99%94-2-%EB%B2%88%EC%97%AD/)
- [유니티 Resources 폴더 번역](http://ronniej.sfuh.tk/%EC%9C%A0%EB%8B%88%ED%8B%B0-resources-%ED%8F%B4%EB%8D%94-%EB%B2%88%EC%97%AD/)
- [에셋번들 기초 - 1 번역](http://ronniej.sfuh.tk/%EC%95%A0%EC%85%8B-%EB%B2%88%EB%93%A4-%EA%B8%B0%EC%B4%88-assetbundle-fundamentals/)
- [애셋번들 기초 - 2 번역](http://ronniej.sfuh.tk/%EC%95%A0%EC%85%8B-%EB%B2%88%EB%93%A4-%EA%B8%B0%EC%B4%88-assetbundle-fundamentals-2-%EB%B2%88%EC%97%AD/)
- [애셋번들 기초 - 3 번역](http://ronniej.sfuh.tk/%EC%95%A0%EC%85%8B-%EB%B2%88%EB%93%A4-%EA%B8%B0%EC%B4%88-assetbundle-fundamentals-2-%EB%B2%88%EC%97%AD/)

- [애셋번들 사용 패턴 - 1 번역](http://ronniej.sfuh.tk/assetbundleusagepattern1/)
- [애셋번들 사용 패턴 - 2 번역](http://ronniej.sfuh.tk/assetbundleusagepattern2/)
- [애셋번들 사용 패턴 - 3 번역](http://ronniej.sfuh.tk/assetbundleusagepatterns3/)
- [애셋번들 사용 패턴 - 4 번역](http://ronniej.sfuh.tk/assetbundleusagepatterns4/)
- [애셋번들 사용 패턴 - 5 번역](http://ronniej.sfuh.tk/assetbundleusagepatterns5/)
- [애셋번들 사용 패턴 - 6 번역](http://ronniej.sfuh.tk/assetbundleusagepatterns6/)

- [에셋번들 실전 가이드 (AssetBundle Best Practices)](https://youtu.be/Lx61ZEKEvnQ)

===========================================
애셋번들 구조
Header, DataSegment

그룹화 방식
1)논리적 그룹화
 UI, 캐릭터, 환경 등 논리적으로 묶을 수 있는 요소
 Downloadable Content(DLC)에 가장 적합
 언제 어디서 사용될 지 정확하게 알고 이썽야 함

2)종류별 그룹화
 오디오 트랙 또는 국가별 언어 파일 등 같은 타입 별 그룹화

3)동시 사용하는 컨텐츠별 그룹화

4)추가팁
자주 변경되는 것과 변경되지 않는 것을 분리
모델과 연관된 텍스처, 애니메이션 등을 그룹화
여러 에셋번들이 참조하는 에셋은 공용 에셋번들로 이동
SD/HD 에셋처럼 절대 동시에 로드하지 않는 에셋들을 그룹화
번들내 50%이하의 에셋이 같이 로드 되지않는 경우 분리를 고려
적은 수의 애셋을 가지며 자주 로드하는 에셋번들을 통합을 고려
같은 오브젝트의 그룹이고 버전만 다른 경우 Variants를 고려

AssetBundle Variants
지정한 플랫폼에 알맞는 에셋번들을 쉽게 로딩
 HD에셋번들, SD에셋번들, 런티임 시 필요한 Variant를 지정
같은 플랫폼에 각각 다른 에셋번들을 로드
 하드웨어 스펙의 차이가 크거나 다른 화면 비율을 가지는 경우
제한사항
 Variant 별로 각각의 에셋이 파일로 존재해야 함


워크플로우
Building AssetBundles -> CDN ->UnityWebRequest <-> Caches[캐쉬클래스] 캐시에 있을경우 캐시 파일에서 가져옴

패치 워크플로우
BUilding AssetBundle -> 파일 리스트 & 버전정보, AssetBundles -> CDN -> 정보파일 다운로드 -> 기존 정보파일 비교 -> 변경여부(Yes) -> UnityWebRequest -> 캐쉬관리(삭제)

커스텀 다운로드[하드코어한 개발자만 하시길...]
HttpWebRequest 및 WebClient클래스
직접 제작한 플러그인 NSURLConnection, java.net.HttpURLConnection
Application.persistentDataPath


애셋번들 로딩
AssetBundle.LoadFromMemory(Async) - 사용하지마세요[에디어테서 쓰인다고함]
AssetBundle.LoadFromFile(Async)
AssetBundle.LoadFromStream(Async) - 안쓰셨으면 함(최적화 제약사항이 많음)
WWW.LoadFromCacheOrDownload(2017.1버전이상 내부에서 UnityWebRequest API를 사용함)

DownloadHandlerAssetBundle : 에셋번들을 위한 DownloadHandler 서브클래스
2018.1이상
UnityWebRequestAssetBundle.GetAssetBundle 사용

```
void Start()
{
    StartCoroutine(GetMyBundle());
}

IEnumerator GetMyBundle()
{
    using(UnityWebRequest uwr = new UnityWebRequestAssetBundle.GetAssetBundle("http://www.my.com/mybundle"))
    {
        yield return uwr.SendWebRequest();

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);

        ...
    }
}
```

애셋번들 Load / Unload
로드에 사용되는 메모리는 최소화 됨

Unload(true) => 번들사용으로 만들어지는 것들이 사라짐

Unload(false) => 사라지지 않음

AssetBundle.Unload()를 호출해서 에셋 번들의 리소스를 언로드할 수 있습니다. unloadAllLoadedObjects 파라미터에 true 을 전달하면 에셋 번들에서 내부적으로 보유한 오브젝트와 AssetBundle.LoadAsset()을 사용하여 에셋 번들에서 로드된 오브젝트가 모두 삭제되고 번들에 의해 사용된 메모리가 릴리스됩니다.

에셋 번들을 로드한 후 원하는 오브젝트를 인스턴스화하고 오브젝트를 유지하면서 번들이 사용하던 메모리를 해제하고 싶은 경우가 있습니다. 다른 에셋 번들을 위한 인스턴스 로드 등, 다른 작업을 위해 메모리를 비울 수 있다는 게 장점입니다. 이 경우 파라미터에 false 을 전달해야 합니다. 번들이 삭제된 후에는 해당 번들에서 오브젝트를 로드할 수 없게 됩니다.

다른 레벨을 로드하기 전에 Resources.Load()를 사용하여 로드한 씬의 오브젝트를 삭제하려면 Object.Destroy()를 호출해야 합니다. 에셋을 해제하려면 Resources.UnloadUnusedAssets()를 사용해야 합니다..


문제와 해결 방법

에셋 중복문제
에셋번들에 명시적으로 할당하지 않은 에셋의 경우
 이 에셋을 참조하는 모든 에셋번들에 에셋 복사본이 포함
 각 복사본은 각기 다른 에셋으로 처리되어 별도의 메모리를 차지함
 오브젝트 요소 중 하나라 할당되지 않은 에셋을 참조하는 경우 동일

해결방법?
 명시적으로 에셋번들 할당
 각 에셋번들 간 종속성을 없애기
 하나의 종속성을 공유하는 두 개의 에셋 번들이 동시에 로드되지 않도록 에셋번들을 분할
 연광성이 있는 에셋들을 하나의 에셋번들로 빌드

스프라이트 아틀라스 중복 문제
 자동생성된 스프라이트 아틀라스의 경우
  스프라이트가 포함된 에셋번들에 아틀라스도 할당
  2017.1 이상의 버전인 경우 SpriteAtlas에셋을 사용

안드로이드 텍스처 문제
