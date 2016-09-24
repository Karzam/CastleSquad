using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DataParser : MonoBehaviour
{
	/*
	 * Get player character name and return 
	 * a data object with all stats
	 */
	public static CharacterData GetPlayerCharacterData(string name)
	{
		string json = Resources.Load<TextAsset>("Scripts/JSON/player_characters").text;
		CharacterData[] characters = JsonHelper.FromJson<CharacterData>(json);

		foreach (var character in characters)
		{
			if (character.name == name) {
				return character;
			}
		}
		return null;
	}

	/*
	 * Get enemy character name and return 
	 * a data object with all stats
	 */
	public static CharacterData GetEnemyCharacterData(string name)
	{
		string json = Resources.Load<TextAsset>("Scripts/JSON/enemy_characters").text;
		CharacterData[] characters = JsonHelper.FromJson<CharacterData>(json);

		foreach (var character in characters)
		{
			if (character.name == name) {
				return character;
			}
		}
		return null;
	}

	/*
	 * Get skill name and return 
	 * a data object with all stats
	 */
	public static SkillData GetSkillData(string name)
	{
		string json = Resources.Load<TextAsset>("Scripts/JSON/player_skills").text;
		SkillData[] skills = JsonHelper.FromJson<SkillData>(json);

		foreach (var skill in skills)
		{
			if (skill.name == name) {
				return skill;
			}
		}
		return null;
	}

}

