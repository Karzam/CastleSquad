using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
	// Interacted object
	GameObject interacted;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		TouchEvent();
		MouseEvent();
	}

	// Check for touch input
	private void TouchEvent() {

		// Touch 
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
		if (Input.GetMouseButtonDown(0)) {
			beganTouchEvent(Input.mousePosition);
		}
		else if (Input.GetMouseButtonUp(0)) {
			endedTouchEvent();
		}
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
	}
	
	// Input end
	private void endedTouchEvent()
	{
		interacted.gameObject.BroadcastMessage("OnTouchUp");
		interacted = null;
	}
}
