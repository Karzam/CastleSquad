using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillButton : ButtonElement
{
	GameObject character;
	Skill skill;
	Vector2 coordinates;

	bool isSelected;

	public void Initialize(GameObject pCharacter, Skill pSkill, Vector2 pCoordinates)
	{
		character = pCharacter;
		skill = pSkill;
		coordinates = pCoordinates;
	}

	public override void OnMouseDown()
	{
		base.OnMouseDown();

		if (!isSelected)
		{
			DeselectButtons();
			character.GetComponent<PlayerCharacter>().SetSkillCast(skill);
			TileManager.instance.DisplayTiles(GetTargetTiles(), TileManager.Tile.Skill);
			HUDManager.instance.HideSideButtons();
			isSelected = true;
		}
		else
		{
			TileManager.instance.RemoveTiles();
			character.GetComponent<PlayerCharacter>().SetSkillCast(null);
			isSelected = false;
		}
	}

	public override void OnMouseUp()
	{
		base.OnMouseUp();
	}

	/*
	 * Deselect all skills buttons
	 */
	public void DeselectButtons()
	{
		for (int i = 0; i < transform.parent.childCount; i++)
		{
			GameObject child = transform.parent.GetChild(i).gameObject;
			child.GetComponent<SkillButton>().isSelected = false;
		}
	}

	/*
	 * Returns the list of available target tiles
	 */
	List<Vector2> GetTargetTiles()
	{
		List<Vector2> tiles = new List<Vector2>();

		foreach (Vector2 tile in MapManager.instance.model.Keys)
		{
			if (tile.x <= coordinates.x + skill.data.range - (coordinates.y - tile.y) &&
				tile.x <= coordinates.x + skill.data.range - (tile.y - coordinates.y) &&
				tile.x - coordinates.x >= 0) {
				if (skill.data.rangeType == "Cross" || skill.data.rangeType == "Away")
				{
					if ((tile.x == coordinates.x || tile.y == coordinates.y))
					{
						if (skill.data.rangeType == "Away")
						{
							if ((tile.x > coordinates.x + skill.data.rangeMinDist && tile.y == coordinates.y)
								|| (tile.x == coordinates.x && tile.y > coordinates.y + skill.data.rangeMinDist)
								|| (tile.x == coordinates.x && tile.y < coordinates.y - skill.data.rangeMinDist))
							{
								tiles.Add(tile);
							}
						}
						else tiles.Add(tile);
					}
				}
				else tiles.Add(tile);
			}
			if (tile.x >= coordinates.x - skill.data.range - (coordinates.y - tile.y) &&
				tile.x >= coordinates.x - skill.data.range - (tile.y - coordinates.y) &&
				tile.x - coordinates.x < 0) {
				if (skill.data.rangeType == "Cross" || skill.data.rangeType == "Away")
				{
					if ((tile.x == coordinates.x || tile.y == coordinates.y))
					{
						if (skill.data.rangeType == "Away")
						{
							if (tile.x < coordinates.x - skill.data.rangeMinDist)
							{
								tiles.Add(tile);
							}
						}
						else tiles.Add(tile);
					}
				}
				else tiles.Add(tile);
			}
		}

		tiles.Remove(new Vector2(coordinates.x, coordinates.y));

		return tiles;
	}

}

