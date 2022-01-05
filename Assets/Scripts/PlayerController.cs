using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float jumpVelocity = 10.0f;
	public float gravityVelocity = 5.0f;

	Vector3 velocity;

	bool m_preparingJump;
	Vector3 m_nextJumpForce;
	float m_nextJumpTimeRemaining = 0.0f;

	Collider2D m_collider;

	// Use this for initialization
	void Start () {
		m_collider = GetComponent<Collider2D>();

		velocity = new Vector3(0.0f, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (m_preparingJump)
		{
			m_nextJumpTimeRemaining -= Time.deltaTime;
			if (m_nextJumpTimeRemaining <= 0.0f)
			{
				Jump();
				return;
			}
		}

		if (Input.GetKeyDown("space"))
		{
			Vector3 jumpForce = Vector3.up * jumpVelocity;
			RequestJump(jumpForce, 0.0f);
		}

		if (IsTouchingTerrain())
		{
			//Debug.Log("Landed");
			if (velocity.y < 0.0f)
				velocity.Set(velocity.x, 0.0f, velocity.z);

			// try to push them out of the ground
			velocity += new Vector3(0.0f, 0.005f, 0.0f);
		}
		else
		{
			//Debug.Log("Gravity");
			velocity += Vector3.down * gravityVelocity;
		}

		transform.position += velocity;
	}

	void OnTriggerEnter2D(Collider2D _other)
	{
		//Debug.Log("Player Entered Trigger");
		ObstacleController obstacle = _other.GetComponent<ObstacleController>();
		if (obstacle != null)
		{
			//Debug.Log("Player got obstacle trigger");

			float jumpSuccessRatio = Random.Range(obstacle.GetJumpSuccessRatioMin(), obstacle.GetJumpSuccessRatioMax());
			float timeBeforeJump = Random.Range(obstacle.GetTimeBeforeJumpMin(), obstacle.GetTimeBeforeJumpMax());

			Vector3 jumpForce = Vector3.up * jumpVelocity * jumpSuccessRatio;
			RequestJump(jumpForce, timeBeforeJump);

			return;
		}

		if (_other.gameObject.layer == 8)
		{
			//Debug.Log("Player collided with terrain");
		}
	}

	void RequestJump(Vector3 _jumpForce, float _timeBeforeJump)
	{
		Debug.Log("Request Jump");

		m_nextJumpForce = _jumpForce;

		if (_timeBeforeJump > 0.0f)
		{
			m_preparingJump = true;
			m_nextJumpTimeRemaining = _timeBeforeJump;
		}
		else
		{
			Jump();
		}
	}

	void Jump()
	{
		velocity = Vector3.zero;
		velocity += m_nextJumpForce;
		m_preparingJump = false;
	}
		
	bool IsTouchingTerrain()
	{
		int layerMask = 1 << LayerMask.NameToLayer("Terrain");
		return m_collider.IsTouchingLayers(layerMask);
	}
}
