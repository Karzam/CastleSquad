using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Enemy characters class
 */
public class EnemyCharacter : Character
{
	public static List<GameObject> enemyList = new List<GameObject>();

	int moveSpeed = 100;

	List<Vector2> pathTiles;

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
		Vector2 destTile = availableTiles[4];
		pathTiles = GetTilesPath(destTile);
		StartCoroutine(MoveTranslation());
	}

	IEnumerator MoveTranslation()
	{
		Vector2 tile = pathTiles[0];

		for(;;)
		{
			if (!CheckTilePosition(tile))
			{
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(MapManager.instance.GetViewCoordinates(tile).x + tileOffset.x,
					MapManager.instance.GetViewCoordinates(tile).y + tileOffset.y, 0), moveSpeed * Time.deltaTime);
			}
			if (Vector3.Distance(transform.localPosition, new Vector3(MapManager.instance.GetViewCoordinates(tile).x + tileOffset.x,
				MapManager.instance.GetViewCoordinates(tile).y + tileOffset.y, 0)) == 0)
			{
				pathTiles.Remove(tile);
				StopAllCoroutines();
				if (pathTiles.Count != 0)
				{
					StartCoroutine(MoveTranslation());
				}
			}
			yield return null;
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

		bool distXNeg = (coordinates.x - destTile.x < 0);
		bool distYNeg = (destTile.y - coordinates.y < 0);

		if (distX < 0) distX *= -1;
		if (distY < 0) distY *= -1;

		for (int x = 0; x <= distX; x++)
		{
			if (MapManager.instance.model[new Vector2(x, coordinates.y)] == null && x != 0)
			{
				tilesPath.Add(new Vector2(coordinates.x - x * (distXNeg ? -1 : 1), coordinates.y));
			}

			if (Mathf.FloorToInt(distX) == x)
			{
				for (int y = 0; y <= distY; y++)
				{
					if (MapManager.instance.model[new Vector2(coordinates.x, y)] == null && y != 0)
					{
						tilesPath.Add(new Vector2(coordinates.x - x * (distXNeg ? -1 : 1), coordinates.y + y * (distYNeg ? -1 : 1)));
					}
				}
			}
		}

		return tilesPath;
	}

	/*
	 * Return true if the character
	 * is on the tile
	 */
	bool CheckTilePosition(Vector2 destTile)
	{
		if (transform.localPosition.x == MapManager.instance.GetViewCoordinates(destTile).x + tileOffset.x
			&& transform.localPosition.y == MapManager.instance.GetViewCoordinates(destTile).y + tileOffset.y)
		{
			return true;
		}

		return false;
	}

	public override void OnMouseDown()
	{
		if (IsCharacterCastingSkill())
		{
			SetTargetedState();
		}
		else if (state == State.Idle)
		{
			DeselectAllCharacters();
			SetSelectedState(false);
		}
		else if (state == State.Selected)
		{
			SetIdleState();
		}
	}

	public override void OnMouseUp()
	{
	}

	protected override void SetIdleState()
	{
		base.SetIdleState();
	}

	protected override void SetSelectedState(bool displayMovingTiles)
	{
		base.SetSelectedState(displayMovingTiles);
		HUDManager.instance.DisplaySideButtons(transform.position, gameObject, false);
		HUDManager.instance.DisplayBottomDetails(gameObject, false);
	}

	protected override void SetTargetedState()
	{
		base.SetTargetedState();
	}

	public override void SetFinishState()
	{
		base.SetFinishState();
		sprite.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
	}

}

