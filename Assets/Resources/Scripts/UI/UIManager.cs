using UnityEngine;
using System.Collections;

/**
 * Manage screen & popups
 */
public class UIManager : MonoBehaviour
{
	public static UIManager instance;

	void Awake()
	{
		instance = this;
	}

}

