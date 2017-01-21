using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/**
 * Manage buttons, pop-ups and battle UIs
 */
public class HUDManager : MonoBehaviour
{
	public static HUDManager instance;

	// Phase UI
	GameObject topLabel;

	// Character selected informations
	GameObject characterBottom;

	// Skills bar
	GameObject skillsBar;

	// Character selected buttons
	GameObject buttonDetail;
	GameObject buttonFinish;

	// Validate skill button
	GameObject buttonValidateSkill;


	void Awake()
	{
		instance = this;

		BattleManager.instance.onStartPlayerPhase += StartPlayerPhase;
		BattleManager.instance.onStartEnemyPhase += StartEnemyPhase;
	}

	public void Initialize()
	{
		Transform parent = GameObject.Find("UI").transform;

		topLabel = GameObject.Find("TopLabel");

		characterBottom = Instantiate(Resources.Load<Object>("Prefabs/UI/HUD/CharacterSelectedHUD")) as GameObject;
        characterBottom.transform.parent = parent;
		characterBottom.SetActive(false);

		skillsBar = Instantiate<Object>(Resources.Load("Prefabs/UI/HUD/SkillBar")) as GameObject;
        skillsBar.transform.parent = parent;
        skillsBar.SetActive(false);

		buttonDetail = Instantiate<Object>(Resources.Load("Prefabs/UI/HUD/ButtonDetail")) as GameObject;
        buttonDetail.transform.parent = parent;
        buttonDetail.SetActive(false);

		buttonFinish = Instantiate<Object>(Resources.Load("Prefabs/UI/HUD/ButtonFinish")) as GameObject;
        buttonFinish.transform.parent = parent;
        buttonFinish.SetActive(false);

		buttonValidateSkill = Instantiate<Object>(Resources.Load("Prefabs/UI/HUD/ButtonValidateSkill")) as GameObject;
        buttonValidateSkill.transform.parent = parent;
        buttonValidateSkill.SetActive(false);
	}

	/*
	 * Display and update character sprite and HP
	 */
	public void DisplayCharacterBottom(CharacterData pData, bool pIsPlayerCharacter)
	{
		characterBottom.GetComponent<CharacterBottom>().SetData(pData, pIsPlayerCharacter);
		characterBottom.SetActive(true);
	}

	/*
	 * Hide character sprite and HP
	 */
	public void HideCharacterBottom()
	{
		if (characterBottom != null)
		{
			characterBottom.SetActive(false);
		}
	}

	/*
	 * Update top label
	 */
	void StartPlayerPhase()
	{
		topLabel.GetComponent<Text>().text = "PLAYER PHASE";
	}

	/*
	 * Update top label
	 */
	void StartEnemyPhase()
	{
		topLabel.GetComponent<Text>().text = "ENEMY PHASE";
	}

	/*
	 * Display character selected buttons
	 */
	public void DisplaySideButtons(Vector2 position, Character character, bool isPlayerCharacter)
	{
		if (isPlayerCharacter)
		{
			buttonDetail.GetComponent<DetailButton>().SetData(character.data, isPlayerCharacter);
			buttonDetail.transform.position = new Vector3(position.x - 15, position.y - 10, -2);
			buttonDetail.SetActive(true);

			buttonFinish.GetComponent<FinishButton>().SetCharacter(character);
			buttonFinish.transform.position = new Vector3(position.x + 15, position.y - 10, -2);
			buttonFinish.SetActive(true);
		}
		else
		{
			buttonDetail.GetComponent<DetailButton>().SetData(character.data, isPlayerCharacter);
			buttonDetail.transform.position = new Vector3(position.x, position.y - 12, -2);
			buttonDetail.SetActive(true);
		}
	}

	/*
	 * Re-display last character selected buttons
	 */
	public void DisplayLastSideButtons()
	{
		buttonDetail.SetActive(true);
		buttonFinish.SetActive(true);
	}

	/*
	 * Hide character selected buttons
	 */
	public void HideSideButtons()
	{
		if (buttonDetail != null)
		{
			buttonDetail.SetActive(false);
			buttonFinish.SetActive(false);
		}
	}

	/*
	 * Display skills buttons and initialize Skill Manager
	 */
	public void DisplaySkillsBar(Character pCharacter, CharacterData pData, List<Skill> pSkills)
	{
		SkillManager.instance.Initialize(pCharacter, pData, pSkills);
		skillsBar.GetComponent<SkillBar>().SetData(pSkills);
		skillsBar.SetActive(true);
	}

	/*
	 * Hide skills buttons
	 */
	public void HideSkillsBar()
	{
		if (skillsBar != null)
		{
			skillsBar.SetActive(false);
		}
	}

	/*
	 * Display validate skill button
	 */
	public void DisplayValidateSkillButton(Vector2 position)
	{
		buttonValidateSkill.transform.position = new Vector3(position.x, position.y - 10, -2);
		buttonValidateSkill.SetActive(true);
	}

	/*
	 * Hide validate skill button
	 */
	public void HideValidateSkillButtons()
	{
		buttonValidateSkill.SetActive(false);
	}

}

