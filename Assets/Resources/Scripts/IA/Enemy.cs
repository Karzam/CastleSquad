using UnityEngine;
using System.Collections;

/*
 * IA which controls enemy characters
 */
public class Enemy : MonoBehaviour
{
	public static Enemy instance;

	void Awake()
	{
		instance = this;
		BattleManager.instance.onStartEnemyPhase += StartEnemyPhase;
	}

	void StartEnemyPhase()
	{
		EnemyCharacter.enemyList[0].GetComponent<EnemyCharacter>().Move();
	}

}
