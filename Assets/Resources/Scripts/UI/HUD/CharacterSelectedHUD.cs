using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterSelectedHUD : MonoBehaviour
{
	
	public void SetData(CharacterData pData, bool pIsPlayerCharacter)
	{
		if (pIsPlayerCharacter)
		{
			transform.FindChild("Sprite").gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Characters/" + pData.name) as Sprite;
		}
		else
		{
			transform.FindChild("Sprite").gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Characters/" + pData.name + "_" + pData.type) as Sprite;
		}

		transform.Find("HP").gameObject.GetComponent<Text>().text = pData.hp.ToString();
	}

}

