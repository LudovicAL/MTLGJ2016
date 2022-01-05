using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	public float speedX = -0.01f;

	public float jumpSuccessRatioMin = 0.4f;
	public float jumpSuccessRatioMax = 1.6f;

	public float timeBeforeJumpMin = 0.0f;
	public float timeBeforeJumpMax = 0.25f;

	public bool showDebug = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position += new Vector3(speedX, 0.0f, 0.0f);
	}

	void OnGUI()
	{
		if (showDebug)
		{
			Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position);
			Rect obstacleRect = new Rect(screenSpace.x, screenSpace.y, 200, 200);
			GUI.Label(obstacleRect, "jump [" + jumpSuccessRatioMin * 100.0f + " to " + jumpSuccessRatioMax * 100.0f + "]%\ntime before jump [" + timeBeforeJumpMin + " to " + timeBeforeJumpMax + "]sec");
		}
	}

	void OnTriggerEnter2D(Collider2D _other)
	{
		Debug.Log("Obstacle Entered Trigger");
		if (_other.GetComponent<PlayerController>() != null)
		{
			Debug.Log("Obstacle got player trigger");
		}
	}

	public float GetTimeBeforeJumpMin() { return timeBeforeJumpMin; }
	public float GetTimeBeforeJumpMax() { return timeBeforeJumpMax; }
	public float GetJumpSuccessRatioMin() { return jumpSuccessRatioMin; }
	public float GetJumpSuccessRatioMax() { return jumpSuccessRatioMax; }
}
