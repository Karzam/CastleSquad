using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	enum State {Idle, Dragged, Dropped};
	State state;

	// Use this for initialization
	void Start ()
	{
		state = State.Idle;
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void OnTouchDown()
	{
		if (state == State.Idle)
		{
				
		}
	}

}

