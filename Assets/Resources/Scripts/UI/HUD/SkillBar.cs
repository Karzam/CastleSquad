using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/**
 * Update and manage skill buttons
 */
public class SkillBar : MonoBehaviour
{
	public static SkillBar instance;

	// Skill manager broadcast
	public delegate void SkillManagerEvent(Skill skill);
	public event SkillManagerEvent updateSkillCast;
	public event SkillManagerEvent updateSkills;


	void Awake()
	{
		instance = this;

		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.GetComponent<SkillButton>().updateSkillSelected += UpdateSkillSelected;
		}
	}
	
	public void SetData(List<Skill> pSkills)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			GameObject child = transform.GetChild(i).gameObject;
			child.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/Skills/" + pSkills[i].data.name) as Sprite;
			child.GetComponent<SkillButton>().Initialize(pSkills[i]);
		}
	}

	/*
	 * Update skill selected
	 */
	void UpdateSkillSelected(Skill skill)
	{
		if (skill != null) {
			updateSkillCast(skill);
			HUDManager.instance.HideSideButtons();
		}
		else {
			updateSkillCast(null);
		}
	}

}

