using UnityEngine;
using System.Collections;

public class ExplosiveScript : MonoBehaviour {

	float Began;
	// Use this for initialization
	void Awake () {
		Began = Time.time;
	
	
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - Began > 3) {
			//Debug.Log("Boom b-gone");
			Destroy(this.gameObject);

		}
	}
}
