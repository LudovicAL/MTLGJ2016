using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerDetection : MonoBehaviour {

	public Text titleText;
	public Text player1Text;
	public Text player2Text;
	public Button startButton;

	private bool p1Set;
	private bool p2Set;

	// Use this for initialization
	void Start () {
		p1Set = false;
		p2Set = false;
		startButton.interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Lorsque les deux controlleurs ont été détectés
		if (p1Set && p2Set)  {
			if (stats.staticController1 == stats.staticController2) {
				Debug.Log ("Both controller plugged in the same port? How is that possible?");
			}
			Debug.Log ("Both controllers detected!");
			startButton.interactable = true;
			startButton.Select();
			titleText.text = "DIGNITY";
		} else {
			//Détection du controleur du Player1
			if (!p1Set)  {
				for (int i = 1; i <= 11; i++) {
					if (i != stats.staticController2) {
						if (Input.GetAxis("HorizontalP" + i) != 0 || Input.GetAxis("VerticalP" + i) != 0) {
							stats.staticController1 = i;
							player1Text.text = "Player 1 set! (Joystick " + stats.staticController1 + ")";
							p1Set = true;
							break;
						}
					}
				}
			}
			//Détection du controleur du Player2
			if (!p2Set)  {
				for (int i = 1; i <= 11; i++) {
					if (i != stats.staticController1) {
						if (Input.GetAxis("HorizontalP" + i) != 0 || Input.GetAxis("VerticalP" + i) != 0) {
							stats.staticController2 = i;
							player2Text.text = "Player 2 set! (Joystick " + stats.staticController2 + ")";
							p2Set = true;
							break;
						}
					}
				}
			}
		}
	}

	public void StartButtonClick()  {
		Debug.Log ("Starting game");
		SceneManager.LoadScene("AurelieGym", LoadSceneMode.Single);
	}
}
