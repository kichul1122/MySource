# UniRx

https://github.com/neuecc/UniRx

http://reactivex.io/

https://qiita.com/toRisouP/items/2f1643e344c741dd94f8

https://blog.naver.com/fkgustn1/221063822765

https://blog.naver.com/fkgustn1/221001131883

//Rx - Reactive Extensions for Unity, LINQ to Events
출발은 옵저버패턴에서부터 시작 c#에선 event를 예로 들 수 있다.
옵저버패턴의 단점은 이벤트의 호출해주는 과정에서의 스트림 처리가없이 바로 동작한다는 것(이벤터를보내기전, 이벤트를받은 후에 어떻게 할것인가를 작성)
Rx는 이런 상황에서 오퍼레이트를 통해 스트림을 정제해서 이벤트를 받게 할수있다.(이벤트를 받기 전에 무엇을 하고 싶다를 작성)
[스트림을 가공해서 자신이 받고 싶은 이벤트만 통지 받으면 좋잖아!!!]
Rx는 
1.스트림을 준비해서
2.스트림을 오퍼레이터로 가공 해서
3.최후에 Subscribe한다


스트림
- 이벤트가 흐르는 파이프 같은 이미지
- 타임라인에 배열되어 있는 이벤트의 시퀀스
- 분기되거나 합쳐지는게 가능
- 코드에서는 IObservable<T>로 취급된다. LINQ에서 IEnumerable<T>에 해당
OnNext, OnErro, OnCompleted
- 스트림은 Subscribe된 순간에 생성 된다.

UniRx를 사용하면 시간의 취급이 굉장히 간단해진다.
- 이벤트의 기다림 : 마우스 클릭이나 버튼의 입력 타이밍에 무언가를 처리 한다.
- 비동기처리 : 다른 스레드에서 통신을 하거나, 데이터를 로드할 때
- 시간 측정이 판정에 필요한 처리 : 홀드, 더블클릭판정
- 시간 변화하는 값의 감시 : False -> True가 되는 순간에 1회만 처리하고 싶을 때

IObserver - 이벤트 메시지를 게시 할 수 있는
IObservable - 이벤트 메시지를 구독 할 수 있는

오퍼레이터 - 스트림을 조작하는 메소드
Select, Where, Skip, SkipUntil, SkipWhile, Take, TakeUntil, TakeWhile, Throttle, Zip, Merge, 
CombineLatest, Distinct, DistinctUntilChanged, Delay, DelayFrame, First, FirstOfDefault, Last, 
LastOfDefault, StartWith, Concat, Buffer, Cast, Catch, CatchIgnore, ObserveOn, Do, Sample, Scan, 
Single, SingleOrDefault, Retry, Repeat, Time, TimeStamp, TimeInterval…

Where - 조건을 만족하는 메시지만 통과 시키는 오퍼레이터로
Select - 요소의 값을 변경한다.
SelectMany - 새로운 스트림을 생성하고, 그 스트림이 흐르는 메시지를 본래의 스트림의 메시지로 취급
Throttle/ThrottleFrame - 메시지가 집중해서 들어 올때에 마지막 이외를 무시 한다.
ThrottleFirst/ThrottleFirstFrame - 최초에 메시지가 올때부터 일정 시간 무시 힌다.
Dealy/DealyFrame - 메시지의 전달을 연기한다.
DistinctUntilChanged - 메시지가 변화한 순간에만 통지힌다.(값에 변화가 있는 순간만 통과)
SkipUntil - 지정한 스트림에 메시지가 올때까지 메시지를 Skip한다.
TakeUntil - 지정한 스트림에 메시지가 오면, 자신의 스트림에 OnCompleted를 보내서 종료 한다.
Repeat - 스트림이 OnCompleted로 종료 될때에 다시 한번 Subscribe를 한다.
First - 스트림에 최초로 받은 메시지만 보낸다.OnNext, OnComplete를 보냄
Zip - 여러개의 스트림의 메시지가 완전히 모일때까지 기다림

//현재는 using UniRx.Trigger; this.UpdateAsObservable()로 호출

Zip + First + Repeat
예)버튼이 둘다 눌리면 Text에 표시힌다.
button1.OnClickAsObservable().Zip(button2.OnClickAsObservable(), (b1, b2) => "Clicked!")
.First().Repeat().SubScribeToText(text, x => test.test + x + "\n");

