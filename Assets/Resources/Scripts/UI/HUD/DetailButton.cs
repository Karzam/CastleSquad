using UnityEngine;
using System.Collections;

public class DetailButton : Button
{
	GameObject detailPopup;

	bool isPlayerCharacter;
	CharacterData data;

	public void SetData(CharacterData pData, bool pIsPlayerCharacter)
	{
		data = pData;
		isPlayerCharacter = pIsPlayerCharacter;
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
		if (isPlayerCharacter) detailPopup.GetComponent<DetailPopup>().FillPlayerCharacterData(data);
		else detailPopup.GetComponent<DetailPopup>().FillEnemyCharacterData(data);
		CharacterHUD.instance.Hide();
	}

}

