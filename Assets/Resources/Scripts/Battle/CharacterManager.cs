using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
	public static CharacterManager instance;

	// Player characters
	List<GameObject> squad;

	void Awake()
	{
		instance = this;
	}
		
	void Start ()
	{
	}

	/*
	 * Add player characters on the map
	 */
	public void InstantiatePlayerSquad()
	{
		// Add characters to the squad
		GameObject warrior = Resources.Load("Prefabs/Characters/Warrior") as GameObject;
		GameObject knight = Resources.Load("Prefabs/Characters/Knight") as GameObject;
		GameObject sorcerer = Resources.Load("Prefabs/Characters/Sorcerer") as GameObject;
		GameObject thief = Resources.Load("Prefabs/Characters/Thief") as GameObject;

		squad = new List<GameObject>{warrior, knight, sorcerer, thief};

		Transform parent = GameObject.Find("Characters").transform;

		foreach(GameObject character in squad)
		{
			Instantiate(character, parent);
		}
	}

}

