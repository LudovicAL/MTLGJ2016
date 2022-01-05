using UnityEngine;
using System.Collections;

public class PersistentInfos : MonoBehaviour {

	private int persistentController1;
	private int persistentController2;
	private int persistentSuccesses = 0;
	private int persistentFailures = 0;

	// Use this for initialization
	void Start () {
		persistentController1 = -1;
		persistentController2 = -1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Do not destroy this game object:
	void Awake() {
		DontDestroyOnLoad(this);
	}

	//Getters and setters
	public static PersistentInfos getInstance() {
		return GameObject.Find("Persistent object").GetComponent<PersistentInfos>();
	}

	public int getPersistentController1() {
		return persistentController1;
	}

	public int getPersistentController2() {
		return persistentController2;
	}

	public int getPersistentSuccesses() {
		return persistentSuccesses;
	}

	public int getPersistentFailures() {
		return persistentFailures;
	}

	public void setPersistentController1(int newValue) {
		persistentController1 = newValue;
	}

	public void setPersistentController2(int newValue) {
		persistentController2 = newValue;
	}

	public void setPersistentSuccesses(int modifier) {
		persistentSuccesses += modifier;
	}

	public void setPersistentFailures(int modifier) {
		persistentFailures += modifier;
	}
}
