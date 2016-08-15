using UnityEngine;
using System.Collections;

public class CloseButton : Button
{

	public void OnTouchDown()
	{
		base.OnTouchDown();
	}

	public void OnTouchUp()
	{
		base.OnTouchUp();

		transform.parent.GetComponent<DetailPopup>().Close();
	}

}

