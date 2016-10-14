using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour
{
	public static CharacterManager instance;


	void Awake()
	{
		instance = this;
	}

	/*
	 * Handle player character touch down
	 */
	public void HandlePlayerCharacterTouchDown(GameObject character)
	{
		Character charComponent = character.GetComponent<Character>();

		if (charComponent.state == Character.State.Idle)
		{
			Character.DeselectAllCharacters();
			if (!charComponent.moved) charComponent.SetSelectedState();
			else charComponent.SetSelectedState();
		}
		else if (charComponent.state == Character.State.Selected)
		{
			if (!charComponent.moved) charComponent.SetDraggedState();
			else charComponent.SetIdleState();
		}
		else if (charComponent.state == Character.State.Dropped)
		{
			charComponent.SetSelectedState();
		}
	}

	/*
	 * Handle player character touch up
	 */
	public void HandlePlayerCharacterTouchUp(GameObject character)
	{
		Character charComponent = character.GetComponent<Character>();

		if (charComponent.state == Character.State.Dragged)
		{
			charComponent.HandleReleased();
		}
	}

	/*
	 * Handle enemy character actions
	 */
	public void HandleEnemyCharacterTouchDown(GameObject character)
	{
		Character charComponent = character.GetComponent<Character>();

		if (charComponent.state == Character.State.Idle)
		{
			// If selected when casting skill
			if (SkillManager.instance.isCastingSkill()) {
				SkillManager.instance.HandleCharacterSelected(charComponent);
			}
			else {
				Character.DeselectAllCharacters();
				charComponent.SetSelectedState();
			}
		}
		else if (charComponent.state == Character.State.Selected)
		{
			charComponent.SetIdleState();
		}
	}

	/*
	 * Handle void touch
	 */
	public void HandleVoidTouch()
	{
		Character.DeselectAllCharacters();
	}

}

