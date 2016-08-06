﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Base class for all the characters
 * Contains interactions with the player
 */
public class Character : MonoBehaviour
{
	// Character datas (stored here temporarily)
	protected Vector2 startCoordinates;
	protected int movementPoints;

	// Current model coordinates
	Vector2 coordinates;

	// Overflown coordinates when dragged
	Vector2 overflownCoordinates = Vector2.zero;

	// Sprite child
	GameObject sprite;

	/*
	 * Current state of the character :
	 * Idle    => Beginning of the player phase
	 * Dragged => Player caught the character
	 * Dropped => Character moved
	 */
	enum State {Idle, Dragged, Dropped};
	State state;

	// Positioning on tile
	Vector2 tileOffset;

	// Drag offset
	Vector2 dragOffset;

	// State update
	Coroutine dragUpdate;

	protected void Start ()
	{
		sprite = transform.FindChild("Sprite").gameObject;

		tileOffset = new Vector2(MapManager.TILE_SIZE / 2, 4);
		dragOffset = new Vector2(0, 40);

		state = State.Idle;

		coordinates = startCoordinates;
		SetPosition(coordinates);
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
			state = State.Dragged;
			dragUpdate = StartCoroutine(DragUpdate());
			MapManager.instance.EnableTilesHighlight(GetMovingTiles());
		}
	}

	public void OnTouchUp()
	{
		// Releasing the character
		if (state == State.Dragged)
		{
			// If moved on available tile
			MapManager.instance.DisableTilesHighlight();
			if (GetMovingTiles().Contains(overflownCoordinates)) {
				state = State.Dropped;
				sprite.transform.eulerAngles = Vector3.zero;
				coordinates = overflownCoordinates;
				SetPosition(coordinates);
			}
			// Else set to the past position
			else {
				state = State.Idle;
				sprite.transform.eulerAngles = Vector3.zero;
				SetPosition(coordinates);
			}
		}
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
												 Camera.main.ScreenToWorldPoint(Input.mousePosition).y - dragOffset.y, -1);
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
				Vector2 tileOverflown = MapManager.instance.GetModelCoordinates(transform.localPosition + new Vector3(0, -spriteHeight / 4, 0));
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

