using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	// Bouton play  
	private Button play;

	// Bouton de settings 
	private Button settings;

	// Bouton google play  
	private Button googlePlay;

	// Use this for initialization
	void Start () {

		play = GameObject.Find("Play").AddComponent<Button>();
		play.addCallback(playTouched);

		settings = GameObject.Find("Settings").AddComponent<Button>();
		settings.addCallback(settingsTouched);

		googlePlay = GameObject.Find("GooglePlay").AddComponent<Button>();
		googlePlay.addCallback(googlePlayTouched);
	}

	// Update is called once per frame
	void Update () {

	}
	
	private void playTouched() {
	}

	private void settingsTouched() {

	}
	 
	private void googlePlayTouched() {
		
	}

}
