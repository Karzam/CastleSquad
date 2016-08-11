using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DataParser : MonoBehaviour
{
	/*
	 * Get character name and return 
	 * a data object with all stats
	 */
	public static CharacterData GetCharacterData(string name)
	{
		using (StreamReader file = new StreamReader("Assets/Resources/JSON/characters.json"))
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

