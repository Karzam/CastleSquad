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
		BattleManager.instance.onStartPlayerPhase += StartPlayerPhase;
		BattleManager.instance.onStartEnemyPhase += StartEnemyPhase;
	}
		
	public void Start ()
	{
		topLabel = GameObject.Find("TopLabel");
	}

	void StartPlayerPhase()
	{
		topLabel.GetComponent<Text>().text = "Player Phase";
		topLabel.GetComponent<Animation>().Play();
	}

	void StartEnemyPhase()
	{
		topLabel.GetComponent<Text>().text = "Enemy Phase";
		topLabel.GetComponent<Animation>().Play();
	}

	void Destroy()
	{
		BattleManager.instance.onStartPlayerPhase -= StartPlayerPhase;
		BattleManager.instance.onStartEnemyPhase -= StartEnemyPhase;
	}
}

