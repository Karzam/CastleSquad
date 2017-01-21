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
	public CharacterData data;

	// Current model coordinates
	public Vector2 coordinates;

	/**
	 * Current state of the character :
	 * 
	 * Idle      => Beginning of the player phase
	 * Selected  => Player touched the character
	 * Dragged   => Player caught the character
	 * Dropped   => Character just moved
	 * Attacked  => Character just attacked
	 * Targeted  => Targeted with skill
	 * Finished  => Finished playing
	 */
	public enum State {Idle, Selected, Dragged, Dropped, Attacked, Targeted, Finished};

	// Current state
	public State state;

	// Skill selected
	public Skill skillCast;

	// Moved ?
	public bool moved;

	// Sprite child
	public GameObject sprite;

	// Overflown coordinates when dragged
	protected Vector2 overflownCoordinates = Vector2.zero;

	// Positioning on tile
	protected Vector2 tileOffset;

	// Skills list
	protected List<Skill> skillsList = new List<Skill>();


	public virtual void Initialize(string name, Vector2 startCoordinates)
	{
		list.Add(gameObject);
		sprite = transform.FindChild("Sprite").gameObject;
		tileOffset = new Vector2(MapManager.TILE_SIZE / 2, 0);
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
		MapManager.instance.UpdateModel(coordinates, gameObject);
		transform.localPosition = new Vector3(MapManager.instance.GetViewCoordinates(coordinates).x + tileOffset.x,
											  MapManager.instance.GetViewCoordinates(coordinates).y + tileOffset.y, -1);
	}

	/*
	 * Set all characters selected to idle state
	 */
	public static void DeselectAllCharacters()
	{
		foreach (GameObject character in list)
		{
			if (character.GetComponent<Character>().state != State.Finished)
			{
				character.GetComponent<Character>().SetIdleState();
			}
		}
	}

	/*
	 * Handle action when character released
	 */
	public void HandleReleased()
	{
		// If moved on available tile
		TileManager.instance.RemoveTiles();
		if (GetDestinationTiles().Contains(overflownCoordinates)) {
			SetDroppedState();
		}
		else {
			SetIdleState();
			sprite.transform.eulerAngles = Vector3.zero;
			SetPosition(coordinates);
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

		tiles.Remove(new Vector2(coordinates.x, coordinates.y));

		return tiles;
	}

	public virtual void SetIdleState()
	{
		state = State.Idle;
	}

	public virtual void SetSelectedState()
	{
		state = State.Selected;
	}

	public virtual void SetDraggedState()
	{
		state = State.Dragged;
	}

	public virtual void SetDroppedState()
	{
		state = State.Dropped;
	}

	public virtual void SetTargetedState()
	{
		state = State.Targeted;
	}

	public virtual void SetFinishState()
	{
		state = State.Finished;
	}

}
