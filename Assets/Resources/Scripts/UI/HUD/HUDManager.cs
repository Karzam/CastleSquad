using UnityEngine;
using System.Collections;

/**
 * Manage all HUDs (characters buttons etc...)
 */
public class HUDManager : MonoBehaviour
{
	public static HUDManager instance;

	GameObject characterSelectedHUD;
	GameObject buttonDetail;
	GameObject buttonFinish;

	bool isPlayerCharacter;


	void Awake()
	{
		instance = this;
	}

	public void Start()
	{
		Transform parent = GameObject.Find("UI").transform;

		characterSelectedHUD = Instantiate(Resources.Load("Prefabs/UI/Battle/CharacterSelectedHUD"), parent) as GameObject;
		characterSelectedHUD.SetActive(false);

		buttonDetail = Instantiate(Resources.Load("Prefabs/UI/Battle/ButtonDetail"), parent) as GameObject;
		buttonDetail.SetActive(false);

		buttonFinish = Instantiate(Resources.Load("Prefabs/UI/Battle/ButtonFinish"), parent) as GameObject;
		buttonFinish.SetActive(false);
	}

	public void DisplayCharacterHUD(Vector2 position, GameObject character, CharacterData data, bool isPlayerCharacter)
	{
		characterSelectedHUD.GetComponent<CharacterSelectedHUD>().SetData(data, isPlayerCharacter);
		characterSelectedHUD.SetActive(true);

		if (isPlayerCharacter)
		{
			buttonDetail.GetComponent<DetailButton>().SetData(data, isPlayerCharacter);
			buttonDetail.transform.position = new Vector3(position.x - 15, position.y - 10, -2);
			buttonDetail.SetActive(true);

			buttonFinish.GetComponent<FinishButton>().SetCharacter(character);
			buttonFinish.transform.position = new Vector3(position.x + 15, position.y - 10, -2);
			buttonFinish.SetActive(true);
		}
		else
		{
			buttonDetail.GetComponent<DetailButton>().SetData(data, isPlayerCharacter);
			buttonDetail.transform.position = new Vector3(position.x, position.y - 12, -2);
			buttonDetail.SetActive(true);
		}
	}

	public void HideCharacterHUD()
	{
		if (buttonDetail != null)
		{
			buttonDetail.SetActive(false);
			buttonFinish.SetActive(false);
			characterSelectedHUD.SetActive(false);
		}
	}

}

