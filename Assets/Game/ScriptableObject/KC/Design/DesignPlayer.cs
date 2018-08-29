using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

[CreateAssetMenu(menuName = "Desgin/Player")]
public class DesignPlayer : ScriptableObject
{
	public float whrilWindMoveSpeed;
	public float whrilWindRange;
	public float attackRange;
	public float rollRange;

	public StatCollection statCollection;
}
