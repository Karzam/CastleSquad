using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{

	public delegate void Callback();
	public Callback callback;

	// Scale bouton statique
	private Vector3 staticScale = new Vector3(1f, 1f, 1f);

	// Scale bouton pressé
	private Vector3 pressedScale = new Vector3(0.9f, 0.9f, 1f);

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	// Pressé 
	public void pressed() {
		transform.localScale = pressedScale;
	}

	// Relaché 
	public void released() {
		transform.localScale = staticScale;
		callbackEvent();
	}

	// Ajoute le callback 
	public void addCallback(Callback method) {
		this.callback = method;
	}

	// Exécute le callback
	public void callbackEvent() {
		callback();
	}

}
