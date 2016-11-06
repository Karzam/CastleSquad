using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Player characters class
 */
public class PlayerCharacter : Character
{
	public static List<GameObject> playerList = new List<GameObject>();

	// Drag state update
	Coroutine dragUpdate;


	// Use this for initialization
	public override void Initialize(string name, Vector2 startCoordinates)
	{
		base.Initialize(name, startCoordinates);

		gameObject.layer = Layer.PLAYER_CHARACTER;
		playerList.Add(gameObject);
		data = DataParser.GetPlayerCharacterData(name);
		SetSprite();
		SetSkills();
	}

	/*
	 * Set skills of the character
	 */
	void SetSkills()
	{
		List<string> skills = new List<string>(new string[] {data.skl_1, data.skl_2, data.skl_3, data.skl_4});

		for (int i = 0; i < 4; i++)
		{
			var skill = new Skill();
			skill.data = DataParser.GetSkillData(skills[i]);
			skillsList.Add(skill);
		}
	}

	/*
	 * Assign sprite to the character
	 */
	void SetSprite()
	{
		sprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Characters/" + data.name);
	}

	public override void SetIdleState()
	{
		base.SetIdleState();
		HUDManager.instance.HideCharacterBottom();
		HUDManager.instance.HideSideButtons();
		HUDManager.instance.HideValidateSkillButtons();
		HUDManager.instance.HideSkillsBar();
		TileManager.instance.RemoveTiles();
	}

	public override void SetSelectedState()
	{
		base.SetSelectedState();
		if (!moved) TileManager.instance.DisplayTiles(GetDestinationTiles(), HighlightType.Move);
		HUDManager.instance.DisplaySideButtons(transform.position, this, true);
		HUDManager.instance.DisplayCharacterBottom(data, true);
		HUDManager.instance.DisplaySkillsBar(this, data, skillsList);
	}

	public override void SetDraggedState()
	{
		base.SetDraggedState();
		dragUpdate = StartCoroutine(DragUpdate());
		TileManager.instance.DisplayTiles(GetDestinationTiles(), HighlightType.Move);
		HUDManager.instance.HideSideButtons();
	}

	public override void SetDroppedState()
	{
		base.SetDroppedState();
		moved = true;
		sprite.transform.eulerAngles = Vector3.zero;
		coordinates = overflownCoordinates;
		SetPosition(coordinates);
	}

	public override void SetFinishState()
	{
		base.SetFinishState();
		sprite.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
		HUDManager.instance.HideSideButtons();
		HUDManager.instance.HideCharacterBottom();
		HUDManager.instance.HideSkillsBar();
		TileManager.instance.RemoveTiles();
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
					TileManager.instance.RemoveTiles(new List<Vector2>{overflownCoordinates}, HighlightType.Move);
					overflownCoordinates = tileOverflown;
					TileManager.instance.DisplayTiles(new List<Vector2>{overflownCoordinates}, HighlightType.Target);
				}
			}
			else {
				StopCoroutine(dragUpdate);
			}
			yield return null;
		}
	}

}

