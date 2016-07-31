using UnityEngine;
using System.Collections;

public class BattleManager : MonoBehaviour
{
	public static BattleManager instance;

	// UI broadcast
	public delegate void UIEvent();
	public event UIEvent onStartPlayerPhase;

	// Battle phases
	enum Phase {Player, Enemy};

	// Current battle phase
	Phase phase;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	public void Start ()
	{
		StartPlayerPhase();
	}

	// Trigger player phase
	void StartPlayerPhase()
	{
		phase = Phase.Player;
		onStartPlayerPhase();
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}

