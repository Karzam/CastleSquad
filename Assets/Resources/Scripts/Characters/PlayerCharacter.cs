using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Player characters class
 */
public class PlayerCharacter : Character
{
	public static List<GameObject> playerList = new List<GameObject>();

	// Overflown coordinates when dragged
	Vector2 overflownCoordinates = Vector2.zero;

	// Moved ?
	bool moved;

	// State update
	Coroutine dragUpdate;


	// Use this for initialization
	public override void Initialize(string name, Vector2 startCoordinates)
	{
		base.Initialize(name, startCoordinates);
		playerList.Add(gameObject);
		data = DataParser.GetPlayerCharacterData(name);
		SetSprite();
	}

	/*
	 * Assign sprite to the character
	 */
	void SetSprite()
	{
		sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Characters/" + data.name);
	}

	protected override void OnTouchDown()
	{
		if (state == State.Idle)
		{
			DeselectAllCharacters();
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
	}

	protected override void OnTouchUp()
	{
		if (state == State.Dragged)
		{
			// If moved on available tile
			MapManager.instance.DisableTilesHighlight();
			if (GetDestinationTiles().Contains(overflownCoordinates)) {
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

	protected override void SetIdleState()
	{
		base.SetIdleState();
		CharacterHUD.instance.Hide();
		MapManager.instance.DisableTilesHighlight();
	}

	protected override void SetSelectedState(bool displayMovingTiles)
	{
		base.SetSelectedState(displayMovingTiles);
		if (displayMovingTiles) MapManager.instance.EnableTilesHighlight(GetDestinationTiles());
		CharacterHUD.instance.Display(transform.position, gameObject, data, true);
	}

	protected override void SetDraggedState()
	{
		base.SetDraggedState();
		dragUpdate = StartCoroutine(DragUpdate());
		MapManager.instance.EnableTilesHighlight(GetDestinationTiles());
		CharacterHUD.instance.Hide();
	}

	protected override void SetDroppedState()
	{
		base.SetDroppedState();
	}

	public override void SetFinishState()
	{
		base.SetFinishState();
		sprite.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
		CharacterHUD.instance.Hide();
		MapManager.instance.DisableTilesHighlight();
		CheckEndPhase();
	}

	/*
	 * Check if all characters have finished
	 */
	void CheckEndPhase()
	{
		foreach(var character in playerList)
		{
			if (character.GetComponent<PlayerCharacter>().state != State.Finished) {
				return;
			}
		}
		CharacterManager.instance.EndPlayerPhase();
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

}