SkipUntil + TakeUntil + Repeat - 이벤트 A가 올때부터 이벤트 B가 올때까지 처리를 하고 싶을 때 사용.
예)드래그로 오브젝트를 회전 시키기
UpdateAsObservable().SkipUntil(OnMouseDownAsObservable()).Select(_ => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")))
.TakeUntil(OnMouseUpAsObservable()).Repeat().Subscribe(rotation => transform 회전 시키기)

예)더블 클릭
var clickStream = UpdateAsObservable().Where(_ => Inpu.GetMouseButtonDown(0));	//클릭 스트림을 정의
//Throttle(200ms) : 200ms간 이벤트가 발생하지 않을 때 통지
//Buffer : 이벤트를 모은다. 종료조건은 Throttle이벤트가 올 때 까지
clickStream.Buffer(clickStream.Throttle(TimeSpan.FromMilliseconds(200))).Where(x => x.Count >= 2)
.SubScribeToText(_test, x => string.Format("DoubleClick : {0}", x.Count));

예)지면 도착 순간을 감지하기
UpdateAsObservable().Select(_ => characterController.isGrounded).DistinctUntilChanged().Wehere(x => x)
.Subscribe(_ => particleSystem.Player());

//ObserveEventValueChanged - 매 프레임 값의 변화를 감시한다면 ObserveEventValueChanged의 쪽이 심플하다.
UnityEngine.Object.ObserveEventValueChanged(x => x.isGrounded).Where(x => x).Subscribe(_ => Debug.Log("OnGrounded!"));

예)isGrounded의 변화를 정돈하기
//ThrottleFrame(5) : 값이 5프레임간 오지 않는다면 최후의 값을 보냄
UpdateAsObservable().Select(_ => playerCharacterController.isGrounded)
.DistinctUntilChanged().ThrottleFrame(5).Subscribe(x => throttledIsGrounded = x);

Unity가 준비한 HTTP통신용 모듈
- 코루틴으로 사용할 필요가 있다.
- 사용 편의성이 좋지는 않다.

ObservableWWW 
- WWW를 Observable로써 취급할 수 있게 한것
- 코루틴을 사용하지 않아도 된다

ObservableWWW.Get("http://naver.com").Subscribe(result => Debug.Log(result));

예)WWW로 텍스쳐 읽어 오기
button.OnClickAsObservable()
.First() //버튼을 연타해도 통신은 1회만 하도록 First를 넣는다.
.SelectMany(ObservableWWW.GetWWW(resourceURL).Timeout(TimeSpan.FromSeconds(3))) //클릭 스트림을 ObservableWWW의 스트리므로 덮어쓴다.
.Select(www => Sprite.Create(www.texture, new Rect(0, 0, 400, 400), Vector2.zero))
.Subscribe(sprite =>
{
	_image.sprite = sprite;
	_button.interactable = false;
}, Debug.LogErr);

예)동시에 통신해서 모든 데이터가 모이면 처리를 진행한다.
var parallel = Observable.WhenAll(
	ObservableWWW.Get("http://google.com/"),
	ObservableWWW.Get("http://bing.com/"),
	ObservableWWW.Get("http://unity3d.com/"));
)
parallel.Subscribe(xs =>
{
	Debug.Log(xs[0].Substring(0, 100));	//google
	Debug.Log(xs[1].Substring(0, 100));	//bing
	Debug.Log(xs[2].Substring(0, 100));	//unity
});

예)앞의 통신 결과를 사용해서 다음 통신을 실행한다. - resourcepath.txt에 적혀있는 URL에 엑세스하는 코드
var resourcePathURL = "http://resourcepath.txt";
ObservableWWW.Get(resourcePathURL).SelectMany(resourceUrl => ObservableWWW.Get(resourceUrl))
.Subscribe(Debug.Log());

기존라이브러리를 스트림으로 변환(PhtonCloud 예제)
PhotonCloud의 콜백이 스트림으로 변환된다

콜백으로부터 스트림을 변경하는 메리트
코드가 명시적이된다.
다양한 오퍼레이터에 의한 유여한 제어가 가능해 지게 된다.

ReactiveProperty
- Subscribe가 가능한 변수
- Observale로서 Subscribe가 가능하다
- 값이 대입될 때 OnNext메시지가 날라간다


