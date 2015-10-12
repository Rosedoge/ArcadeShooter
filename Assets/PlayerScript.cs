using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
	public Text AmmoTxt;
	public Text HealthTxt;
	public GameObject IceProjectile;
	public AudioClip clipAmb; 
	public AudioClip clipShoot;
	
	private AudioSource audioAmb;
	private AudioSource audioShoot;

	public GameObject GameController;
	bool shooting = false;
	int Ammo = 1;
	int health = 10;
	float AmmoRegen = 2f;
	float SavedTime, TouchTime = 0;
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
	void OnTriggerStay(Collider other) {

		if (other.gameObject.tag == "Enemy") {
			TouchTime = TouchTime + Time.deltaTime;
			Debug.Log(TouchTime);
			switch((int)TouchTime){
			case 1:
				health = 9;
				break;
			case 2:
				health = 8;
				break;
			case 3:
				health = 7;
				break;
			case 4:
				health = 6;
				break;
			case 5:
				health = 5;
				break;
			case 6:
				health = 4;
				break;
			case 7:
				health = 3;
				break;
			case 8:
				health = 2;
				break;
			case 9:
				health = 1;
				break;
			case 10:
				health = 0;
				break;
			}
			if(TouchTime > 10.0f){

				GameController.gameObject.GetComponent<GameScript>().EndGame();
			}
		}

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
		string ammotemp = "Ammo: ";
		string healthtemp = "";
		for (int i = 1; i <= Ammo; i++) {
			ammotemp = ammotemp + "█";
		}
		for (int i = 1; i <= health; i++) {
			healthtemp = healthtemp + "█";
		}
		AmmoTxt.text= ammotemp;
		HealthTxt.text= healthtemp;

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
