using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public GameObject canvas;
	public Button buttonResumeGame;
	public Button buttonQuit;
	public Text textTaunt;
	public Text textDistance;
	public Text textAttemps;
	public Text textSuccessRate;
	public float buttonFireRate;
	public bool active;

	private List<Button> buttonList;
	private int optionQuantity;
	private int currentSelection;
	private float nextButtonFire;
	private int modifier;
	private string[] tauntsArray;
	private System.Random rnd;

	public void Start() {
		tauntsArray = new string[] {
			"Poor job there...!",
			"C'mon! Try harder!",
			"Wasn't that littering?!",
			"How do you live with yourself?",
			"One man's failure is another man's failure.",
			"Oh, the humanity.",
			"Horrifying!",
			"What would his family think?",
			"Maybe you're not his friend anymore.",
			"That's no way to treat the dead.",
			"He deserved better.",
			"Hey, that's a human being you know!?",
			"#@?(@#%@",
			"Your lack of coordination is concerning.",
			"You're lucky the dead don't feel pain.",
			"Weekend at Bernie's!",
			"Is it the smell that distubed you?",
			"Somewhat disgraceful, don't you think.",
			"You deserve jailtime for what just happened.",
			"I think you dropped something.",
			"Butterfingers!",
			"You tripped?",
			"You've disapointed me for the last time, admiral.",
			"Carrying dead bodies isn't rocketscience.",
			"What would your mother say!?",
			"You only die once.",
			"God, how embarassing.",
			"Shame.",
			"His ancestors are gonna feel that one.",
			"Good help is so hard to find.",
			"You're supposed to get drunk after the funeral.",
			"Did you even try?",
			"Do you even lift?",
			"Don't restart the game, it would be disrespectful.",
			"No running in the cemetery.",
			"Please exit this game, you've done enough harm.",
			"Making death fun since 2016."
		};
		rnd = new System.Random ();
		currentSelection = 0;
		buttonList = new List<Button>();
		addActiveButtons (ref buttonResumeGame);
		addActiveButtons (ref buttonQuit);
		optionQuantity = buttonList.Count;
		nextButtonFire = 0.0f;
		SetButtonsColors ();
		Activate(active);
	}

	//Ajoute à la liste des boutons seulement les boutons "enabled"
	private void addActiveButtons(ref Button button)  {
		if (button.interactable)  {
			buttonList.Add (button);
		}
	}

	//Appelé lorsqu'un utilisateur clique sur l'un des boutons du menu
	public void MenuButtonClick(int buttonNo) {
		switch (buttonNo) {
			case 0:
				Activate(false);
				//bool restartInputPressed = Input.GetButton("Restart");
				//if (restartInputPressed) {
				SceneManager.LoadScene("AurelieGym", LoadSceneMode.Single);
				//}
				Debug.Log ("Click: Resume game");
				break;
			case 1: 
				Debug.Log ("Click: Quit");
				Application.Quit ();
				break;
		}
	}


	void Update() {
		if (active) { //Si le menu est visible
			modifier = (int)Math.Round(Input.GetAxis ("Vertical"));
			if (modifier != 0 && Time.time > nextButtonFire ) { //Lorsque l'usager parcours les boutons
				if (SetCurrentSelection(modifier))  {
					SetButtonsColors ();
					buttonList [currentSelection].Select ();
					nextButtonFire = Time.time + buttonFireRate;
					Debug.Log ("Current selection:" + currentSelection);
				}
			}
			if (Input.GetKey(KeyCode.UpArrow) && Time.time > nextButtonFire) {
				if (SetCurrentSelection(1))  {
					SetButtonsColors ();
					buttonList [currentSelection].Select ();
					nextButtonFire = Time.time + buttonFireRate;
					Debug.Log ("Current selection:" + currentSelection);
				}
			}
			if (Input.GetKey(KeyCode.DownArrow) && Time.time > nextButtonFire) {
				if (SetCurrentSelection(-1))  {
					SetButtonsColors ();
					buttonList [currentSelection].Select ();
					nextButtonFire = Time.time + buttonFireRate;
					Debug.Log ("Current selection:" + currentSelection);
				}
			}
			if (Input.GetButton("Fire1")) {
				buttonList [currentSelection].onClick.Invoke();
			}
		}
		if (!active && Input.GetButton("Start")) { //Lorsque l'usager pèse Start (sur le XBox controller) ou escape (sur le clavier)
			Activate(true);
		}
	}

	//Active ou désactive le MainMenu
	public void Activate(bool _activate)
	{
		canvas.SetActive (_activate);
		active = _activate;

		if (_activate) {
			nextButtonFire = Time.time + buttonFireRate;
			if (stats.staticFailures + stats.staticSuccesses != 0) { //Si ça n'est pas le premier essai, afficher les stats.
				buttonResumeGame.GetComponentInChildren<Text> ().text = "RESTART";
				textTaunt.text = tauntsArray [rnd.Next (0, tauntsArray.Length)];
				textDistance.text = StatsManager.getInstance ().getDistance () + " meters away from proper burial.";
				int successRate = (stats.staticSuccesses / (stats.staticSuccesses + stats.staticFailures)) * 100;
				textSuccessRate.text = "Success ratio : " + successRate + "%";
				textAttemps.text = (stats.staticSuccesses + stats.staticFailures) + " attemp(s)";
			}
		}
	}

	//Met à jour la couleur du bouton sélectionné
	public void SetButtonsColors(){
		for (int i = 0; i < optionQuantity; i++) {
			buttonList [i].image.color = (i == currentSelection) ? Color.white : Color.grey;
		}
	}

	//Change le bouton actuellement sélectionné
	public bool SetCurrentSelection (int modifier) {
		if (modifier > 0 && currentSelection > 0){
			currentSelection += modifier * -1;
			return true;
		} else if (modifier < 0 && currentSelection < optionQuantity - 1) {
			currentSelection += modifier * -1;
			return true;
		}
		return false;
	}
}