코드가 선언적이 되어서, 처리의 의도가 알기 쉽다.
복잡한 로직을 오퍼레이터의 조합으로 구현 가능하다.

Observable로 변경하는 3가지 방법
UpdateAsObservable - 유니티 기반
- 지정한 gameObject에 연동된 Observable가 만들어진디ㅏ.
- gameObject의 Destroy때에 OnCompleted가 발행된다.

Observable.EveryUpdate - 독립된 기반
- gameObject로부터 독립된 Observable이 만들어 진다.
- MnonoBehaviour에 관계 없는 곳에서도 사용 가능

ObserveEveryValueChanged - 독립된 기반
- Observable.EveryUpdate의 파생 버전
- 값의 변화를 매 프레임 감시하는 Observable이 생성된다.

독립된 기반에서 UnityEngine.Object를 다룰때 게임오브젝트가 파괴되면서 UnityEngine.Object가 Null이되면서 예외가 발생할수있음.
수명 관리의 간단한 방법
AddTo(this.gameObject);

컴포넌트를 스트림으로 연결하기
컴포넌트를 스트림으로 연결하는 것으로,
Observer패턴한 설계로 만들 수 있다
전체가 이벤트 기반이 되게 할 수 있다
더불어 Rx는 Observer패턴 그 자체

예)타이머의 카운트를 화면에 표시한다.
타이머측을 스트림으로 변경

TimerConponent.cs
public ReadOnlyReactiveProperty<int> CurrentTime
{
	get { return _timerReactiveProperty.ToReadOnlyReactiveProperty();}
}

private void Start()
{
	Observable.Timer(TimeSpan.FromSeconds(1)).Repeat()
	.Subscribe(_ => _timerReactiveProperty.Value--)
	.AddTo(gameObject);
}

TimerDisplayComponent.cs
void Start()
{
	_timerCompoenet.CurrentTime.SubscribeToText(_timerText);
}

스트림으로 연결하는 메리트
Observer패턴이 간단히 구현 가능하다
- 변화를 폴링(Polling)하는 구현이 사라진다
- 필요한 타이밍에 필요한 처리를 하는 방식으로 작성하는 것이 좋다.

기존의 이벤트 통지구조보다 간단
- C#의 Event준비단계가 귀찮아서 쓰고 싶지 않다
- Unity의 SendMessage는 쓰고 싶지 않다
- Rx라면 Observable를 준비하면 OK! 간단!

코루틴과 조합하기
코루틴 -> Observable : Observable.FromCoroutine
복잡한 스트림을 간단하게 작성 가능
- 팩토리 메소드나 오퍼레이터 체인 만으로는 작성할 수 없는 복잡한 로직의 스트림을 작성하는 것이 가능
연속적인 처리를 캡슐화 가능하다
- 복잡한 처리를 코루틴에 숨겨서 외부에서는 Observable로서 선언적으로 취급하는 것이 가능하다

private  void  Start ()
{
    // 플레이어가 생존 해있는 시간을 30 초 카운트 다운
    // 타이머의 현재 카운트 [초]이 통지되는
    Observable.FromCoroutine<int>(observer => CountDownCoroutine ( observer ,  30 ,  player ))
        .Subscribe ( count  =>  Debug.Log ( count ));
}

/// <summary>
/// 플레이어가 살아있는 동안에 만 카운트 다운 타이머
/// 플레이어가 죽은 경우 계산은 중지
/// </ summary>
IEnumerator  CountDownCoroutine ( IObserver < int >  observer, int  startTime, player  player )
{
    var  currentTime  =  startTime ;
    while  ( currentTime  >  0 )
    {
        if  ( player.IsAlive )
        {
            observer.OnNext ( currentTime);
            currentTime -= 1;
        }
        yield  return  new  WaitForSeconds ( 1 );
    }
    observer.OnCompleted ();
}


Observable -> 코루틴 : StartAsCoroutine

StartAsCoroutine은 다음과 같은 특징이 있습니다.
OnCompleted를 호출할 때까지 yield return null을 계속 반복한다.
StartAsCoroutine에 전달 된 함수는 OnCompleted를 호출할 때에 한 번만 실행되고 마지막 OnNext 값이 전달된다.
StartAsCoroutine을 잘 사용하면 비동기 처리를 포함하는 복잡한 처리를 Task의 await 처리와 같이 동기화 처리처럼 다룰 수 있습니다.

