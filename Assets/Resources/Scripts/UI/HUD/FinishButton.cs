using UnityEngine;
using System.Collections;

public class FinishButton : Button
{
	GameObject character;

	public void SetCharacter(GameObject pCharacter)
	{
		character = pCharacter;
	}

	public void OnTouchDown()
	{
		base.OnTouchDown();
	}

	public void OnTouchUp()
	{
		base.OnTouchUp();

		character.GetComponent<Character>().SetFinishState();
	}

}

