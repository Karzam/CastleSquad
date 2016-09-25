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

	// Tiles highlight sprites
	Sprite highlightSprite;
	Sprite targetHighlightSprite;
	Sprite moveHighlightSprite;
	Sprite selectedSprite;

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
		moveHighlightSprite = Resources.Load<Sprite>("Sprites/Tiles/MoveHighlight") as Sprite;
		targetHighlightSprite = Resources.Load<Sprite>("Sprites/Tiles/TargetHighlight") as Sprite;
		selectedSprite = Resources.Load<Sprite>("Sprites/Tiles/SelectedHighlight") as Sprite;
	}

	/*
	 * Draw the tilemap and fill the model array
	 */
	public void InitializeMap()
	{
		GameObject grassMap = Resources.Load<GameObject>("Prefabs/Maps/Grass") as GameObject;
		grassMap.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Maps/grass_map");
		GameObject map = Instantiate(grassMap, GameObject.Find("Map").transform) as GameObject;
		map.transform.position = new Vector3(-156, 66.9f, 0);

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
		return model[new Vector2(Mathf.Floor(position.x / TILE_SIZE), Mathf.Floor(position.y / TILE_SIZE))];
	}

	/*
	 * Set highlight on tiles
	 */
	public void EnableTilesHighlight(List<Vector2> tiles, bool isTargetTiles = false)
	{
		Transform parent = GameObject.Find("Map").transform;

		highlightSprite = (isTargetTiles ? targetHighlightSprite : moveHighlightSprite);
		GameObject lHighlight = Resources.Load<GameObject>("Prefabs/Tiles/HighlightedTile") as GameObject;
		lHighlight.GetComponent<SpriteRenderer>().sprite = highlightSprite;

		foreach (Vector2 tile in tiles)
		{
			GameObject highlight = Instantiate(lHighlight);
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

	/*
	 * Tint tile highlight in red
	 */
	public void TintTileHighlight(Vector2 tile)
	{
		foreach (GameObject lTile in highlightTiles)
		{
			if (lTile != null)
			{
				if (GetModelCoordinates(lTile.transform.localPosition) != tile) {
					if (lTile.GetComponent<SpriteRenderer>().sprite != highlightSprite) {
						lTile.GetComponent<SpriteRenderer>().sprite = highlightSprite;
					}
				}
				else {
					lTile.GetComponent<SpriteRenderer>().sprite = selectedSprite;
				}
			}
		}
	}

}

