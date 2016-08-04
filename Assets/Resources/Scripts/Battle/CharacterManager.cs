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

	// Use this for initialization
	void Start ()
	{
	
	}

	public void InstantiatePlayerSquad()
	{
		// Add characters to the squad
		GameObject warrior = Resources.Load("Prefabs/Characters/Warrior") as GameObject;
		GameObject knight = Resources.Load("Prefabs/Characters/Knight") as GameObject;
		GameObject sorcerer = Resources.Load("Prefabs/Characters/Sorcerer") as GameObject;
		GameObject thief = Resources.Load("Prefabs/Characters/Thief") as GameObject;

		squad = new List<GameObject>{warrior, knight, sorcerer, thief};

		Transform parent = GameObject.Find("Characters").transform;

		// Set parent to map origin
		parent.position = new Vector3(-156, -80.4f, -1);

		foreach(GameObject character in squad)
		{
			Instantiate(character, parent);
		}
	}

}

