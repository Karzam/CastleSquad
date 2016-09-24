using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
	public static InputManager instance;

	public delegate void Event();
	public event Event onTouchVoid;

	Collider2D hit;
	
	void Awake()
	{
		instance = this;
	}

	void Update ()
	{
		// Nowhere touch
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Vector2 touchPos = new Vector2(screenPoint.x, screenPoint.y);
			Collider2D hit = Physics2D.OverlapPoint(touchPos);

			if (hit)
			{
				if (hit.gameObject.GetComponent("ButtonElement") == null)
				{
					onTouchVoid();
				}
			}
			else onTouchVoid();
		}
	}

//	private void MouseEvent()
//	{
//		if (Input.GetMouseButtonDown(0)) {
//			beganTouchEvent(Input.mousePosition);
//		}
//		else if (Input.GetMouseButtonUp(0)) {
//			endedTouchEvent();
//		}
//	}
//	
//	// Input start
//	private void beganTouchEvent(Vector3 touchPosition)
//	{
//		Vector3 screenPoint = Camera.main.ScreenToWorldPoint(touchPosition);
//		
//		Vector2 touchPos = new Vector2(screenPoint.x, screenPoint.y);
//		hit = Physics2D.OverlapPoint(touchPos);
//		
//		if (hit)
//		{
//			if (hit.gameObject.GetComponent<ButtonElement>() != null) {
//				//hit.gameObject.GetComponent<ButtonElement>().OnTouchDown();
//				print("button component");
//			}
//			else print("no button component");
//		}
//		else {
//			onTouchVoid();
//		}
//	}
//	
//	// Input end
//	private void endedTouchEvent()
//	{
//		if (hit != null)
//		{
//			//if (hit.gameObject.GetComponent<ButtonElement>() != null) hit.gameObject.GetComponent<ButtonElement>().OnTouchUp();
//			hit = null;
//		}
//	}
}
