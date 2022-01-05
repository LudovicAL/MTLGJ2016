using UnityEngine;
using System.Collections;

public class CoffinDriver : MonoBehaviour {

	public GameObject p1Hand;
	public GameObject p2Hand;

	public GameObject p1AnchorPoint;
	public GameObject p2AnchorPoint;

	public GameObject coffinLid;
	public GameObject coffinLidBreakoff;

	float desiredAngle = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 p1DesiredChange = p1Hand.transform.position - p1AnchorPoint.transform.position;
		Vector3 p2DesiredChange = p2Hand.transform.position - p2AnchorPoint.transform.position;

		Vector3 totalDesiredChangeInPosition = (p1DesiredChange + p2DesiredChange) / 2.0f;
		totalDesiredChangeInPosition = new Vector3(Mathf.Clamp (totalDesiredChangeInPosition.x, -1.0f, 1.0f), Mathf.Clamp (totalDesiredChangeInPosition.y, -0.1f, 0.1f), totalDesiredChangeInPosition.z);

		GetComponent<Rigidbody2D> ().MovePosition (transform.position + totalDesiredChangeInPosition);

		Vector3 p1NewAnchorPosition = p1AnchorPoint.transform.position + (p1DesiredChange * 0.5f);
		Vector3 p2NewAnchorPosition = p2AnchorPoint.transform.position + (p2DesiredChange * 0.5f);

		Vector3 angleDirectionVector = p2NewAnchorPosition - p1NewAnchorPosition;
		if (angleDirectionVector.y < 0.0f) {
			float dotOfVectors = Vector3.Dot (Vector3.Normalize (angleDirectionVector), new Vector3 (1.0f, 0.0f, 0.0f));
			desiredAngle = -Mathf.Rad2Deg * Mathf.Acos (dotOfVectors);
		} else if (angleDirectionVector.y > 0.0f) {
			float dotOfVectors = Vector3.Dot (Vector3.Normalize (angleDirectionVector), new Vector3 (1.0f, 0.0f, 0.0f));
			desiredAngle = Mathf.Rad2Deg * Mathf.Acos (dotOfVectors);
		} else {
			desiredAngle = 0.0f;
		}

		GetComponent<Rigidbody2D> ().MoveRotation (desiredAngle);

		UpdateCoffinLidBreakoff();
	}

	void UpdateCoffinLidBreakoff()
	{
		if (coffinLid == null || coffinLidBreakoff == null)
		{
			Debug.Log("Missing coffin lid breakoff objects");
			return;
		}
		
		if (coffinLid.transform.position.x  > coffinLidBreakoff.transform.position.x)
		{
			FixedJoint2D fixedJoint = coffinLid.GetComponent<FixedJoint2D>();
			if (fixedJoint != null && fixedJoint.enabled)
			{
				fixedJoint.enabled = false;
				Debug.Log("Disabled coffin lid");
			}
		}
	}
}
