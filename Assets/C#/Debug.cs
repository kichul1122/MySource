using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugCustom
{
	public enum Color { Red, Green, Max }
	public static void Log(string log, Color color)
	{
		UnityEngine.Debug.Log(string.Format("<color={0}>{1}</color>", color.ToString().ToLower(), log));
	}
	
}
