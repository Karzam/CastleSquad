using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillsBar : MonoBehaviour
{
	
	public void SetData(CharacterData pData, Vector2 coordinates)
	{
		List<string> skills = new List<string>(new string[] {pData.skl_1, pData.skl_2, pData.skl_3, pData.skl_4});

		for (int i = 0; i < transform.childCount; i++)
		{
			GameObject child = transform.GetChild(i).gameObject;
			child.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/Skills/" + skills[i]) as Sprite;
			child.GetComponent<SkillButton>().Initialize(DataParser.GetSkillData(skills[i]), coordinates);
		}
	}

}

