using UnityEngine;
using System.Collections;

public class RestartComponent : MonoBehaviour {

	Vector3 initialPosition;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void ActivateRestart()
	{
		transform.position = initialPosition;
	}
}
