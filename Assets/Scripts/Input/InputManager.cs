using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{

	// Bouton en cours d'interaction  
	public Button button;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		checkTouchEvent();
	}

	// Détecte les inputs tactiles 
	private void checkTouchEvent() {

		// Touch 
		if (Input.touchCount > 0) {
			// Début du touch 
			if (Input.GetTouch(0).phase == TouchPhase.Began) {
				beganTouchEvent(Input.GetTouch(0).position);
			}
			// Fin du touch 
			else if (Input.GetTouch(0).phase == TouchPhase.Ended) {
				endedTouchEvent();
			}
		}
		// Souris
		else if (Input.GetMouseButtonDown(0)) {
			beganTouchEvent(Input.mousePosition);
		}
		else if (Input.GetMouseButtonUp(0)) {
			endedTouchEvent();
		}
	}
	
	// Début d'input 
	private void beganTouchEvent(Vector3 touchPosition) {
		
		Vector3 screenPoint = Camera.main.ScreenToWorldPoint(touchPosition);
		
		Vector2 touchPos = new Vector2(screenPoint.x, screenPoint.y);
		Collider2D hit = Physics2D.OverlapPoint(touchPos);
		
		if (hit) {

			if (GameObject.Find(hit.gameObject.name)) {
				this.button = GameObject.Find(hit.gameObject.name).GetComponent<Button>();
				button.pressed();
			}
		}
	}
	
	// Fin d'input 
	private void endedTouchEvent() {

		button.released();
	}
}
