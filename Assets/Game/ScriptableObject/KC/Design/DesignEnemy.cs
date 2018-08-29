using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

[CreateAssetMenu(menuName = "Desgin/Enemy")]
public class DesignEnemy : ScriptableObject
{
	public float searchRange;
	public float attackRange;
	public float rotationSpeed;
	public float directionRange;

	public GameObject weaponPrefab;
	
	public StatCollection statCollection;
}
