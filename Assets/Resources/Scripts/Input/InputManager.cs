using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
	public static InputManager instance;

	// Interacted object
	GameObject interacted;

	public delegate void Event();
	public event Event onTouchVoid;
	
	void Awake()
	{
		instance = this;
	}

	void Update ()
	{
		TouchEvent();
		MouseEvent();
	}
		
	private void TouchEvent()
	{
		if (Input.touchCount > 0)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Began) {
				beganTouchEvent(Input.GetTouch(0).position);
			}
			else if (Input.GetTouch(0).phase == TouchPhase.Ended) {
				endedTouchEvent();
			}
		}
	}

	private void MouseEvent()
	{
		#if UNITY_EDITOR
			if (Input.GetMouseButtonDown(0)) {
				beganTouchEvent(Input.mousePosition);
			}
			else if (Input.GetMouseButtonUp(0)) {
				endedTouchEvent();
			}
		#endif
	}
	
	// Input start
	private void beganTouchEvent(Vector3 touchPosition) {
		
		Vector3 screenPoint = Camera.main.ScreenToWorldPoint(touchPosition);
		
		Vector2 touchPos = new Vector2(screenPoint.x, screenPoint.y);
		Collider2D hit = Physics2D.OverlapPoint(touchPos);
		
		if (hit)
		{
			interacted = hit.gameObject;
			hit.gameObject.BroadcastMessage("OnTouchDown");
		}
		else {
			onTouchVoid();
		}
	}
	
	// Input end
	private void endedTouchEvent()
	{
		if (interacted != null)
		{
			interacted.gameObject.BroadcastMessage("OnTouchUp");
			interacted = null;
		}
	}
}
