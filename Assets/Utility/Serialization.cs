using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

// List <T>
[Serializable]
public class Serialization<T>
{
	[SerializeField]
	List<T> target;
	public List<T> ToList() { return target; }

	public Serialization(List<T> target)
	{
		this.target = target;
	}
}

// Dictionary <TKey, TValue>
[Serializable]
public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
{
	[SerializeField]
	List<TKey> keys;
	[SerializeField]
	List<TValue> values;

	Dictionary<TKey, TValue> target;
	public Dictionary<TKey, TValue> ToDictionary() { return target; }

	public Serialization(Dictionary<TKey, TValue> target)
	{
		this.target = target;
	}

	public void OnBeforeSerialize()
	{
		keys = new List<TKey>(target.Keys);
		values = new List<TValue>(target.Values);
	}

	public void OnAfterDeserialize()
	{
		int count = Math.Min(keys.Count, values.Count);
		target = new Dictionary<TKey, TValue>(count);
		for (int i = 0; i < count; ++i)
		{
			target.Add(keys[i], values[i]); 
		}
	}
}

/*
// List <T> -> Json 문자열 (예 : List <Enemy>) 
string str = JsonUtility.ToJson(new Serialization<Enemy>(enemies)); // 출력 : { "target": [{ "name ":"슬라임 ","skills ":"공격 "]}, {"name ":"킹 슬라임 ","skills ":"공격 ","회복 "]}]} 
																	// Json 문자열 -> List <T>
List<Enemy> enemies = JsonUtility.FromJson<Serialization<Enemy>>(str).ToList();

// Dictionary <TKey, TValue> -> Json 문자열 (예 : Dictionary <int, Enemy>) 
string str = JsonUtility.ToJson(new Serialization<int, Enemy>(enemies)); // 출력 : { "keys ": [1000,2000]"values ": [{"name ":"슬라임 ","skills ":"공격 "]}, {"name ":"킹 슬라임 ","skills ":"공격 ","회복 "]}]} 
																		 // Json 문자열 -> Dictionary <TKey, TValue> 
Dictionary<int, Enemy> enemies = JsonUtility.FromJson<Serialization<int, Enemy>>(str).ToDictionary();

*/

// BitArray
[Serializable]
public class SerializationBitArray : ISerializationCallbackReceiver
{
	[SerializeField]
	string flags;

	BitArray target;
	public BitArray ToBitArray() { return target; }

	public SerializationBitArray(BitArray target)
	{
		this.target = target;
	}

	public void OnBeforeSerialize()
	{
		var ss = new System.Text.StringBuilder(target.Length);
		for (var i = 0; i < target.Length; ++i)
		{
			ss.Insert(0, target[i] ? '1' : '0');
		}
		flags = ss.ToString();
	}

	public void OnAfterDeserialize()
	{
		target = new BitArray(flags.Length);
		for (var i = 0; i < flags.Length; ++i)
		{
			target.Set(flags.Length - i - 1, flags[i] == '1');
		}
	}
}

/*
BitArray bits = new BitArray ( 4 );
bits.Set ( 1 , true );

// BitArray -> Json 문자열 
var str = JsonUtility.ToJson ( new SerializationBitArray (bits)); // 출력 : { "flags": "0010"} 
// Json 문자열 -> BitArray
BitArray bits = JsonUtility.FromJson <SerializationBitArray> (s) .ToBitArray ();

*/
