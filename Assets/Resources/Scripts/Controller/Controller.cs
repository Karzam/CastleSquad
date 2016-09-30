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

	GameObject currentObject;


	void Awake()
	{
		instance = this;
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
			currentObject = hit;
			CharacterManager.instance.HandlePlayerCharacterTouchDown(hit.gameObject);
		}
		else if (hit.layer == Layer.ENEMY_CHARACTER)
		{
			currentObject = hit;
			CharacterManager.instance.HandleEnemyCharacterTouchDown(hit.gameObject);
		}
		else if (hit.layer == Layer.BUTTON)
		{
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

