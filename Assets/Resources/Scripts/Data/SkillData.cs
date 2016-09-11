using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class SkillData
{
	/*
	 * Range types :
	 * - Full, Cross, Away
	 * 
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
	public int durationDamage;
	public int durationHeal;
}