using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UniRx;

public static class MyRxExtension
{
	//public static IObservable<Tuple<T>> WrapValueToClass<T>(this IObservable<T> source)	where T : struct
	//{
	//	var dummy = 0;
	//	return Observable.Create<Tuple<T>>(observer =>
	//	{
	//		return source.Subscribe(Observer.Create<T>(x =>
	//		{
	//			dummy.GetHashCode(); // capture outer value
	//			var v = new Tuple<T>(x);
	//			observer.OnNext(v);
	//		}, observer.OnError, observer.OnCompleted));
	//	});
	//}

	public static IEnumerable<Tuple<T>> WrapValueToClass<T>(this IEnumerable<T> source) where T : struct
	{
		var e = ((IEnumerable)source).GetEnumerator();
		using (e as IDisposable)
		{
			while (e.MoveNext())
			{
				yield return new Tuple<T>((T)e.Current);
			}
		}
	}
}
