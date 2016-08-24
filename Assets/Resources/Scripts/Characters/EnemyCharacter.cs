using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Enemy characters class
 */
public class EnemyCharacter : Character
{
	public static List<GameObject> enemyList = new List<GameObject>();

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

	public void Move()
	{
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

