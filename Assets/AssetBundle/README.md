# Asset Bundle

[번역] 에셋번들과 리소스에 대한 가이드(http://code-kooc.tistory.com/entry/%EB%B2%88%EC%97%AD-%EC%97%90%EC%85%8B%EB%B2%88%EB%93%A4%EA%B3%BC-%EB%A6%AC%EC%86%8C%EC%8A%A4%EC%97%90-%EB%8C%80%ED%95%9C-%EA%B0%80%EC%9D%B4%EB%93%9C?category=717775)

에셋번들이 2019버전때 Addressable Asset System 요걸로 대체된다.

https://docs.unity3d.com/Packages/com.unity.addressables@0.2/manual/AddressableAssetsGettingStarted.html

https://youtu.be/u3K86nnzwc4

현재사용하려면 패키지 매니페스트에 아래것을 추가해야한다.

{
    "dependencies": {
        "com.unity.addressables": "0.1.2-preview"
    }
}

