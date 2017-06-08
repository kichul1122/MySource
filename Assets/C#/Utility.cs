using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
	public static string NowTime()
	{
		//return System.DateTime.Now.ToString("yy/MM/dd-HH:mm:ss");
		return System.DateTime.Now.ToShortTimeString();
	}

}
