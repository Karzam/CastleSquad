using UnityEngine;
using System.Collections;

abstract public class AbstractButton : MonoBehaviour
{
	public abstract void OnMouseDown();
	public abstract void OnMouseUp();
}

public class ButtonElement : AbstractButton
{
	// Static button scale
	Vector3 staticScale = new Vector3(1f, 1f, 1f);

	// Pressed button scale
	Vector3 pressedScale = new Vector3(0.8f, 0.8f, 1f);

	public override void OnMouseDown()
	{
		transform.localScale = pressedScale;
	}

	public override void OnMouseUp()
	{
		transform.localScale = staticScale;
	}

}