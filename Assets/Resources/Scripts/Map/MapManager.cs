using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manage the tilemap and contains
 * model and view properties & methods
 */
public class MapManager : MonoBehaviour
{
	public static MapManager instance;

	public Dictionary<Vector2, GameObject> model;

	public const float TILE_SIZE = 26.8f;
	
	public const int MAP_WIDTH = 10;
	public const int MAP_HEIGHT = 5;


	void Awake()
	{
		instance = this;
	}

	/*
	 * Draw the tilemap and fill the model array
	 */
	public void Initialize()
	{
		GameObject grassMap = Resources.Load<GameObject>("Prefabs/Maps/Grass") as GameObject;
		grassMap.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Maps/grass_map");
		GameObject map = Instantiate<Object>(grassMap) as GameObject;
        map.transform.parent = GameObject.Find("Map").transform;
        map.transform.position = new Vector3(-136, 66f, 0);

		model = new Dictionary<Vector2, GameObject>();

		for (int x = 0; x < MAP_WIDTH; x++)
		{
			for (int y = 0; y < MAP_HEIGHT; y++)
			{
				model.Add(new Vector2(x, y), null);
			}
		}
	}

	/*
	 * Update model with new object coordinates
	 */
	public void UpdateModel(Vector2 coordinates, GameObject obj)
	{
		Vector2 oldCoordinates = new Vector2();

		// Suppress old object
		foreach (KeyValuePair<Vector2, GameObject> entry in model)
		{
			if (entry.Value != null)
			{
				if (entry.Value.Equals(obj))
				{
					oldCoordinates = entry.Key;
				}
			}
		}

		model[oldCoordinates] = null;

		// Insert new object
		model[coordinates] = obj;
	}

	/*
	 * Get model coordinates with view position
	 */
	public Vector2 GetModelCoordinates(Vector2 position)
	{
		return new Vector2(Mathf.Floor(position.x / TILE_SIZE), Mathf.Floor(position.y / TILE_SIZE));
	}

	/*
	 * Get view position with model coordinates
	 */
	public Vector2 GetViewCoordinates(Vector2 position)
	{
		return new Vector2(position.x * TILE_SIZE, position.y * TILE_SIZE);
	}

	/*
	 * Return object at model coordinates
	 */
	public GameObject GetObjectWithModelCoordinates(Vector2 position)
	{
		return model[new Vector2(position.x, position.y)];
	}

}

