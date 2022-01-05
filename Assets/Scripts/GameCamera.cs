using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	public float maxDistFromPlayer = 2.0f;

	public GameObject player1;
	public GameObject player2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (player1 == null || player2 == null)
			return;

		// clamp position to always see the players
		float lowestYPos = Mathf.Min(player1.transform.position.y, player2.transform.position.y);

		if (transform.position.y > lowestYPos + maxDistFromPlayer)
		{
			transform.position = new Vector3(transform.position.x, lowestYPos + maxDistFromPlayer, transform.position.z);
		}

		// Debug
		//float minLimitY = lowestYPos - maxDistFromPlayer;
		//Debug.DrawLine(new Vector3(transform.position.x - 5.0f, minLimitY, transform.position.z), new Vector3(transform.position.x + 5.0f, minLimitY, transform.position.z));
	}
}
