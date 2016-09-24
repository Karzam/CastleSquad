using UnityEngine;
using System.Collections;

public class FinishButton : ButtonElement
{
	GameObject character;

	public void SetCharacter(GameObject pCharacter)
	{
		character = pCharacter;
	}

	public override void OnMouseDown()
	{
		base.OnMouseDown();
	}

	public override void OnMouseUp()
	{
		base.OnMouseUp();

		character.GetComponent<Character>().SetFinishState();
	}

}

