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
		StartPlayerPhase();
	}

	/*
	 * Draw player and enemy squads
	 */
	void InstantiatePlayerCharacters()
	{
		Dictionary<string, Vector2> playerCharacters = new Dictionary<string, Vector2>()
		{
			{"Tyr", new Vector2(2, 4)},
			{"Ambu", new Vector2(4, 3)},
			{"Orag", new Vector2(10, 1)},
			{"Korri", new Vector2(7, 4)}
		};

		foreach (var character in playerCharacters)
		{
			InstantiateNewCharacter(character.Key, character.Value);
		}
	}

	void InstantiateNewCharacter(string name, Vector2 coordinates)
	{
		Transform parent = GameObject.Find("Characters").transform;
		GameObject character = Instantiate(Resources.Load("Prefabs/Battle/Character"), parent) as GameObject;
		character.GetComponent<Character>().Initialize(name, coordinates);
	}

	/*
	 * Trigger player phase
	 */
	void StartPlayerPhase()
	{
		phase = Phase.Player;
		onStartPlayerPhase();
	}
}

