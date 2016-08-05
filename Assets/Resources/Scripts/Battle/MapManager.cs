using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
	public static MapManager instance;

	[SerializeField]
	GameObject grassMap;

	[SerializeField]
	GameObject highlightedTile;

	// Coordinates map
	public Dictionary<Vector2, GameObject> model;

	List<GameObject> highlightTiles = new List<GameObject>();

	public const float TILE_SIZE = 22.4f;
	
	const int MAP_WIDTH = 14;
	const int MAP_HEIGHT = 6;

	void Awake()
	{
		instance = this;
	}

	void Start ()
	{
	}

	/*
	 * Draw the tilemap and fill the model array
	 */
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

	/*
	 * Get model coordinates with view position
	 */
	public Vector2 GetModelCoordinates(Vector2 position)
	{
		return new Vector2(Mathf.Floor(position.x / MAP_WIDTH), Mathf.Floor(position.y / MAP_HEIGHT));
	}

	/*
	 * Get view position with model coordinates
	 */
	public Vector2 GetViewCoordinates(Vector2 position)
	{
		return new Vector2(position.x * TILE_SIZE, position.y * TILE_SIZE);
	}

	/*
	 * Set highlight on tiles available to move
	 */
	public void EnableTilesHighlight(List<Vector2> tiles)
	{
		Transform parent = GameObject.Find("Map").transform;

		foreach (Vector2 tile in tiles)
		{
			GameObject highlight = Instantiate(highlightedTile) as GameObject;
			highlight.transform.parent = parent;
			highlight.transform.localPosition = GetViewCoordinates(tile);
			highlightTiles.Add(highlight);
		}
	}

	/*
	 * Delete highlight on tiles
	 */
	public void DisableTilesHighlight()
	{
		foreach (GameObject tile in highlightTiles)
		{
			Destroy(tile);
		}
	}

}

