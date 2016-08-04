using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
	public static BattleUI instance;

	GameObject topLabel;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		topLabel = GameObject.Find("TopLabel");

		BattleManager.instance.onStartPlayerPhase += StartPlayerPhase;
	}

	void StartPlayerPhase()
	{
		topLabel.GetComponent<Text>().text = "Player Phase";
		topLabel.GetComponent<Animation>().Play();
	}

	void Destroy()
	{
		BattleManager.instance.onStartPlayerPhase -= StartPlayerPhase;
	}
}

