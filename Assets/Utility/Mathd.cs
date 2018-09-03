///////////////////
///KC_3D PROJECT///
///////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public static class Mathd
{
	public static double Lerp(double a, double b, double t)
	{
		return a + (b - a) * Clamp01(t);
	}

	public static double Repeat(double t, double length)
	{
		return t - Floor(t / length) * length;
	}

	public static double Pow(double f, double p)
	{
		return System.Math.Pow(f, p);
	}

	public static double Floor(double f)
	{
		return System.Math.Floor(f);
	}

	public static double Clamp01(double value)
	{
		double result;
		if (value < 0)
		{
			result = 0;
		}
		else if (value > 1)
		{
			result = 1;
		}
		else
		{
			result = value;
		}
		return result;
	}

	public static double Clamp(double value, double min, double max)
	{
		if (value < min)
		{
			value = min;
		}
		else if (value > max)
		{
			value = max;
		}
		return value;
	}
}
