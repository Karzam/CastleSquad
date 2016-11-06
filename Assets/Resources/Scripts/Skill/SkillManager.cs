using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manage skill casting, targeting, and damage computing,
 * with current characters skills
 */
public class SkillManager : MonoBehaviour
{
	public static SkillManager instance;

	// Current character
	Character character;

	// Current character data
	CharacterData data;

	// Current skills list
	List<Skill> skills = new List<Skill>();

	// Current character's skill cast
	Skill skillCast;

	// Skill range tiles
	List<Vector2> skillTiles = new List<Vector2>();

	// Tiles coordinates targeted
	List<Vector2> targets = new List<Vector2>();


	void Awake()
	{
		instance = this;
	}

	public void Initialize()
	{
		SkillBar.instance.updateSkillCast += UpdateSkillCast;
	}

	public void Initialize(Character pCharacter, CharacterData pData, List<Skill> pSkills)
	{
		character = pCharacter;
		data = pData;
		skills = pSkills;
	}

	/*
	 * Trigger skill
	 */
	public void TriggerSkill()
	{
		// Damage animation
		AnimationManager.instance.PlayDamageAnimation(MapManager.instance.GetObjectWithModelCoordinates(targets[0]).GetComponent<Character>().sprite);
	}

	/*
	 * Return true if one character is casting skill
	 */
	public bool isCastingSkill()
	{
		return skillCast != null;
	}

	/*
	 * Character selected as a target
	 */
	public void HandleCharacterSelected(Character target)
	{
		// Check if character is already selected
		if (targets.Contains(target.coordinates))
		{
			// Deselect
			HUDManager.instance.HideValidateSkillButtons();
			TileManager.instance.RemoveTiles(targets);
			TileManager.instance.DisplayTiles(GetTargetTiles(), HighlightType.Skill);
			targets.Clear();
		}
		else
		{
			// Check if character is inside skill range
			if (skillTiles.Contains(target.coordinates))
			{
				targets.Add(target.coordinates);

				// Get other targets if skill zone
				if (skillCast.data.zoneType != "")
				{
					List<Vector2> zoneTiles = GetZoneTiles();
					foreach (Vector2 tile in zoneTiles)
					{
						targets.Add(tile);
					}
				}

				HUDManager.instance.DisplayValidateSkillButton(character.transform.position);
				TileManager.instance.DisplayTiles(targets, HighlightType.Target);
			}
		}
	}

	/*
	 * Update skill cast
	 */
	void UpdateSkillCast(Skill skill)
	{
		if (skill != null) {
			skillCast = skill;
			skillTiles = GetTargetTiles();
			TileManager.instance.DisplayTiles(GetTargetTiles(), HighlightType.Skill);
		}
		else {
			skillCast = null;
			skillTiles.Clear();
			TileManager.instance.RemoveTiles();
		}
	}

	/*
	 * Returns the list of available target tiles
	 */
	List<Vector2> GetTargetTiles()
	{
		List<Vector2> tiles = new List<Vector2>();

		Vector2 coordinates = character.coordinates;

		foreach (Vector2 tile in MapManager.instance.model.Keys)
		{
			if (tile.x <= coordinates.x + skillCast.data.range - (coordinates.y - tile.y) &&
				tile.x <= coordinates.x + skillCast.data.range - (tile.y - coordinates.y) &&
				tile.x - coordinates.x >= 0) {
				if (skillCast.data.rangeType == "Cross" || skillCast.data.rangeType == "Away")
				{
					if ((tile.x == coordinates.x || tile.y == coordinates.y))
					{
						if (skillCast.data.rangeType == "Away")
						{
							if ((tile.x > coordinates.x + skillCast.data.rangeMinDist && tile.y == coordinates.y)
								|| (tile.x == coordinates.x && tile.y > coordinates.y + skillCast.data.rangeMinDist)
								|| (tile.x == coordinates.x && tile.y < coordinates.y - skillCast.data.rangeMinDist))
							{
								tiles.Add(tile);
							}
						}
						else tiles.Add(tile);
					}
				}
				else tiles.Add(tile);
			}
			if (tile.x >= coordinates.x - skillCast.data.range - (coordinates.y - tile.y) &&
				tile.x >= coordinates.x - skillCast.data.range - (tile.y - coordinates.y) &&
				tile.x - coordinates.x < 0) {
				if (skillCast.data.rangeType == "Cross" || skillCast.data.rangeType == "Away")
				{
					if ((tile.x == coordinates.x || tile.y == coordinates.y))
					{
						if (skillCast.data.rangeType == "Away")
						{
							if (tile.x < coordinates.x - skillCast.data.rangeMinDist)
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

	/*
	 * Return the list of skill's zone tiles
	 */
	List<Vector2> GetZoneTiles()
	{
		List<Vector2> tiles = new List<Vector2>();

		Vector2 coordinates = targets[0];

		foreach (Vector2 tile in MapManager.instance.model.Keys)
		{
			if (tile.x <= coordinates.x + skillCast.data.zoneRange && tile.y <= coordinates.y + skillCast.data.zoneRange &&
				tile.x <= coordinates.x + skillCast.data.zoneRange && tile.y >= coordinates.y - skillCast.data.zoneRange &&
				tile.x - coordinates.x >= 0) {
				if (skillCast.data.zoneType == "Cross")
				{
					if (tile.x <= coordinates.x + skillCast.data.zoneRange - (coordinates.y - tile.y)
						&& tile.x <= coordinates.x + skillCast.data.zoneRange - (tile.y - coordinates.y)) {
						tiles.Add(tile);
					}
				}
				else {
					tiles.Add(tile);
				}
			}
			if (tile.x >= coordinates.x - skillCast.data.zoneRange && tile.y <= coordinates.y + skillCast.data.zoneRange &&
				tile.x >= coordinates.x - skillCast.data.zoneRange && tile.y >= coordinates.y - skillCast.data.zoneRange &&
				tile.x - coordinates.x < 0) {
				if (skillCast.data.zoneType == "Cross")
				{
					if (tile.x >= coordinates.x - skillCast.data.zoneRange - (coordinates.y - tile.y)
						&& tile.x >= coordinates.x - skillCast.data.zoneRange - (tile.y - coordinates.y)) {
						tiles.Add(tile);
					}
				}
				else {
					tiles.Add(tile);
				}
			}
		}
		
		return tiles;
	}

}

