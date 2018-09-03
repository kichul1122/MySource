using UnityEngine;

public interface IDamageable
{
	void TakeDamage(Damage damage);
}

public struct Damage
{
	public long value;
	public bool isCritical;
	public bool isGetHit;
	public Vector3? position;

	public Damage(long value, bool isCritical = false, bool isGetHit = false, Vector3? position = null)
	{
		this.value = value;
		this.isCritical = isCritical;
		this.isGetHit = isGetHit;
		this.position = position;
	}
}

public interface IHp
{
	long CurrentHp { get; set; }
	long MaxHp { get; }
}

public interface IEnemyData
{
	DesignEnemy DesginEnemy { get; }
	CharacterStatCollection CharacterStatCollection { get; }
	CharacterStat GetCharacterStat(EStat eStat);
}

public interface IEnemyAttack
{
	void Attack();
}