using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillButton : ButtonElement
{
	SkillData data;
	Vector2 coordinates;

	bool isSelected;

	public void Initialize(SkillData pData, Vector2 pCoordinates)
	{
		data = pData;
		coordinates = pCoordinates;
	}

	public override void OnMouseDown()
	{
		base.OnMouseDown();

		if (!isSelected)
		{
			DeselectButtons();
			isSelected = true;
			HUDManager.instance.HideCharacterHUD();
			MapManager.instance.DisableTilesHighlight();
			MapManager.instance.EnableTilesHighlight(GetTargetTiles(), true);
		}
		else
		{
			isSelected = false;
			MapManager.instance.DisableTilesHighlight();
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
			if (tile.x <= coordinates.x + data.range - (coordinates.y - tile.y) &&
				tile.x <= coordinates.x + data.range - (tile.y - coordinates.y) &&
				tile.x - coordinates.x >= 0) {
				if (data.rangeType == "Cross" || data.rangeType == "Away")
				{
					if ((tile.x == coordinates.x || tile.y == coordinates.y))
					{
						if (data.rangeType == "Away")
						{
							if ((tile.x > coordinates.x + data.rangeMinDist && tile.y == coordinates.y)
								|| (tile.x == coordinates.x && tile.y > coordinates.y + data.rangeMinDist)
								|| (tile.x == coordinates.x && tile.y < coordinates.y - data.rangeMinDist))
							{
								tiles.Add(tile);
							}
						}
						else tiles.Add(tile);
					}
				}
				else tiles.Add(tile);
			}
			if (tile.x >= coordinates.x - data.range - (coordinates.y - tile.y) &&
				tile.x >= coordinates.x - data.range - (tile.y - coordinates.y) &&
				tile.x - coordinates.x < 0) {
				if (data.rangeType == "Cross" || data.rangeType == "Away")
				{
					if ((tile.x == coordinates.x || tile.y == coordinates.y))
					{
						if (data.rangeType == "Away")
						{
							if (tile.x < coordinates.x - data.rangeMinDist)
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

