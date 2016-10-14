using UnityEngine;
using System.Collections;

/**
 * Manage UI elements
 */
public class UIManager : MonoBehaviour
{
	public static UIManager instance;


	void Awake()
	{
		instance = this;
	}

}

