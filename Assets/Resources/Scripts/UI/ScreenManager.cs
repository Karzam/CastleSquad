using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour {

	// Use this for initialization
	protected virtual void Start () {

		// Démarrage sur l'écran titre 
		gameObject.AddComponent<TitleScreen>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}
}
