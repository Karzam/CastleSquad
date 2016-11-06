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

		characterBottom = Instantiate(Resources.Load("Prefabs/UI/HUD/CharacterSelectedHUD"), parent) as GameObject;
		characterBottom.SetActive(false);

		skillsBar = Instantiate(Resources.Load("Prefabs/UI/HUD/SkillBar"), parent) as GameObject;
		skillsBar.SetActive(false);

		buttonDetail = Instantiate(Resources.Load("Prefabs/UI/HUD/ButtonDetail"), parent) as GameObject;
		buttonDetail.SetActive(false);

		buttonFinish = Instantiate(Resources.Load("Prefabs/UI/HUD/ButtonFinish"), parent) as GameObject;
		buttonFinish.SetActive(false);

		buttonValidateSkill = Instantiate(Resources.Load("Prefabs/UI/HUD/ButtonValidateSkill"), parent) as GameObject;
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
		topLabel.GetComponent<Text>().text = "Player Phase";
		topLabel.GetComponent<Animation>().Play();
	}

	/*
	 * Update top label
	 */
	void StartEnemyPhase()
	{
		topLabel.GetComponent<Text>().text = "Enemy Phase";
		topLabel.GetComponent<Animation>().Play();
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

