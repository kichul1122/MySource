# UniRx

https://github.com/neuecc/UniRx

http://reactivex.io/

https://qiita.com/toRisouP/items/2f1643e344c741dd94f8

https://blog.naver.com/fkgustn1/221063822765

https://blog.naver.com/fkgustn1/221001131883

//Rx - Reactive Extensions for Unity, LINQ to Events


����� ���������Ͽ������� ���� c#���� event�� ���� �� �� �ִ�.


������������ ������ �̺�Ʈ�� ȣ�����ִ� ���������� ��Ʈ�� ó�������� �ٷ� �����Ѵٴ� ��(�̺��͸���������, �̺�Ʈ������ �Ŀ� ��� �Ұ��ΰ��� �ۼ�)


Rx�� �̷� ��Ȳ���� ���۷���Ʈ�� ���� ��Ʈ���� �����ؼ� �̺�Ʈ�� �ް� �Ҽ��ִ�.(�̺�Ʈ�� �ޱ� ���� ������ �ϰ� �ʹٸ� �ۼ�)


[��Ʈ���� �����ؼ� �ڽ��� �ް� ���� �̺�Ʈ�� ���� ������ ���ݾ�!!!]


Rx�� 


1.��Ʈ���� �غ��ؼ�


2.��Ʈ���� ���۷����ͷ� ���� �ؼ�


3.���Ŀ� Subscribe�Ѵ�




��Ʈ��


- �̺�Ʈ�� �帣�� ������ ���� �̹���


- Ÿ�Ӷ��ο� �迭�Ǿ� �ִ� �̺�Ʈ�� ������


- �б�ǰų� �������°� ����


- �ڵ忡���� IObservable<T>�� ��޵ȴ�. LINQ���� IEnumerable<T>�� �ش�


OnNext, OnErro, OnCompleted


- ��Ʈ���� Subscribe�� ������ ���� �ȴ�.



UniRx�� ����ϸ� �ð��� ����� ������ ����������.


- �̺�Ʈ�� ��ٸ� : ���콺 Ŭ���̳� ��ư�� �Է� Ÿ�ֿ̹� ���𰡸� ó�� �Ѵ�.


- �񵿱�ó�� : �ٸ� �����忡�� ����� �ϰų�, �����͸� �ε��� ��


- �ð� ������ ������ �ʿ��� ó�� : Ȧ��, ����Ŭ������


- �ð� ��ȭ�ϴ� ���� ���� : False -> True�� �Ǵ� ������ 1ȸ�� ó���ϰ� ���� ��



IObserver - �̺�Ʈ �޽����� �Խ� �� �� �ִ�


IObservable - �̺�Ʈ �޽����� ���� �� �� �ִ�



���۷����� - ��Ʈ���� �����ϴ� �޼ҵ�


Select, Where, Skip, SkipUntil, SkipWhile, Take, TakeUntil, TakeWhile, Throttle, Zip, Merge, 


CombineLatest, Distinct, DistinctUntilChanged, Delay, DelayFrame, First, FirstOfDefault, Last, 


LastOfDefault, StartWith, Concat, Buffer, Cast, Catch, CatchIgnore, ObserveOn, Do, Sample, Scan, 


Single, SingleOrDefault, Retry, Repeat, Time, TimeStamp, TimeInterval��



Where - ������ �����ϴ� �޽����� ��� ��Ű�� ���۷����ͷ�


Select - ����� ���� �����Ѵ�.


SelectMany - ���ο� ��Ʈ���� �����ϰ�, �� ��Ʈ���� �帣�� �޽����� ������ ��Ʈ���� �޽����� ���


Throttle/ThrottleFrame - �޽����� �����ؼ� ��� �ö��� ������ �ܸ̿� ���� �Ѵ�.


ThrottleFirst/ThrottleFirstFrame - ���ʿ� �޽����� �ö����� ���� �ð� ���� ����.


Dealy/DealyFrame - �޽����� ������ �����Ѵ�.


DistinctUntilChanged - �޽����� ��ȭ�� �������� ��������.(���� ��ȭ�� �ִ� ������ ���)


SkipUntil - ������ ��Ʈ���� �޽����� �ö����� �޽����� Skip�Ѵ�.


TakeUntil - ������ ��Ʈ���� �޽����� ����, �ڽ��� ��Ʈ���� OnCompleted�� ������ ���� �Ѵ�.


Repeat - ��Ʈ���� OnCompleted�� ���� �ɶ��� �ٽ� �ѹ� Subscribe�� �Ѵ�.


First - ��Ʈ���� ���ʷ� ���� �޽����� ������.OnNext, OnComplete�� ����


