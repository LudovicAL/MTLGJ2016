using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

	public AudioClip calmAudioClip;
	public AudioClip stressAudioClip;
	public bool paused;

	private MainMenu mainMenu;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		GameObject mainMenuObject = GameObject.Find("MainMenu");
		if (mainMenuObject != null)
			mainMenu = mainMenuObject.GetComponent<MainMenu>();
		
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (mainMenu == null)
			return;
		
		if (mainMenu.active != paused) {
			paused = mainMenu.active;
			if (paused) {
				audioSource.clip = calmAudioClip;
				audioSource.Play ();
			} else {
				audioSource.clip = stressAudioClip;
				audioSource.Play ();
			}
		}
	}
}
