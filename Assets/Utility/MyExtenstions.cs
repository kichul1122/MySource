using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UniRx;
using System.Linq;
using System.Threading;
using static UnityEngine.Application;
using UnityEngine.Events;

public static class MyExtenstions
{
	
}

public static class UIBillboardExtension
{
	//public static Transform CreateHpBar(this TransformPool pool, Transform target, IHp iHp)
	//{
	//	Transform hpBar = pool.Rent();
	//	hpBar.localScale = Vector3.one;
	//	hpBar?.GetComponent<UIHpBarController>()?.Initialize(target, iHp);

	//	return hpBar;
	//}

	//public static Transform CreateDamage(this TransformPool pool, Transform target, Damage damage, UIDamageController.EDamage eDamage)
	//{
	//	Transform damageTransform = pool.Rent();
	//	damageTransform.localScale = Vector3.one;
	//	damageTransform?.GetComponent<UIDamageController>()?.Initialize(target, damage, eDamage);

	//	return damageTransform;
	//}

}
public static class ParticleExtension
{
	//public static Transform CreateGetHit01(this TransformPool pool, Transform target)
	//{
	//	Transform getHit = pool.Rent();
	//	getHit.localScale = Vector3.one;
	//	getHit?.GetComponent<ParticleAutoReturnController>()?.Initialize(target);

	//	return getHit;
	//}
}

public static class MissileExtension
{
	//public static Transform CreateMissile01(this TransformPool pool, Transform shootTransform, int layer, Damage damage)
	//{
	//	Transform missile = pool.Rent();
	//	missile.gameObject.layer = layer;
	//	missile.position = shootTransform.position;
	//	missile.rotation = shootTransform.rotation;
	//	missile.GetComponent<MissileController>()?.Initialize(damage);

	//	return missile;
	//}
}

public static class Rx
{
	public static IObservable<string> GetWWW(string url)
	{
		// convert coroutine to IObservable
		return Observable.FromCoroutine<string>((observer, cancellationToken) => GetWWWCore(url, observer, cancellationToken));
	}

	// IObserver is a callback publisher
	// Note: IObserver's basic scheme is "OnNext* (OnError | Oncompleted)?" 
	private static IEnumerator GetWWWCore(string url, IObserver<string> observer, CancellationToken cancellationToken)
	{
		var www = new UnityEngine.WWW(url);
		while (!www.isDone && !cancellationToken.IsCancellationRequested)
		{
			yield return null;
		}

		if (cancellationToken.IsCancellationRequested) yield break;

		if (www.error != null)
		{
			observer.OnError(new Exception(www.error));
		}
		else
		{
			observer.OnNext(www.text);
			observer.OnCompleted(); // IObserver needs OnCompleted after OnNext!
		}
	}

	//public static IObservable<float> ToObservable(this UnityEngine.AsyncOperation asyncOperation)
	//{
	//	if (asyncOperation == null) throw new ArgumentNullException("asyncOperation");

	//	return Observable.FromCoroutine<float>((observer, cancellationToken) => RunAsyncOperation(asyncOperation, observer, cancellationToken));
	//}

	//private static IEnumerator RunAsyncOperation(UnityEngine.AsyncOperation asyncOperation, IObserver<float> observer, CancellationToken cancellationToken)
	//{
	//	while (!asyncOperation.isDone && !cancellationToken.IsCancellationRequested)
	//	{
	//		observer.OnNext(asyncOperation.progress);
	//		yield return null;
	//	}
	//	if (!cancellationToken.IsCancellationRequested)
	//	{
	//		observer.OnNext(asyncOperation.progress); // push 100%
	//		observer.OnCompleted();
	//	}
	//}

	public static IObservable<Vector3> DragAsObservable()
	{
		var mousePositionAsObservable = Observable.EveryUpdate().Select(_ => Input.mousePosition);

		return mousePositionAsObservable
			.Buffer(2, 1)
			.Select(mousePosition => mousePosition.First() - mousePosition.Last())
			.DistinctUntilChanged()
			.Where(_ => Input.GetMouseButton(0));
	}
}

//// CompositeDisposable is similar with List<IDisposable>, manage multiple IDisposable
//CompositeDisposable disposables = new CompositeDisposable(); // field

//void Start()
//{
//	Observable.EveryUpdate().Subscribe(x => Debug.Log(x)).AddTo(disposables);
//}

//void OnTriggerEnter(Collider other)
//{
//	// .Clear() => Dispose is called for all inner disposables, and the list is cleared.
//	// .Dispose() => Dispose is called for all inner disposables, and Dispose is called immediately after additional Adds.
//	disposables.Clear();
//}

//그러나 파이프 라인에서 특별한 OnCompleted 호출이 필요한 경우에는 TakeWhile, TakeUntil, TakeUntilDestroy 및 TakeUntilDisable을 대신 사용하기 바랍니다.
//RepeatUntilDestroy, RepeatUntilDisable 

