using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manage all the battle phases
 */
public class BattleManager : MonoBehaviour
{
	public static BattleManager instance;

	// UI broadcast
	public delegate void UIEvent();
	public event UIEvent onStartPlayerPhase;
	public event UIEvent onStartEnemyPhase;

	// Battle phases
	public enum Phase {Player, Enemy};

	// Current battle phase
	Phase phase;


	void Awake()
	{
		instance = this;
	}
		
	public void Start ()
	{
		MapManager.instance.InitializeMap();
		BattleUI.instance.Start();

		InstantiatePlayerCharacters();
		InstantiateEnemyCharacters();

		StartPlayerPhase();
	}

	/*
	 * Draw player characters
	 */
	void InstantiatePlayerCharacters()
	{
		Dictionary<string, Vector2> characters = new Dictionary<string, Vector2>()
		{
			{"Tyr", new Vector2(2, 4)},
			{"Ambu", new Vector2(7, 2)},
			{"Orag", new Vector2(3, 1)},
			{"Korri", new Vector2(7, 4)}
		};

		foreach (var character in characters)
		{
			Transform parent = GameObject.Find("Characters").transform;
			GameObject chara = Instantiate(Resources.Load("Prefabs/Battle/PlayerCharacter"), parent) as GameObject;
			chara.GetComponent<Character>().Initialize(character.Key, character.Value);
		}
	}

	/*
	 * Draw enemy characters
	 */
	void InstantiateEnemyCharacters()
	{
		Dictionary<string, Vector2> characters = new Dictionary<string, Vector2>()
		{
			{"Goblin_Warrior", new Vector2(10, 2)}
		};

		foreach (var character in characters)
		{
			Transform parent = GameObject.Find("Characters").transform;
			GameObject chara = Instantiate(Resources.Load("Prefabs/Battle/EnemyCharacter"), parent) as GameObject;
			chara.GetComponent<Character>().Initialize(character.Key, character.Value);
		}
	}

	/*
	 * Trigger player phase
	 */
	void StartPlayerPhase()
	{
		phase = Phase.Player;
		onStartPlayerPhase();
	}

	/*
	 * End player phase and
	 * trigger enemy phase
	 */
	void EndPlayerPhase()
	{
		Invoke("StartEnemyPhase", 1);
	}

	/*
	 * Trigger enemy phase
	 */
	void StartEnemyPhase()
	{
		phase = Phase.Enemy;
		onStartEnemyPhase();
	}

	/*
	 * End enemy phase and
	 * trigger player phase
	 */
	void EndEnemyPhase()
	{
		Invoke("StartPlayerPhase", 1);
	}
}

