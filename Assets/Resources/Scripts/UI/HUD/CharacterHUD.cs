using UnityEngine;
using System.Collections;

/**
 * Display buttons off character selected
 */
public class CharacterHUD : MonoBehaviour
{
	public static CharacterHUD instance;

	GameObject buttonDetail;
	GameObject buttonFinish;

	void Awake()
	{
		instance = this;
	}

	public void Start()
	{
		Transform parent = GameObject.Find("UI").transform;

		buttonDetail = Instantiate(Resources.Load("Prefabs/UI/Battle/ButtonDetail"), parent) as GameObject;
		buttonDetail.SetActive(false);

		buttonFinish = Instantiate(Resources.Load("Prefabs/UI/Battle/ButtonFinish"), parent) as GameObject;
		buttonFinish.SetActive(false);
	}

	public void Display(Vector2 position, GameObject character, CharacterData data)
	{
		buttonDetail.GetComponent<DetailButton>().SetData(data);
		buttonDetail.transform.position = new Vector3(position.x - 15, position.y - 10, -2);
		buttonDetail.SetActive(true);
		buttonFinish.GetComponent<FinishButton>().SetCharacter(character);
		buttonFinish.transform.position = new Vector3(position.x + 15, position.y - 10, -2);
		buttonFinish.SetActive(true);
	}

	public void Hide()
	{
		if (buttonDetail != null)
		{
			buttonDetail.SetActive(false);
			buttonFinish.SetActive(false);
		}
	}

}

