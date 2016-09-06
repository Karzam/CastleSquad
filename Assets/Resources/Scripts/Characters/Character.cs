using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Base class for all characters
 * Contains interactions with the player
 */
public class Character : MonoBehaviour
{
	public static List<GameObject> list = new List<GameObject>();

	// Character data
	protected CharacterData data;

	// Current model coordinates
	protected Vector2 coordinates;

	// Sprite child
	protected GameObject sprite;

	/*
	 * Current state of the character :
	 * 
	 * Idle      => Beginning of the player phase
	 * Selected  => Player touched the character
	 * Dragged   => Player caught the character
	 * Dropped   => Character just moved
	 * Attacked  => Character just attacked
	 * Finished  => Finished playing
	 */
	public enum State {Idle, Selected, Dragged, Dropped, Attacked, Finished};

	// Current state
	public State state;

	// Positioning on tile
	protected Vector2 tileOffset;

	void Awake()
	{
		InputManager.instance.onTouchVoid += OnTouchVoid;
	}

	public virtual void Initialize(string name, Vector2 startCoordinates)
	{
		list.Add(gameObject);

		sprite = transform.FindChild("Sprite").gameObject;

		tileOffset = new Vector2(MapManager.TILE_SIZE / 2, 4);

		coordinates = startCoordinates;

		SetPosition(coordinates);
		SetIdleState();
	}

	/*
	 * Change position on the map
	 * with new model coordinates
	 */
	protected void SetPosition(Vector2 position)
	{
		transform.localPosition = new Vector3(MapManager.instance.GetViewCoordinates(coordinates).x + tileOffset.x,
											  MapManager.instance.GetViewCoordinates(coordinates).y + tileOffset.y, -1);
	}

	protected virtual void OnTouchDown()
	{
	}

	protected virtual void OnTouchUp()
	{
	}

	void OnTouchVoid()
	{
		DeselectAllCharacters();
		if (state == State.Selected)
		{
			SetIdleState();
		}
	}

	/*
	 * Set all characters selected to idle state
	 */
	protected void DeselectAllCharacters()
	{
		foreach (GameObject character in list) {
			if (character.GetComponent<Character>().state != State.Finished)
			{
				character.GetComponent<Character>().SetIdleState();
			}
		}
	}

	/*
	 * Returns the list of tiles position available to move
	 */
	protected List<Vector2> GetDestinationTiles()
	{
		List<Vector2> tiles = new List<Vector2>();

		foreach (Vector2 tile in MapManager.instance.model.Keys)
		{
			if (tile.x <= coordinates.x + data.mp - (coordinates.y - tile.y) &&
				tile.x <= coordinates.x + data.mp - (tile.y - coordinates.y) &&
				tile.x - coordinates.x >= 0) {
				tiles.Add(tile);
			}
			if (tile.x >= coordinates.x - data.mp - (coordinates.y - tile.y) &&
				tile.x >= coordinates.x - data.mp - (tile.y - coordinates.y) &&
				tile.x - coordinates.x < 0) {
				tiles.Add(tile);
			}
		}

		return tiles;
	}

	protected virtual void SetIdleState()
	{
		state = State.Idle;
	}

	protected virtual void SetSelectedState(bool displayMovingTiles)
	{
		state = State.Selected;
	}

	protected virtual void SetDraggedState()
	{
		state = State.Dragged;
	}

	protected virtual void SetDroppedState()
	{
		state = State.Dropped;
	}

	public virtual void SetFinishState()
	{
		state = State.Finished;
	}

}

