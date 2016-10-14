using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DetailPopup : MonoBehaviour
{
	/*
	 * Fill with player character data
	 */
	public void FillPlayerCharacterData(CharacterData data)
	{
		transform.FindChild("Sprite").gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Characters/" + data.name) as Sprite;
		GameObject.Find("Name").gameObject.GetComponent<Text>().text = data.name;
		GameObject.Find("Type").gameObject.GetComponent<Text>().text = data.type;
		GameObject.Find("Level").gameObject.GetComponent<Text>().text = data.level.ToString();
		GameObject.Find("HP").gameObject.GetComponent<Text>().text = data.hp.ToString();
		GameObject.Find("MP").gameObject.GetComponent<Text>().text = data.mp.ToString();
		GameObject.Find("Str").gameObject.GetComponent<Text>().text = data.str.ToString();
		GameObject.Find("Def").gameObject.GetComponent<Text>().text = data.def.ToString();
		GameObject.Find("Mag").gameObject.GetComponent<Text>().text = data.mag.ToString();
		GameObject.Find("Res").gameObject.GetComponent<Text>().text = data.res.ToString();
	}

	/*
	 * Fill with enemy character data
	 */
	public void FillEnemyCharacterData(CharacterData data)
	{
		transform.FindChild("Sprite").gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Characters/" + data.name + "_" + data.type) as Sprite;
		GameObject.Find("Name").gameObject.GetComponent<Text>().text = data.name;
		GameObject.Find("Type").gameObject.GetComponent<Text>().text = data.type;
		GameObject.Find("Level").gameObject.GetComponent<Text>().text = data.level.ToString();
		GameObject.Find("HP").gameObject.GetComponent<Text>().text = data.hp.ToString();
		GameObject.Find("MP").gameObject.GetComponent<Text>().text = data.mp.ToString();
		GameObject.Find("Str").gameObject.GetComponent<Text>().text = data.str.ToString();
		GameObject.Find("Def").gameObject.GetComponent<Text>().text = data.def.ToString();
		GameObject.Find("Mag").gameObject.GetComponent<Text>().text = data.mag.ToString();
		GameObject.Find("Res").gameObject.GetComponent<Text>().text = data.res.ToString();
	}

	/*
	 * Close popup
	 */
	public void Close()
	{
		HUDManager.instance.DisplayLastSideButtons();
		Destroy(gameObject);
	}

}

