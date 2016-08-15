using UnityEngine;
using System.Collections;

public class DetailButton : Button
{
	GameObject detailPopup;

	CharacterData data;

	public void SetData(CharacterData pData)
	{
		data = pData;
	}

	public void OnTouchDown()
	{
		base.OnTouchDown();
	}

	public void OnTouchUp()
	{
		base.OnTouchUp();

		Transform parent = GameObject.Find("UI").transform;
		detailPopup = Instantiate(Resources.Load("Prefabs/UI/Battle/PopupDetail"), parent) as GameObject;
		detailPopup.GetComponent<DetailPopup>().Fill(data);
		CharacterHUD.instance.Hide();
	}

}