Zip - �������� ��Ʈ���� �޽����� ������ ���϶����� ��ٸ�



//����� using UniRx.Trigger; this.UpdateAsObservable()�� ȣ��



Zip + First + Repeat


��)��ư�� �Ѵ� ������ Text�� ǥ������.


button1.OnClickAsObservable().Zip(button2.OnClickAsObservable(), (b1, b2) => "Clicked!")


.First().Repeat().SubScribeToText(text, x => test.test + x + "\n");



SkipUntil + TakeUntil + Repeat - �̺�Ʈ A�� �ö����� �̺�Ʈ B�� �ö����� ó���� �ϰ� ���� �� ���.


��)�巡�׷� ������Ʈ�� ȸ�� ��Ű��


UpdateAsObservable().SkipUntil(OnMouseDownAsObservable()).Select(_ => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")))


.TakeUntil(OnMouseUpAsObservable()).Repeat().Subscribe(rotation => transform ȸ�� ��Ű��)



��)���� Ŭ��


var clickStream = UpdateAsObservable().Where(_ => Inpu.GetMouseButtonDown(0));  //Ŭ�� ��Ʈ���� ����


//Throttle(200ms) : 200ms�� �̺�Ʈ�� �߻����� ���� �� ����


//Buffer : �̺�Ʈ�� ������. ���������� Throttle�̺�Ʈ�� �� �� ����


clickStream.Buffer(clickStream.Throttle(TimeSpan.FromMilliseconds(200))).Where(x => x.Count >= 2)


.SubScribeToText(_test, x => string.Format("DoubleClick : {0}", x.Count));



��)���� ���� ������ �����ϱ�


UpdateAsObservable().Select(_ => characterController.isGrounded).DistinctUntilChanged().Wehere(x => x)


.Subscribe(_ => particleSystem.Player());



//ObserveEventValueChanged - �� ������ ���� ��ȭ�� �����Ѵٸ� ObserveEventValueChanged�� ���� �����ϴ�.


UnityEngine.Object.ObserveEventValueChanged(x => x.isGrounded).Where(x => x).Subscribe(_ => Debug.Log("OnGrounded!"));



��)isGrounded�� ��ȭ�� �����ϱ�


//ThrottleFrame(5) : ���� 5�����Ӱ� ���� �ʴ´ٸ� ������ ���� ����


UpdateAsObservable().Select(_ => playerCharacterController.isGrounded)


.DistinctUntilChanged().ThrottleFrame(5).Subscribe(x => throttledIsGrounded = x);



Unity�� �غ��� HTTP��ſ� ���


- �ڷ�ƾ���� ����� �ʿ䰡 �ִ�.


- ��� ���Ǽ��� ������ �ʴ�.



ObservableWWW 


- WWW�� Observable�ν� ����� �� �ְ� �Ѱ�


- �ڷ�ƾ�� ������� �ʾƵ� �ȴ�



ObservableWWW.Get("http://naver.com").Subscribe(result => Debug.Log(result));



��)WWW�� �ؽ��� �о� ����


button.OnClickAsObservable()


.First() //��ư�� ��Ÿ�ص� ����� 1ȸ�� �ϵ��� First�� �ִ´�.


.SelectMany(ObservableWWW.GetWWW(resourceURL).Timeout(TimeSpan.FromSeconds(3))) //Ŭ�� ��Ʈ���� ObservableWWW�� ��Ʈ���Ƿ� �����.


.Select(www => Sprite.Create(www.texture, new Rect(0, 0, 400, 400), Vector2.zero))


.Subscribe(sprite =>


{


    _image.sprite = sprite;


    _button.interactable = false;


}, Debug.LogErr);



��)���ÿ� ����ؼ� ��� �����Ͱ� ���̸� ó���� �����Ѵ�.


var parallel = Observable.WhenAll(


    ObservableWWW.Get("http://google.com/"),


    ObservableWWW.Get("http://bing.com/"),


    ObservableWWW.Get("http://unity3d.com/"));


)


parallel.Subscribe(xs =>


{


    Debug.Log(xs[0].Substring(0, 100)); //google


    Debug.Log(xs[1].Substring(0, 100)); //bing


    Debug.Log(xs[2].Substring(0, 100)); //unity


});



��)���� ��� ����� ����ؼ� ���� ����� �����Ѵ�. - resourcepath.txt�� �����ִ� URL�� �������ϴ� �ڵ�


var resourcePathURL = "http://resourcepath.txt";


ObservableWWW.Get(resourcePathURL).SelectMany(resourceUrl => ObservableWWW.Get(resourceUrl))


.Subscribe(Debug.Log());



