using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
	public Text AmmoTxt;
	public Text HealthTxt;
	public GameObject IceProjectile;
	public AudioClip clipAmb; 
	public AudioClip clipAmb2;
	public AudioClip clipAmb3;
	public AudioClip clipAmb4; 


	public AudioClip clipShoot;
	
	private AudioSource audioAmb;
	private AudioSource audioAmb2;
	private AudioSource audioAmb3;
	private AudioSource audioAmb4;

	private AudioSource audioShoot;

	public GameObject GameController;
	public GameObject GunArm;
	GameObject GunEnd, NormalPos, ADSPos;
	bool shooting = false, sonarEnabled = false;
	bool sonarSwap = false, Aiming = false;
	int Ammo = 600;
	int health = 10;
	float AmmoRegen = 2f;
	float SavedTime, TouchTime = 0;
	bool Swapped2 = false, Swapped3 = false, Swapped4 = false;
	//Bullet Variance
	float strayFactor = 1;
	// Use this for initialization
	void Awake () {
		this.gameObject.GetComponent<SonarFx>().enabled = false;
		GunEnd = GameObject.FindGameObjectWithTag ("GunEnd");
		NormalPos = GameObject.FindGameObjectWithTag ("NormalArmState");
		ADSPos = GameObject.FindGameObjectWithTag ("ADSState");

		SavedTime = Time.time;
//		audioAmb = AddAudio(clipAmb, true, false, 0.2f);
		audioAmb2 = AddAudio(clipAmb2, true, true, 0.2f); // always
//		audioAmb3 = AddAudio(clipAmb3, true, false, 0.2f);
//		audioAmb4 = AddAudio(clipAmb4, true, false, 0.2f);
		audioShoot = AddAudio(clipShoot, false, false, 0.2f);
		//audioAmb2.Play ();

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
			Ammo += 1;
		}
		Controls ();
		UpdateUI ();
		//AudioUpdate ();

	}
	void playAudio(){


	}
	void AudioUpdate(){
		if (health >= 7) {
			//Nothing?
		} else if (health < 7 && health >= 4 && Swapped2 == false) {
			//Debug.Log("fuck");
			Swapped2 = true;
			audioAmb2.loop = false;
			audioAmb2.clip = clipAmb3;
			audioAmb2.Play();
			audioAmb2.loop = true;

		}else if (health < 4 && health >= 1 && Swapped3 == false) {
			//Debug.Log("fuck");
			Swapped3 = true;
			audioAmb2.loop = false;
			audioAmb2.clip = clipAmb4;
			audioAmb2.Play();
			audioAmb2.loop = true;
			
		}else if (health < 1 && Swapped4 == false) {
			//Debug.Log("fuck");
			Swapped4 = true;
			audioAmb2.loop = false;
			audioAmb2.clip = clipAmb;
			audioAmb2.Play();
			audioAmb2.loop = true;
			
		}

	}
	void ADS(){
		GunArm.gameObject.transform.position = ADSPos.gameObject.transform.position;
		Aiming = true;
		strayFactor = 0.5f;
	}

	void Controls() {
		
		if (Input.GetKeyDown(KeyCode.Mouse0) && shooting == false && Ammo >= 1) {
			audioShoot.Play();
			Shoot ();
			
		}
		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			shooting = false;
		}
		if (Input.GetKeyDown(KeyCode.Mouse1) && Aiming == false) {
			ADS();
			
		}
		if (Input.GetKeyUp (KeyCode.Mouse1)) {
			GunArm.gameObject.transform.position = NormalPos.gameObject.transform.position;
			Aiming = false;
			strayFactor = 1;
		}
		if (Input.GetKeyDown (KeyCode.X) && sonarSwap == false) {
			Sonar ();
		}
		if (Input.GetKeyUp (KeyCode.X)) {
			sonarSwap = false;
		}
		if (sonarEnabled == false) {
			//Destroy(this.gameObject.GetComponent<SonarFx>());
			
		} else {
			//this.gameObject.AddComponent<SonarFx> ();
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
		//AmmoTxt.text= ammotemp;
		//HealthTxt.text= healthtemp;

	}

	void Sonar(){
		if(!sonarEnabled){
			//Destroy(this.gameObject.GetComponent<SonarFx>());
			this.gameObject.GetComponent<SonarFx>().enabled = false;
			sonarEnabled = true;
		}else{
			//this.gameObject.AddComponent<SonarFx> ();
			this.gameObject.GetComponent<SonarFx>().enabled = true;
			sonarEnabled = false;
		}
		sonarSwap = true;
	}
	void Shoot(){

		Ammo -= 1;
		//GameObject iceProj = new GameObject ();
		GameObject iceProj = (GameObject)Instantiate (IceProjectile, GunEnd.gameObject.transform.position, this.gameObject.transform.rotation);
		// clone.rigidbody.AddForce(clone.transform.forward * shootForce);

		//iceProj.transform.rotation = this.GetComponentInChildren<Transform> ().transform.rotation;
		//iceProj.GetComponent<Rigidbody> ().AddForce (iceProj.transform.forward * 500f); //Ice balls are slower
		//iceProj.GetComponent<BulletScript>().type = "Ice";


		float randomNumberX = Random.Range(-strayFactor, strayFactor);
		float randomNumberY = Random.Range(-strayFactor, strayFactor);
		float randomNumberZ = Random.Range(-strayFactor, strayFactor);
		//var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
		iceProj.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
		iceProj.GetComponent<Rigidbody>().AddForce(iceProj.transform.forward * 500);

	}

}
