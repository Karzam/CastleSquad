using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour
{
	public static CharacterManager instance;

	void Awake()
	{
		instance = this;
	}

	// Battle phases broadcast
	public delegate void BattlePhaseEvent();
	public event BattlePhaseEvent onEndPlayerPhase;
	public event BattlePhaseEvent onEndEnemyPhase;

	public void EndPlayerPhase()
	{
		onEndPlayerPhase();
	}

	public void EndEnemyPhase()
	{
		onEndPlayerPhase();
	}

}