//Buffer 활용
//Buffer 연산자를 사용하면 이전 프레임에서의 값과 현재 프레임에서의 값의 비교와 같이 스트림에 흐르는 메시지를 비교하는 것과 같은 작업을 손쉽게 처리할 수 있습니다.
//이전 값과 현재 값의 비교가 필요하는 경우는 아래와 같은 예들이 있겠네요.
//이전 프레임에서의 플레이어의 좌표와 지금 프레임에서의 플레이어의 좌표의 비교
//이전 프레임에서의 마우스 좌표와 현재 프레임에서의 마우스 좌표의 비교.
//Buffer 연산자를 사용할 때에는 몇 개를 모아서 스트림으로 방류할 것인지를 정하는 count 개수와 다음 방류시 이전 값중 몇 개를 버릴 것인지를 정하는 skip 개수를 지정할 수 있습니다.
//    Buffer ( count, skip )
//Buffer 연산자 호출시 skip값을 지정하면 Buffer가 이벤트를 방류한 뒤 어느 타이밍에 다시 방류할지를 지정할 수 있습니다. skip값을 지정하지 않은 경우에는 Buffer가 지정한 count값과 같은 값을 skip값으로 설정합니다..
//// Skip을 지정하지 않은 경우
//Observable.Range ( 1 ,  10 )
//    .Select ( x => x.ToString ())
//    .Buffer ( 2 )  // 2 개씩 정리 (방류 한 후에 2 개 날려에서 다음을 방류하는 .Buffer (2, 2)와 동일)
//    .Subscribe ( x  =>

//	{
//	// Buffer의 내용을 표시
//	Debug.Log(x.Aggregate<string>((p, c) => p.ToString() + "," + c.ToString()));
//});
//위의 결과는 아래와 같습니다.
//1,2
//3,4
//5,6
//7,8
//9,10
//Observable.Range ( 1 ,  10 )
//    .Select ( x => x . ToString ())
//    .Buffer ( 2 , 1 )  // 2 개씩 넣어. 방류 후 1 개 날려 방류
//    .Subscribe ( x  =>

//	{
//	// Buffer의 내용을 표시
//	Debug.Log(x.Aggregate<string>((p, c) => p.ToString() + "," + c.ToString()));
//});
//1,2
//2,3
//3,4
//4,5
//5,6
//6,7
//7,8
//9,10
//10
//Observable.Range ( 1 ,  10 )
//    .Select ( x => x . ToString ())
//    .Buffer ( 3 , 2 )  // 3 개씩 넣어. 방류 후 2 개 날려 방류
//    .Subscribe ( x  =>

//	{
//	// Buffer의 내용을 표시
//	Debug.Log(x.Aggregate<string>((p, c) => p.ToString() + "," + c.ToString()));
//});
//1,2,3
//3,4,5
//5,6,7
//7,8,9
//9,10
//Buffer의 Skip을 이용하면 쉽게 스트림의 값을 비교하는 것이 가능하다.
//Buffer를 이용하여 이전 메시지와 현재 메시지의 차이 비교하기
//this . UpdateAsObservable ()
//    .Select ( _  =>  this.transform.position )
//    .Buffer ( 2, 1 )
//    .Where ( x  =>  x.Count  ==  2 )  // OnCompleted시 1 개만 흘러 나오는 것을 컷
//    .Select ( x  =>  x.Last ()  -  x.First ())
//    .Subscribe ( x  =>  Debug.Log ( "Delta :"  +  x ));
//다음은 Buffer(n, 1)을 이용해서 Update의 10회 호출시 Time.deltatime의 평균을 계산하는 코드이다.
//this . UpdateAsObservable ()
//    .Select ( _  =>  Time.deltaTime )
//    .Buffer ( 10,  1 )
//    .Select ( x  =>  x.Average ())
//    .Subscribe ( x  =>  Debug . Log ( "Average :"  +  x ));
//Pairsiwe 연산자
//Pairwise 연산자는 Buffer(2, 1) 과 유사하게 사용할 수 있는 연산자입니다.
//// Pairwise ()
//Observable . Range ( 1 ,  10 )
//    . Pairwise ()
//    . Subscribe ( x  =>
//        Debug . Log ( string . Format ( "{0}, {1}" ,  x . Previous ,  x . Current ))
//    );

//// Buffer (2,1)
//Observable . Range ( 1 ,  10 )
//    . Select ( x => x . ToString ())
//    . Buffer ( 2 , 1 )
//    . Subscribe ( x  =>

//	{
//	// Buffer의 내용을 표시
//	Debug.Log(x.Aggregate<string>((p, c) => p.ToString() + "," + c.ToString()));
//});
//아래는 결과입니다.
//(Pairwise)
//1,2
//2,3
//3,4
//4,5
//5,6
//6,7
//7,8
//9,10

//(Buffer)
//1,2
//2,3
//3,4
//4,5
//5,6
//6,7
//7,8
//9,10
//10 //  ←
//Pairwise는 항상 쌍으로 값을 보내는 반면에 Buffer(2, 1)의 마지막 값은 skip으로 인해 하나만 들어온다 것이 다른 점입니다.