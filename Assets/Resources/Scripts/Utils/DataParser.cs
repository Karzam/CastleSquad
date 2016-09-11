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
		using (StreamReader file = new StreamReader(Application.dataPath + "Assets/Resources/JSON/player_characters.json"))
		{
			string json = file.ReadToEnd();
			CharacterData[] characters = JsonHelper.FromJson<CharacterData>(json);

			foreach (var character in characters)
			{
				if (character.name == name) {
					return character;
				}
			}
			return null;
		}
	}

	/*
	 * Get enemy character name and return 
	 * a data object with all stats
	 */
	public static CharacterData GetEnemyCharacterData(string name)
	{
		using (StreamReader file = new StreamReader(Application.dataPath + "Assets/Resources/JSON/enemy_characters.json"))
		{
			string json = file.ReadToEnd();
			CharacterData[] characters = JsonHelper.FromJson<CharacterData>(json);

			foreach (var character in characters)
			{
				if (character.name == name) {
					return character;
				}
			}
			return null;
		}
	}

}

