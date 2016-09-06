using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Enemy characters class
 */
public class EnemyCharacter : Character
{
	public static List<GameObject> enemyList = new List<GameObject>();

	int moveSpeed = 2;

	// Use this for initialization
	public override void Initialize(string name, Vector2 startCoordinates)
	{
		base.Initialize(name, startCoordinates);
		enemyList.Add(gameObject);
		data = DataParser.GetEnemyCharacterData(name.Split("_"[0])[0]);
		SetSprite();
	}

	/*
	 * Assign sprite to the character
	 */
	void SetSprite()
	{
		sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Characters/" + data.name + "_" + data.type);
		sprite.GetComponent<SpriteRenderer>().flipX = true;
	}

	/*
	 * Move unit automatically
	 */
	public void Move()
	{
		List<Vector2> availableTiles = GetDestinationTiles();

		Vector2 destTile = new Vector2(9, 3);

		List<Vector2> pathTiles = GetTilesPath(destTile);

		for (int i = 0; i < pathTiles.Count; i++)
		{
			StartCoroutine("MoveTranslation", pathTiles[i]);
			print(pathTiles[i]);
		}
	}

	/*
	 * Returns an array of tiles path
	 */
	List<Vector2> GetTilesPath(Vector2 destTile)
	{
		List<Vector2> tilesPath = new List<Vector2>();

		float distX = coordinates.x - destTile.x;
		float distY = destTile.y - coordinates.y;

		for (int x = 0; x <= distX; x++)
		{
			if (MapManager.instance.model[new Vector2(x, coordinates.y)] == null && x != 0)
			{
				tilesPath.Add(new Vector2(coordinates.x - x, coordinates.y));
			}

			if (Mathf.FloorToInt(distX) == x)
			{
				for (int y = 0; y <= distY; y++)
				{
					if (MapManager.instance.model[new Vector2(coordinates.x, y)] == null && y != 0)
					{
						tilesPath.Add(new Vector2(coordinates.x - x, coordinates.y + y));
					}
				}
			}
		}

		return tilesPath;
	}
	
	IEnumerator MoveTranslation(Vector2 destTile)
	{
		while (!CheckTilePosition(destTile))
		{
			transform.Translate(new Vector3(destTile.x, destTile.y, 0) * Time.deltaTime * 4);
			yield return null;
		}
	}

	/*
	 * Return true if the character
	 * is on the tile
	 */
	bool CheckTilePosition(Vector2 tile)
	{
		if (transform.localPosition.x == MapManager.instance.GetViewCoordinates(tile).x + tileOffset.x
			&& transform.localPosition.y == MapManager.instance.GetViewCoordinates(tile).y + tileOffset.y)
		{
			return true;
		}

		return false;
	}

	protected override void OnTouchDown()
	{
		if (state == State.Idle)
		{
			DeselectAllCharacters();
			SetSelectedState(false);
		}
		else if (state == State.Selected)
		{
			SetIdleState();
		}
	}

	protected override void OnTouchUp()
	{
	}

	protected override void SetIdleState()
	{
		base.SetIdleState();
	}

	protected override void SetSelectedState(bool displayMovingTiles)
	{
		base.SetSelectedState(displayMovingTiles);
		CharacterHUD.instance.Display(transform.position, gameObject, data, false);
	}

	public override void SetFinishState()
	{
		base.SetFinishState();
		sprite.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
	}

}

