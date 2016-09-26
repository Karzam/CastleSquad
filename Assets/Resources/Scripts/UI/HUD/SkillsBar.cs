using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillsBar : MonoBehaviour
{
	
	public void SetData(GameObject character, CharacterData pData, List<Skill> skills)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			GameObject child = transform.GetChild(i).gameObject;
			child.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/Skills/" + skills[i].data.name) as Sprite;
			child.GetComponent<SkillButton>().Initialize(character, skills[i], character.GetComponent<Character>().coordinates);
		}
	}

}

