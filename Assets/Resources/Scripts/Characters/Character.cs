using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Base class for all player characters
 * Contains interactions with the player
 */
public class Character : MonoBehaviour
{
	public static List<GameObject> list = new List<GameObject>();

	// Character data
	CharacterData data;

	int movementPoints;
	Vector2 startCoordinates;

	// Current model coordinates
	Vector2 coordinates;

	// Overflown coordinates when dragged
	Vector2 overflownCoordinates = Vector2.zero;

	// Sprite child
	GameObject sprite;

	/*
	 * Current state of the character :
	 * 
	 * Idle      => Beginning of the player phase
	 * Selected  => Player touched the character
	 * Dragged   => Player caught the character
	 * Dropped   => Character just moved
	 */
	public enum State {Idle, Selected, Dragged, Dropped};

	// Current state
	public State state;

	// Moved ?
	bool moved;

	// Positioning on tile
	Vector2 tileOffset;

	// State update
	Coroutine dragUpdate;

	void Awake()
	{
		InputManager.instance.onTouchVoid += OnTouchVoid;
	}

	public void New(string name)
	{
		list.Add(gameObject);

		data = DataParser.GetCharacterData(name);
		print(data.str);

		sprite = transform.FindChild("Sprite").gameObject;

		tileOffset = new Vector2(MapManager.TILE_SIZE / 2, 4);

		coordinates = new Vector2(4, 3);
		startCoordinates = coordinates;

		SetSprite();
		SetPosition(coordinates);
		SetIdleState();
	}

	/*
	 * Assign sprite to the character
	 */
	void SetSprite()
	{
		sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load("Sprites/Characters/Ambu") as Sprite;
	}

	/*
	 * Change position on the map
	 * with new model coordinates
	 */
	void SetPosition(Vector2 position)
	{
		transform.localPosition = new Vector3(MapManager.instance.GetViewCoordinates(coordinates).x + tileOffset.x,
											  MapManager.instance.GetViewCoordinates(coordinates).y + tileOffset.y, -1);
	}
		
	public void OnTouchDown()
	{
		if (state == State.Idle)
		{
			// Deselect other characters
			foreach (GameObject character in list) {
				character.GetComponent<Character>().SetIdleState();
			}
			if (!moved) SetSelectedState(true);
			else SetSelectedState(false);
		}
		else if (state == State.Selected)
		{
			if (!moved) SetDraggedState();
			else SetIdleState();
		}
		else if (state == State.Dropped)
		{
			SetSelectedState(false);
		}

		foreach (GameObject character in list) {
			print(character.GetComponent<Character>().state);
		}
	}

	public void OnTouchUp()
	{
		if (state == State.Dragged)
		{
			// If moved on available tile
			MapManager.instance.DisableTilesHighlight();
			if (GetMovingTiles().Contains(overflownCoordinates)) {
				SetDroppedState();
				moved = true;
				sprite.transform.eulerAngles = Vector3.zero;
				coordinates = overflownCoordinates;
				SetPosition(coordinates);
			}
			else {
				SetIdleState();
				sprite.transform.eulerAngles = Vector3.zero;
				SetPosition(coordinates);
			}
		}
	}

	void OnTouchVoid()
	{
		// Deselect other characters
		foreach (GameObject character in list) {
			character.GetComponent<Character>().SetIdleState();
		}
		if (state == State.Selected)
		{
			SetIdleState();
		}
	}

	void SetIdleState()
	{
		state = State.Idle;
		CharacterHUD.instance.Hide();
		MapManager.instance.DisableTilesHighlight();
	}

	void SetSelectedState(bool displayMovingTiles)
	{
		state = State.Selected;
		if (displayMovingTiles) MapManager.instance.EnableTilesHighlight(GetMovingTiles());
		CharacterHUD.instance.Display(transform.position);
	}

	void SetDraggedState()
	{
		state = State.Dragged;
		dragUpdate = StartCoroutine(DragUpdate());
		MapManager.instance.EnableTilesHighlight(GetMovingTiles());
		CharacterHUD.instance.Hide();
	}

	void SetDroppedState()
	{
		state = State.Dropped;
	}

	/*
	 * Update dragged character position and animation
	 */
	IEnumerator DragUpdate()
	{
		int direction = 1;
		float spriteHeight = sprite.GetComponent<Renderer>().bounds.size.y;
		Transform dragPoint = transform.FindChild("DragPoint").transform;

		for(;;)
		{
			if (state == State.Dragged) {
				transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
												 Camera.main.ScreenToWorldPoint(Input.mousePosition).y - spriteHeight / 2, -1);
				// Swing animation
				float angle = transform.FindChild("Sprite").localEulerAngles.z;
				angle = (angle > 180) ? angle - 360 : angle;
				if (angle > 6) {
					direction = -1;
				}
				else if (angle < -6) {
					direction = 1;
				}
				transform.FindChild("Sprite").RotateAround(dragPoint.position, Vector3.forward, Time.deltaTime * 40 * direction);
				// Highlight current tile
				Vector2 tileOverflown = MapManager.instance.GetModelCoordinates(transform.localPosition + new Vector3(0, 0, 0));
				if (tileOverflown != overflownCoordinates) {
					overflownCoordinates = tileOverflown;
					MapManager.instance.TintTileHighlight(overflownCoordinates);
				}
			}
			else {
				StopCoroutine(dragUpdate);
			}
			yield return null;
		}
	}

	/*
	 * Returns the list of tiles position available to move
	 */
	List<Vector2> GetMovingTiles()
	{
		List<Vector2> tiles = new List<Vector2>();

		foreach (Vector2 tile in MapManager.instance.model.Keys)
		{
			if (tile.x <= startCoordinates.x + movementPoints - (startCoordinates.y - tile.y) &&
				tile.x <= startCoordinates.x + movementPoints - (tile.y - startCoordinates.y) &&
				tile.x - startCoordinates.x >= 0) {
				tiles.Add(tile);
			}
			if (tile.x >= startCoordinates.x - movementPoints - (startCoordinates.y - tile.y) &&
				tile.x >= startCoordinates.x - movementPoints - (tile.y - startCoordinates.y) &&
				tile.x - startCoordinates.x < 0) {
				tiles.Add(tile);
			}
		}

		return tiles;
	}

}

