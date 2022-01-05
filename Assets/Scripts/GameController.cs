using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public float initialVelocity = 0.08f;
	public float distanceFromObjectiveWhenStop = 3.0f;

	GameObject corpse;
	GameObject playerBox;

	RestartComponent[] restartComponents;

	Vector3 velocity;

	// Use this for initialization
	void Start () {
		velocity = new Vector3(initialVelocity, 0.0f, 0.0f);

		corpse = GameObject.Find("Corpse_Body");
		playerBox = GameObject.Find("PlayerBox");

		if (corpse == null)
			Debug.Log("Missing corpse");
		if (playerBox == null)
			Debug.Log("Missing playerBox");

		restartComponents = GameObject.FindObjectsOfType(typeof(RestartComponent)) as RestartComponent[];
		Debug.Log("Found " + restartComponents.Length + " restart components");
	}
	
	// Update is called once per frame
	void Update () {
		if (ComputeDistanceToObjective() > distanceFromObjectiveWhenStop)
			transform.position += velocity;

		if (corpse != null && playerBox != null)
		{
			bool lost = playerBox.transform.position.x - corpse.transform.position.x > 10.0f;
			if (lost)
			{
				Debug.Log("Lost! Dist " + (playerBox.transform.position.x - corpse.transform.position.x));
				GameObject mainMenuObject = GameObject.Find("MainMenu");
				MainMenu mainMenu = mainMenuObject ? mainMenuObject.GetComponent<MainMenu>() : null;
				if (mainMenu != null && !mainMenu.active)
				{
					stats.staticFailures++;
					mainMenu.Activate(true);
				}
			}
		}
	}

	float ComputeDistanceToObjective()
	{
		GameObject objective = GameObject.Find("Objective position");
		if (objective == null)
			return Mathf.Infinity;

		return objective.transform.position.x - corpse.transform.position.x;
	}
}
