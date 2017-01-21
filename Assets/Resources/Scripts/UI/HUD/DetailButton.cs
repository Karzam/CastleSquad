using UnityEngine;
using System.Collections;

public class DetailButton : ButtonElement
{
	GameObject detailPopup;

	bool isPlayerCharacter;
	CharacterData data;

	public void SetData(CharacterData pData, bool pIsPlayerCharacter)
	{
		data = pData;
		isPlayerCharacter = pIsPlayerCharacter;
	}

	public override void OnMouseDown()
	{
		base.OnMouseDown();
	}

	public override void OnMouseUp()
	{
		base.OnMouseUp();

		Transform parent = GameObject.Find("UI").transform;
		detailPopup = Instantiate<Object>(Resources.Load("Prefabs/UI/Popup/PopupDetail")) as GameObject;
        detailPopup.transform.parent = parent;
		HUDManager.instance.HideSideButtons();
		if (isPlayerCharacter) detailPopup.GetComponent<DetailPopup>().FillPlayerCharacterData(data);
		else detailPopup.GetComponent<DetailPopup>().FillEnemyCharacterData(data);
	}

}

