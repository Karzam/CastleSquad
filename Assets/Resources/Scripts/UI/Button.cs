using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
	// Static button scale
	private Vector3 staticScale = new Vector3(1f, 1f, 1f);

	// Pressed button scale
	private Vector3 pressedScale = new Vector3(0.8f, 0.8f, 1f);

	protected void OnTouchDown()
	{
		transform.localScale = pressedScale;
	}

	protected void OnTouchUp()
	{
		transform.localScale = staticScale;
	}

}
