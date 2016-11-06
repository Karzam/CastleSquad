using UnityEngine;
using System.Collections;

public class ValidateSkillButton : ButtonElement
{
	
	public override void OnMouseDown()
	{
		base.OnMouseDown();

		SkillManager.instance.TriggerSkill();
	}

	public override void OnMouseUp()
	{
		base.OnMouseUp();
	}

}

