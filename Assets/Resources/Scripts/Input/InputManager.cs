using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
	public static InputManager instance;

	public delegate void InputEvent(GameObject hit);
	public event InputEvent onTouchDown;
	public event InputEvent onTouchUp;

	GameObject currentObject;

	
	void Awake()
	{
		instance = this;
	}

	void Update ()
	{
		MouseEvent();
	}

	private void MouseEvent()
	{
		if (Input.GetMouseButtonDown(0)) {
			beganTouchEvent(Input.mousePosition);
		}
		else if (Input.GetMouseButtonUp(0)) {
			endedTouchEvent();
		}
	}
	
	// Input start
	private void beganTouchEvent(Vector3 touchPosition)
	{
		Vector3 screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 touchPos = new Vector2(screenPoint.x, screenPoint.y);
		Collider2D hit = Physics2D.OverlapPoint(touchPos);

		if (hit)
		{
			currentObject = hit.gameObject;
			onTouchDown(hit.gameObject);
		}
		else onTouchDown(null);
	}
	
	// Input end
	private void endedTouchEvent()
	{
		if (currentObject != null)
		{
			onTouchUp(currentObject);
			currentObject = null;
		}
	}
}
