﻿using UnityEngine;
using System.Collections;

public class Sorcerer : Character
{

	// Use this for initialization
	protected void Start ()
	{
		startCoordinates = new Vector2(8, 1);
		movementPoints = 3;

		base.Start();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

