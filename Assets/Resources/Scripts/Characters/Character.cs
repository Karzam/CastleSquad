using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
	// Character datas (stored here temporarily)
	protected Vector2 startCoordinates;
	protected int movementPoints;

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
		tileOffset = new Vector2(MapManager.TILE_SIZE / 2, 4);
		dragOffset = new Vector2(0, 20);

		state = State.Idle;

		SetPosition(startCoordinates);
	}

	/*
	 * Change position on the map
	 */
	void SetPosition(Vector2 position)
	{
		transform.localPosition = new Vector3(MapManager.instance.GetViewCoordinates(startCoordinates).x + tileOffset.x,
											  MapManager.instance.GetViewCoordinates(startCoordinates).y + tileOffset.y, -1);
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
		if (state == State.Dragged)
		{
			state = State.Idle;
			MapManager.instance.DisableTilesHighlight();
		}
	}

	/*
	 * Update dragged character position and animation
	 */
	IEnumerator DragUpdate()
	{
		int direction = 1;
		Vector2 currentTile = startCoordinates;
		Transform dragPoint = transform.FindChild("DragPoint").transform;

		for(;;)
		{
			if (state == State.Dragged) {
				transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
												 Camera.main.ScreenToWorldPoint(Input.mousePosition).y - dragOffset.y, -1);
				// Swing animation
				float angle = transform.FindChild("Sprite").localEulerAngles.z;
				angle = (angle > 180) ? angle - 360 : angle;
				if (angle > 14) {
					direction = -1;
				}
				else if (angle < -14) {
					direction = 1;
				}
				transform.FindChild("Sprite").RotateAround(dragPoint.position, Vector3.forward, Time.deltaTime * 40 * direction);
				// Highlight current tile
				//Vector2 tileOverflown = MapManager.instance.GetModelCoordinates(
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

