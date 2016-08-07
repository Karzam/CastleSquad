using UnityEngine;
using System.Collections;

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

		InstantiateCharacters();

		StartPlayerPhase();
	}

	/*
	 * Draw player and enemy squads
	 */
	void InstantiateCharacters()
	{
		CharacterManager.instance.InstantiatePlayerSquad();
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

