using UnityEngine;
using System.Collections;

public class Knight : Character
{

	// Use this for initialization
	protected void Start ()
	{
		startCoordinates = new Vector2(2, 3);
		movementPoints = 3;

		base.Start();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

