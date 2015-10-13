using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour {

	public Text tone;
	public Text ttwo;
	public Text tthree;
	public Text tfour;
	float SavedTime;
	float inTime;
	// Use this for initialization
	void Start () {
		SavedTime = Time.time;
		tone.gameObject.GetComponent<CanvasRenderer> ().SetAlpha (0);
		ttwo.gameObject.GetComponent<CanvasRenderer> ().SetAlpha (0);
		tthree.gameObject.GetComponent<CanvasRenderer> ().SetAlpha (0);
		tfour.gameObject.GetComponent<CanvasRenderer> ().SetAlpha (0);
	}
	
	// Update is called once per frame
	void Update () {
		inTime = Time.time;
		if (inTime >= 1) {
			tone.gameObject.GetComponent<CanvasRenderer> ().SetAlpha (100);
		}
		if (inTime >= 6) {
			ttwo.gameObject.GetComponent<CanvasRenderer> ().SetAlpha (100);
		}
		if (inTime >= 11) {
			tthree.gameObject.GetComponent<CanvasRenderer> ().SetAlpha (100);
		}
		if (inTime >= 16) {
			tfour.gameObject.GetComponent<CanvasRenderer> ().SetAlpha (100);
		}
		if (inTime >= 16) {
			Application.LoadLevel ("SceneTest");
		}
	
	}
}
