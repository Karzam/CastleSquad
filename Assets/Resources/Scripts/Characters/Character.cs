using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	protected Vector2 startCoordinates;

	enum State {Idle, Dragged, Dropped};
	State state;

	// Positioning on tile
	Vector2 offset;

	// Use this for initialization
	protected void Start ()
	{
		offset = new Vector2(MapManager.TILE_SIZE / 2, 4);

		state = State.Idle;

		SetPosition(startCoordinates);
	}

	void SetPosition(Vector2 position)
	{
		transform.localPosition = new Vector3(MapManager.instance.GetViewCoordinates(startCoordinates).x + offset.x,
											  MapManager.instance.GetViewCoordinates(startCoordinates).y + offset.y, -1);
	}

	public void OnTouchDown()
	{
		if (state == State.Idle)
		{
				
		}
	}

}

