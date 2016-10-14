using UnityEngine;
using System.Collections;

public class FinishButton : ButtonElement
{
	Character character;

	public void SetCharacter(Character pCharacter)
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

		character.SetFinishState();
	}

}

