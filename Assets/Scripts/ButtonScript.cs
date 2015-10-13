using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Reject(){
		Application.LoadLevel ("SceneTest");
		Debug.Log ("UGH");

	}
	public void Accept(){
		Application.Quit ();

	}
}
