using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manage map tiles (green, orange, red...)
 */
public class TileManager : MonoBehaviour
{
	public static TileManager instance;

	// Move tile (green)
	Sprite moveSprite;

	// Skill aiming tile (orange)
	Sprite skillSprite;

	// Target / Overflown tile (red)
	Sprite targetSprite;

	// Tiles list
	Dictionary<Vector2, GameObject> tiles = new Dictionary<Vector2, GameObject>();


	void Awake()
	{
		instance = this;
	}

	void Start ()
	{
		moveSprite = Resources.Load<Sprite>("Sprites/Tiles/Move") as Sprite;
		skillSprite = Resources.Load<Sprite>("Sprites/Tiles/Skill") as Sprite;
		targetSprite = Resources.Load<Sprite>("Sprites/Tiles/Target") as Sprite;

		InitializeTiles();
	}

	/*
	 * Initialize tiles list
	 */
	void InitializeTiles()
	{
		Transform parent = GameObject.Find("Map").transform;

		for (int x = 0; x < MapManager.MAP_WIDTH; x++)
		{
			for (int y = 0; y < MapManager.MAP_HEIGHT; y++)
			{
				GameObject lTile = Instantiate(Resources.Load<GameObject>("Prefabs/Tiles/Tile")) as GameObject;
				lTile.transform.parent = parent;
				lTile.transform.localPosition = new Vector2 (MapManager.instance.GetViewCoordinates (new Vector2 (x, y)).x + 3.2f, MapManager.instance.GetViewCoordinates (new Vector2 (x, y)).y + 0.4f);
				tiles.Add(new Vector2(x, y), lTile);
			}
		}
	}

	/*
	 * Set tiles sprite
	 */
	public void DisplayTiles(List<Vector2> pList, HighlightType pType)
	{
		if (pType != HighlightType.Target) {
			RemoveTiles();
		}

		foreach (Vector2 pTile in pList)
		{
			foreach (KeyValuePair<Vector2, GameObject> tile in tiles)
			{
				if (pTile == tile.Key) {
					tile.Value.GetComponent<SpriteRenderer>().sprite = GetTileSprite(pType);
				}
			}
		}
	}

	/*
	 * Delete tiles sprite
	 * If type parameter specified, replace with type sprite
	 */
	public void RemoveTiles(List<Vector2> pList = default(List<Vector2>), HighlightType pType = default(HighlightType))
	{
		if (pList != null)
		{
			foreach (Vector2 pTile in pList)
			{
				if (pType != null) {
					tiles[new Vector2(pTile.x, pTile.y)].GetComponent<SpriteRenderer>().sprite = GetTileSprite(pType);
				}
				else {
					tiles[new Vector2(pTile.x, pTile.y)].GetComponent<SpriteRenderer>().sprite = null;
				}
			}
		}
		else
		{
			foreach (KeyValuePair<Vector2, GameObject> pTile in tiles)
			{
				pTile.Value.GetComponent<SpriteRenderer>().sprite = null;
			}
		}
	}

	/*
	 * Get tile type sprite
	 */
	Sprite GetTileSprite(HighlightType type)
	{
		Sprite sprite;

		if (type == HighlightType.Move) sprite = moveSprite;
		else if (type == HighlightType.Skill) sprite = skillSprite;
		else sprite = targetSprite;

		return sprite;
	}

}

