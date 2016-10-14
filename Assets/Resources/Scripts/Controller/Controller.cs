using UnityEngine;
using System.Collections;

/**
 * Receive inputs from the Input Manager (observer pattern)
 * and order different managers (Character, Skill etc...)
 * to handle actions internally.
 */
public class Controller : MonoBehaviour
{
	public static Controller instance;

	void Awake()
	{
		instance = this;
	}

	public void Initialize()
	{
		InputManager.instance.onTouchDown += OnTouchDown;
		InputManager.instance.onTouchUp += OnTouchUp;
	}

	/*
	 * Receive object touch down and order
	 * managers to handle actions
	 */
	void OnTouchDown(GameObject hit)
	{
		if (hit == null)
		{
			CharacterManager.instance.HandleVoidTouch();
		}
		else if (hit.layer == Layer.PLAYER_CHARACTER)
		{
			CharacterManager.instance.HandlePlayerCharacterTouchDown(hit.gameObject);
		}
		else if (hit.layer == Layer.ENEMY_CHARACTER)
		{
			CharacterManager.instance.HandleEnemyCharacterTouchDown(hit.gameObject);
		}
	}

	/*
	 * Receive object touch up and order
	 * managers to handle actions
	 */
	void OnTouchUp(GameObject hit)
	{
		if (hit.layer == Layer.PLAYER_CHARACTER)
		{
			CharacterManager.instance.HandlePlayerCharacterTouchUp(hit.gameObject);
		}
	}
}

