using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillButton : ButtonElement
{
	public static List<GameObject> list = new List<GameObject>();

	// Skill bar broadcast
	public delegate void SkillBarEvent(Skill skill);
	public event SkillBarEvent updateSkillSelected;

	public bool isSelected;

	Skill skill;

	public void Initialize(Skill pSkill)
	{
		skill = pSkill;
	}

	public override void OnMouseDown()
	{
		base.OnMouseDown();

		if (!isSelected) {
			isSelected = true;
			updateSkillSelected(skill);
		}
		else {
			isSelected = false;
			updateSkillSelected(null);
		}
	}

}

