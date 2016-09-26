using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manage map tiles (green, orange, red...)
 */
public class TileManager : MonoBehaviour
{
	public static TileManager instance;

	// Tile types
	public enum Tile {Move, Skill, Target};

	// Move tile (green)
	Sprite moveSprite;

	// Skill aiming tile (orange)
	Sprite skillSprite;

	// Target / Overflown tile (red)
	Sprite targetSprite;

	// Current tile sprite
	Sprite currentSprite;

	// Current tiles list
	List<GameObject> tiles = new List<GameObject>();


	void Awake()
	{
		instance = this;
	}

	void Start ()
	{
		moveSprite = Resources.Load<Sprite>("Sprites/Tiles/Move") as Sprite;
		skillSprite = Resources.Load<Sprite>("Sprites/Tiles/Skill") as Sprite;
		targetSprite = Resources.Load<Sprite>("Sprites/Tiles/Target") as Sprite;
	}

	/*
	 * Display tiles with type and coordinates in parameters
	 * NB : Target tiles are replacing current tiles sprite and not removing them
	 */
	public void DisplayTiles(List<Vector2> pTiles, Tile type)
	{
		Transform parent = GameObject.Find("Map").transform;
		GameObject lHighlight = Resources.Load<GameObject>("Prefabs/Tiles/Tile") as GameObject;

		if (type != Tile.Target)
		{
			currentSprite = GetTileSprite(type);
			lHighlight.GetComponent<SpriteRenderer>().sprite = currentSprite;

			RemoveTiles();

			foreach (Vector2 tile in pTiles)
			{
				GameObject highlight = Instantiate(lHighlight);
				highlight.transform.parent = parent;
				highlight.transform.localPosition = MapManager.instance.GetViewCoordinates(tile);
				tiles.Add(highlight);
			}
		}
		else
		{
			foreach (Vector2 lTarget in pTiles)
			{
				foreach (GameObject lTile in tiles)
				{
					if (lTile != null)
					{
						if (MapManager.instance.GetModelCoordinates(lTile.transform.localPosition) == lTarget)
						{
							if (lTile.GetComponent<SpriteRenderer>().sprite != targetSprite)
							{
								lTile.GetComponent<SpriteRenderer>().sprite = targetSprite;
							}
						}
						else
						{
							lTile.GetComponent<SpriteRenderer>().sprite = currentSprite;
						}
					}
				}
			}
		}
	}

	/*
	 * Delete highlighted tiles
	 */
	public void RemoveTiles()
	{
		if (tiles.Count > 0)
		{
			foreach (GameObject tile in tiles)
			{
				Destroy(tile);
			}
		}
	}

	/*
	 * Get tile type sprite
	 */
	Sprite GetTileSprite(Tile type)
	{
		Sprite sprite;

		if (type == Tile.Move) sprite = moveSprite;
		else if (type == Tile.Skill) sprite = skillSprite;
		else sprite = targetSprite;

		return sprite;
	}

}

