using UnityEngine;
using System.Collections;

/**
 * Contains all animations methods
 */
public class AnimationManager : MonoBehaviour
{
	public static AnimationManager instance;


	void Awake()
	{
		instance = this;
	}

	/*
	 * Character damage animation
	 */
	public void PlayDamageAnimation(GameObject obj)
	{
		StartCoroutine(DamageAnimation(obj));
	}

	private IEnumerator DamageAnimation(GameObject obj)
	{
		while (obj.GetComponent<SpriteRenderer>().color.a != 0.2f)
		{
			obj.GetComponent<SpriteRenderer>().color = Color.Lerp(obj.GetComponent<SpriteRenderer>().color, new Color(1, 1, 1, 0.2f), Mathf.PingPong(Time.time, 1.0f));
			yield return null;
		}

		while (obj.GetComponent<SpriteRenderer>().color.a != 1.0f)
		{
			obj.GetComponent<SpriteRenderer>().color = Color.Lerp(obj.GetComponent<SpriteRenderer>().color, new Color(1, 1, 1, 1f), Mathf.PingPong(Time.time, 1.0f));
			yield return null;
		}
	}

}

