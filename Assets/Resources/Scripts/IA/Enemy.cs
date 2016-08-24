using UnityEngine;
using System.Collections;

/*
 * Controls enemy characters
 */
public class Enemy : MonoBehaviour
{
	public static Enemy instance;

	void Awake()
	{
		BattleManager.instance.onStartEnemyPhase += StartEnemyPhase;
	}

	void StartEnemyPhase()
	{
		EnemyCharacter.enemyList[0].GetComponent<EnemyCharacter>().Move();
	}

	void Move()
	{

	}

}
