using UnityEngine;
using System.Collections;

public class PlayerDriverController : MonoBehaviour {
	
	public float stickTranslation = 0.02f;
	public float maxDistFromParentBox = 2.0f;
	public int firstPlayerNo;
	public int secondPlayerNo;

	public float p1HandsOnCoffinRatio = 0.0f;
	public float p2HandsOnCoffinRatio = 0.0f;
	public float totalHandsOnCoffinRatio = 0.0f;

	float xAxis_1;
	float xAxis_2;
	string horizontal_P1;
	string horizontal_P2;

	Vector3 velocity;

	// Use this for initialization
	void Start () {
		horizontal_P1 = "HorizontalP" + stats.staticController1;
		horizontal_P2 = "HorizontalP" + stats.staticController2;
	}
	
	// Update is called once per frame
	void Update () {
		totalHandsOnCoffinRatio = (0.5f * p1HandsOnCoffinRatio) + (0.5f * p2HandsOnCoffinRatio);

		xAxis_1 = Input.GetAxis (horizontal_P1);
		xAxis_2 = Input.GetAxis (horizontal_P2);

		transform.position += new Vector3((xAxis_1 + xAxis_2) * stickTranslation, 0.0f, 0.0f);

		// clamp position inside box
		Vector3 parentPosition = transform.parent.transform.position;
		if (transform.position.x < parentPosition.x - maxDistFromParentBox)
		{
			transform.position = new Vector3(parentPosition.x - maxDistFromParentBox, transform.position.y, transform.position.z);
		}
		else if (transform.position.x > parentPosition.x + maxDistFromParentBox)
		{
			transform.position = new Vector3(parentPosition.x + maxDistFromParentBox, transform.position.y, transform.position.z);
		}

		// Debug
		//float minLimitX = parentPosition.x - maxDistFromParentBox;
		//float maxLimitX = parentPosition.x + maxDistFromParentBox;
		//Debug.DrawLine(new Vector3(minLimitX, parentPosition.y + 5.0f, parentPosition.z), new Vector3(minLimitX, parentPosition.y - 5.0f, parentPosition.z));
		//Debug.DrawLine(new Vector3(maxLimitX, parentPosition.y + 5.0f, parentPosition.z), new Vector3(maxLimitX, parentPosition.y - 5.0f, parentPosition.z));
	}
}
