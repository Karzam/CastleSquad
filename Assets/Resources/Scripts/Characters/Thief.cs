using UnityEngine;
using System.Collections;

public class Thief : Character
{

	// Use this for initialization
	protected void Start ()
	{
		startCoordinates = new Vector2(6, 4);
		movementPoints = 4;

		base.Start();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

