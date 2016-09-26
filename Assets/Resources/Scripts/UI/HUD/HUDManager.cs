using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manage all HUDs (characters buttons etc...)
 */
public class HUDManager : MonoBehaviour
{
	public static HUDManager instance;

	GameObject characterSelectedHUD;
	GameObject skillsBar;
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

		skillsBar = Instantiate(Resources.Load("Prefabs/UI/Battle/SkillsBar"), parent) as GameObject;
		skillsBar.SetActive(false);

		buttonDetail = Instantiate(Resources.Load("Prefabs/UI/Battle/ButtonDetail"), parent) as GameObject;
		buttonDetail.SetActive(false);

		buttonFinish = Instantiate(Resources.Load("Prefabs/UI/Battle/ButtonFinish"), parent) as GameObject;
		buttonFinish.SetActive(false);
	}

	public void DisplayBottomDetails(GameObject character, bool isPlayerCharacter)
	{
		characterSelectedHUD.GetComponent<CharacterSelectedHUD>().SetData(character.GetComponent<Character>().data, isPlayerCharacter);
		characterSelectedHUD.SetActive(true);
	}

	public void HideBottomDetails()
	{
		if (characterSelectedHUD != null)
		{
			characterSelectedHUD.SetActive(false);
		}
	}

	public void DisplaySideButtons(Vector2 position, GameObject character, bool isPlayerCharacter)
	{
		if (isPlayerCharacter)
		{
			buttonDetail.GetComponent<DetailButton>().SetData(character.GetComponent<Character>().data, isPlayerCharacter);
			buttonDetail.transform.position = new Vector3(position.x - 15, position.y - 10, -2);
			buttonDetail.SetActive(true);

			buttonFinish.GetComponent<FinishButton>().SetCharacter(character);
			buttonFinish.transform.position = new Vector3(position.x + 15, position.y - 10, -2);
			buttonFinish.SetActive(true);
		}
		else
		{
			buttonDetail.GetComponent<DetailButton>().SetData(character.GetComponent<Character>().data, isPlayerCharacter);
			buttonDetail.transform.position = new Vector3(position.x, position.y - 12, -2);
			buttonDetail.SetActive(true);
		}
	}

	public void HideSideButtons()
	{
		if (buttonDetail != null)
		{
			buttonDetail.SetActive(false);
			buttonFinish.SetActive(false);
		}
	}

	public void DisplaySkillsBar(GameObject character, List<Skill> skills)
	{
		skillsBar.GetComponent<SkillsBar>().SetData(character, character.GetComponent<Character>().data, skills);
		skillsBar.SetActive(true);
	}

	public void HideSkillsBar()
	{
		if (skillsBar != null)
		{
			skillsBar.SetActive(false);
		}
	}

}

