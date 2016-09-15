using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class SkillData
{
	/*
	 * Range types => Full, Cross, Away
 	 * Zone types => Cross, Square, Line
	 */

	public string name;
	public int duration;
	public int cooldown;
	public string rangeType;
	public int rangeMinDist;
	public int range;
	public string zoneType;
	public int zoneRange;
	public int damage;
	public int heal;
	public int durationDamage;
	public int durationHeal;
	public int mpDecrease;
	public int strDecrease;
	public int defDecrease;
	public int magDecrease;
	public int resDecrease;
	public int mpIncrease;
	public int strIncrease;
	public int defIncrease;
	public int magIncrease;
	public int resIncrease;
}