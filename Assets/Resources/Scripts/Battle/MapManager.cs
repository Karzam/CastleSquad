using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
	public static MapManager instance;

	[SerializeField]
	GameObject grassMap;

	Dictionary<Vector2, GameObject> model;

	public const float TILE_SIZE = 22.4f;
	
	const int MAP_WIDTH = 14;
	const int MAP_HEIGHT = 6;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start ()
	{
	}

	public void InitializeMap()
	{
		GameObject map = Instantiate(grassMap, GameObject.Find("Map").transform) as GameObject;
		map.transform.position = new Vector3(-156, 54, 0);

		model = new Dictionary<Vector2, GameObject>();

		for (int x = 0; x < MAP_WIDTH; x++)
		{
			for (int y = 0; y < MAP_HEIGHT; y++)
			{
				model.Add(new Vector2(x, y), null);
			}
		}
	}

	public Vector2 GetModelCoordinates(Vector2 position)
	{
		return new Vector2(Mathf.Floor(position.x / MAP_WIDTH), Mathf.Floor(position.y / MAP_HEIGHT));
	}

	public Vector2 GetViewCoordinates(Vector2 position)
	{
		return new Vector2(position.x * TILE_SIZE, position.y * TILE_SIZE);
	}

}

