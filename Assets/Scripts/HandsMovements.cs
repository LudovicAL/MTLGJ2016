using UnityEngine;
using System.Collections;
using System;

public class HandsMovements : MonoBehaviour {

	public PlayerDriverController playerDriverController;
	public float transformationSpeed;
	public float maxRadiusX;
	public float maxRadiusY;
	public GameObject referenceObject;
	public int joystickNumber;
	public int playerNumber;

	private Vector3 referencePoint;
	float xAxis;
	float yAxis;
	string horizontal;
	string vertical;

	void Start() {
		if (playerNumber == 1)
		{
			horizontal = "Horizontal_R_P" + stats.staticController1;
			vertical = "Vertical_R_P" + stats.staticController1;
		}
		else
		{
			horizontal = "Horizontal_R_P" + stats.staticController2;
			vertical = "Vertical_R_P" + stats.staticController2;
		}
	}

	// Update is called once per frame
	void Update () {

		xAxis = Input.GetAxis (horizontal);

		switch(playerNumber)
		{
		case 1:
			playerDriverController.p1HandsOnCoffinRatio = xAxis;
			break;
		case 2:
			playerDriverController.p2HandsOnCoffinRatio = xAxis;
			break;
		}

		referencePoint = referenceObject.transform.position;

		yAxis = Input.GetAxis (vertical);
		transform.position = referencePoint + (new Vector3 (playerDriverController.totalHandsOnCoffinRatio * maxRadiusX, yAxis * maxRadiusY, 0.0f));
	}
}