�������̺귯���� ��Ʈ������ ��ȯ(PhtonCloud ����)


PhotonCloud�� �ݹ��� ��Ʈ������ ��ȯ�ȴ�



�ݹ����κ��� ��Ʈ���� �����ϴ� �޸�Ʈ


�ڵ尡 ������̵ȴ�.


�پ��� ���۷����Ϳ� ���� ������ ��� ������ ���� �ȴ�.



ReactiveProperty


- Subscribe�� ������ ����


- Observale�μ� Subscribe�� �����ϴ�


- ���� ���Ե� �� OnNext�޽����� ���󰣴�




�ڵ尡 �������� �Ǿ, ó���� �ǵ��� �˱� ����.


������ ������ ���۷������� �������� ���� �����ϴ�.



Observable�� �����ϴ� 3���� ���


UpdateAsObservable - ����Ƽ ���


- ������ gameObject�� ������ Observable�� ���������.


- gameObject�� Destroy���� OnCompleted�� ����ȴ�.



Observable.EveryUpdate - ������ ���


- gameObject�κ��� ������ Observable�� ����� ����.


- MnonoBehaviour�� ���� ���� �������� ��� ����



ObserveEveryValueChanged - ������ ���


- Observable.EveryUpdate�� �Ļ� ����


- ���� ��ȭ�� �� ������ �����ϴ� Observable�� �����ȴ�.



������ ��ݿ��� UnityEngine.Object�� �ٷ궧 ���ӿ�����Ʈ�� �ı��Ǹ鼭 UnityEngine.Object�� Null�̵Ǹ鼭 ���ܰ� �߻��Ҽ�����.


���� ������ ������ ���


AddTo(this.gameObject);



������Ʈ�� ��Ʈ������ �����ϱ�


������Ʈ�� ��Ʈ������ �����ϴ� ������,


Observer������ ����� ���� �� �ִ�


��ü�� �̺�Ʈ ����� �ǰ� �� �� �ִ�


���Ҿ� Rx�� Observer���� �� ��ü



��)Ÿ�̸��� ī��Ʈ�� ȭ�鿡 ǥ���Ѵ�.


Ÿ�̸����� ��Ʈ������ ����



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



��Ʈ������ �����ϴ� �޸�Ʈ


Observer������ ������ ���� �����ϴ�


- ��ȭ�� ����(Polling)�ϴ� ������ �������


- �ʿ��� Ÿ�ֿ̹� �ʿ��� ó���� �ϴ� ������� �ۼ��ϴ� ���� ����.



������ �̺�Ʈ ������������ ����


- C#�� Event�غ�ܰ谡 �����Ƽ� ���� ���� �ʴ�


- Unity�� SendMessage�� ���� ���� �ʴ�


- Rx��� Observable�� �غ��ϸ� OK! ����!



�ڷ�ƾ�� �����ϱ�


�ڷ�ƾ -> Observable : Observable.FromCoroutine


������ ��Ʈ���� �����ϰ� �ۼ� ����


- ���丮 �޼ҵ峪 ���۷����� ü�� �����δ� �ۼ��� �� ���� ������ ������ ��Ʈ���� �ۼ��ϴ� ���� ����


�������� ó���� ĸ��ȭ �����ϴ�


- ������ ó���� �ڷ�ƾ�� ���ܼ� �ܺο����� Observable�μ� ���������� ����ϴ� ���� �����ϴ�



private  void  Start ()


{


    // �÷��̾ ���� ���ִ� �ð��� 30 �� ī��Ʈ �ٿ�


    // Ÿ�̸��� ���� ī��Ʈ [��]�� �����Ǵ�


    Observable.FromCoroutine<int>(observer => CountDownCoroutine ( observer ,  30 ,  player ))


        .Subscribe ( count  =>  Debug.Log ( count ));


}



/// <summary>


/// �÷��̾ ����ִ� ���ȿ� �� ī��Ʈ �ٿ� Ÿ�̸�


/// �÷��̾ ���� ��� ����� ����


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




Observable -> �ڷ�ƾ : StartAsCoroutine



StartAsCoroutine�� ������ ���� Ư¡�� �ֽ��ϴ�.


OnCompleted�� ȣ���� ������ yield return null�� ��� �ݺ��Ѵ�.


StartAsCoroutine�� ���� �� �Լ��� OnCompleted�� ȣ���� ���� �� ���� ����ǰ� ������ OnNext ���� ���޵ȴ�.


StartAsCoroutine�� �� ����ϸ� �񵿱� ó���� �����ϴ� ������ ó���� Task�� await ó���� ���� ����ȭ ó��ó�� �ٷ� �� �ֽ��ϴ�.