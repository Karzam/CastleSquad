using UnityEngine;
using System.Collections;

public class CloseButton : ButtonElement
{

	public override void OnMouseDown()
	{
		base.OnMouseDown();
	}

	public override void OnMouseUp()
	{
		base.OnMouseUp();

		transform.parent.GetComponent<DetailPopup>().Close();
	}

}

