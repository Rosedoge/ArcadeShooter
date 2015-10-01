using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
	public Text AmmoTxt;
	public GameObject IceProjectile;
	public AudioClip clipAmb; 
	public AudioClip clipShoot;
	
	private AudioSource audioAmb;
	private AudioSource audioShoot;
	bool shooting = false;
	int Ammo = 6;
	float AmmoRegen = 2f;
	float SavedTime;
	// Use this for initialization
	void Awake () {
		SavedTime = Time.time;
		audioAmb = AddAudio(clipAmb, true, true, 0.2f);
		audioShoot = AddAudio(clipShoot, false, false, 0.2f);
		audioAmb.Play ();

	}
	public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol) { 
		AudioSource newAudio = this.gameObject.AddComponent <AudioSource> ();
		newAudio.clip = clip; 
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol; 
		return newAudio; 
	}

	
	// Update is called once per frame
	void Update () {

		if (Time.time - SavedTime >= AmmoRegen && Ammo < 10) {
			SavedTime = Time.time;
			Ammo+=1;
		}
		Controls ();
		UpdateUI ();
	}

	void Controls() {
		
		if (Input.GetKeyDown(KeyCode.Mouse0) && shooting == false && Ammo >= 1) {
			audioShoot.Play();
			Shoot ();
			
		}
		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			shooting = false;
		}
	}

	void UpdateUI() {
		string ammotemp = "";
		for (int i = 1; i <= Ammo; i++) {
			ammotemp = ammotemp + "█";
		}

		AmmoTxt.text= ammotemp;

	}
	void Shoot(){

		Ammo -= 1;
		//GameObject iceProj = new GameObject ();
		GameObject iceProj = (GameObject)Instantiate (IceProjectile, this.gameObject.transform.position, this.gameObject.transform.rotation);
		// clone.rigidbody.AddForce(clone.transform.forward * shootForce);

		iceProj.transform.rotation = this.GetComponentInChildren<Transform> ().transform.rotation;
		iceProj.GetComponent<Rigidbody> ().AddForce (iceProj.transform.forward * 500f); //Ice balls are slower
		//iceProj.GetComponent<BulletScript>().type = "Ice";

	}

}
