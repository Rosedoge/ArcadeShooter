using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
	public Text AmmoTxt;
	public Text HealthTxt;
	public GameObject IceProjectile;
//	public AudioClip clipAmb; 
//	public AudioClip clipAmb2;
//	public AudioClip clipAmb3;
//	public AudioClip clipAmb4; 
	public AudioClip clipShoot;
//	private AudioSource audioAmb;
//	private AudioSource audioAmb2;
//	private AudioSource audioAmb3;
//	private AudioSource audioAmb4;
	private AudioSource audioShoot;
	public GameObject GameController;
	public GameObject GunArm, M1911, Skorp;

	GameObject GunEnd, NormalPos, ADSPos, GunBack;
	int tempAmmo = 0, tempReserve = 0;
	bool shooting = false, sonarEnabled = false, GunSwap = false;
	bool sonarSwap = false, Aiming = false, Reloading = false;
	int Ammo = 1000;
	int health = 10;
	float SavedRTimer, ShotTimer;
	float SavedTime, TouchTime = 0;
//	bool Swapped2 = false, Swapped3 = false, Swapped4 = false;

	string ActiveGun = "M1911";
	//Bullet Variance
	float strayFactor = 1;
	// Use this for initialization
	void Awake () {


		this.gameObject.GetComponent<SonarFx>().enabled = false;
		GunEnd = GameObject.FindGameObjectWithTag ("GunEnd");
		GunEnd.gameObject.transform.forward = this.gameObject.transform.forward;
		GunBack = GameObject.FindGameObjectWithTag ("BackGun");
		Skorp.gameObject.transform.position = GunBack.gameObject.transform.position;
		NormalPos = GameObject.FindGameObjectWithTag ("NormalArmState");
		ADSPos = GameObject.FindGameObjectWithTag ("ADSState");

		SavedTime = Time.time;
//		audioAmb = AddAudio(clipAmb, true, false, 0.2f);
//		audioAmb2 = AddAudio(clipAmb2, true, true, 0.2f); // always
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
	void OnTriggerEnter(Collider other) {


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

		//SavedRTimer = Time.time;
		Controls ();
		UpdateUI ();
		//AudioUpdate ();

	}
	void playAudio(){


	}
//	void AudioUpdate(){
//		if (health >= 7) {
//			//Nothing?
//		} else if (health < 7 && health >= 4 && Swapped2 == false) {
//			//Debug.Log("fuck");
//			Swapped2 = true;
//			audioAmb2.loop = false;
//			audioAmb2.clip = clipAmb3;
//			audioAmb2.Play();
//			audioAmb2.loop = true;
//
//		}else if (health < 4 && health >= 1 && Swapped3 == false) {
//			//Debug.Log("fuck");
//			Swapped3 = true;
//			audioAmb2.loop = false;
//			audioAmb2.clip = clipAmb4;
//			audioAmb2.Play();
//			audioAmb2.loop = true;
//			
//		}else if (health < 1 && Swapped4 == false) {
//			//Debug.Log("fuck");
//			Swapped4 = true;
//			audioAmb2.loop = false;
//			audioAmb2.clip = clipAmb;
//			audioAmb2.Play();
//			audioAmb2.loop = true;
//			
//		}
//
//	}
	void ADS(){
		GunArm.gameObject.transform.position = ADSPos.gameObject.transform.position;
		Aiming = true;
		strayFactor = 0.5f;
	}

	void Controls() {
		if (ActiveGun == "M1911") {
			if (Input.GetKeyDown (KeyCode.Mouse0) && shooting == false && Ammo >= 1) {
				audioShoot.Play ();
				M1911.gameObject.GetComponent<M1911Script>().Shoot (Aiming);
			
			}
			if (Input.GetKeyUp (KeyCode.Mouse0)) {
				shooting = false;
			}
		} else if (ActiveGun == "Skorpion") {
			if (Input.GetKey (KeyCode.Mouse0)&& Skorp.gameObject.GetComponent<SkorpScript>().AmmoInMag >= 1) {
				if(Time.time - ShotTimer > 0.07f){
					audioShoot.Play ();
					Skorp.gameObject.GetComponent<SkorpScript>().Shoot (Aiming);
					ShotTimer = Time.time;
				}
				
			}
		}
		if (Input.GetKeyDown(KeyCode.Mouse1) && Aiming == false) {
			ADS();
			
		}
		if (Input.GetKeyUp (KeyCode.Mouse1)) {
			GunArm.gameObject.transform.position = NormalPos.gameObject.transform.position;
			Aiming = false;
			strayFactor = 1;
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			Reloading = true;
			if (ActiveGun == "M1911") {
				M1911.gameObject.GetComponent<M1911Script>().Reload();
				//Reloading = M1911.gameObject.GetComponent<M1911Script>().Reload;
				//reloadTimer = Time.time + 2f;
				
			} else if (ActiveGun == "Skorpion") {
				Skorp.gameObject.GetComponent<SkorpScript>().Reload();
				//reloadTimer = Time.time + 5f;
			}
			
		}
		if (Input.GetKeyUp (KeyCode.R)) {
			Reloading = false;

		}
		if (Input.GetKeyDown (KeyCode.X) && sonarSwap == false) {
			Sonar ();
		}
		if (Input.GetKeyUp (KeyCode.X)) {
			sonarSwap = false;
		}
		if (Input.GetKeyDown (KeyCode.Q) && GunSwap == false) {
			gunSwap ();
		}
		if (Input.GetKeyUp (KeyCode.Q)) {
			GunSwap = false;
		}

	}
	void gunSwap(){
		if (ActiveGun == "M1911") {
			ActiveGun = "Skorpion";
			Skorp.gameObject.transform.position = M1911.gameObject.transform.position;
			M1911.gameObject.transform.position = GunBack.gameObject.transform.position;
			GunEnd = GameObject.FindGameObjectWithTag ("GunEndSkorp");
		}else if (ActiveGun == "Skorpion") {
			ActiveGun = "M1911";
			M1911.gameObject.transform.position = Skorp.gameObject.transform.position;
			Skorp.gameObject.transform.position = GunBack.gameObject.transform.position;
			GunEnd = GameObject.FindGameObjectWithTag ("GunEnd");
		}
		GunSwap = true;
	}

	void UpdateUI() {

		string ammotemp = "Ammo: ";
		string healthtemp = "";
		//Ammo
		if (ActiveGun == "M1911") {
			tempAmmo = M1911.gameObject.GetComponent<M1911Script>().AmmoInMag;
			tempReserve = M1911.gameObject.GetComponent<M1911Script>().AmmoInReserve;

		} else if (ActiveGun == "Skorpion") {
			tempAmmo = Skorp.gameObject.GetComponent<SkorpScript>().AmmoInMag;
			tempReserve = Skorp.gameObject.GetComponent<SkorpScript>().AmmoInReserve;

		}
		for (int i = 1; i <= health; i++) {
			healthtemp = healthtemp + "█";
		}
		AmmoTxt.text= "Ammo " + tempAmmo + " / " + tempReserve;
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
		if (ActiveGun == "M1911") {
			Ammo -= 1;
			//GameObject iceProj = new GameObject ();
			GameObject iceProj = (GameObject)Instantiate (IceProjectile, GunEnd.gameObject.transform.position, this.gameObject.transform.rotation);

			float randomNumberX = Random.Range (-strayFactor, strayFactor);
			float randomNumberY = Random.Range (-strayFactor, strayFactor);
			float randomNumberZ = Random.Range (-strayFactor, strayFactor);
			//var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
			iceProj.transform.Rotate (randomNumberX, randomNumberY, randomNumberZ);
			iceProj.GetComponent<Rigidbody> ().AddForce (iceProj.transform.forward * 700);
		}else if (ActiveGun == "Skorpion") {
			Ammo -= 1;
			//GameObject iceProj = new GameObject ();
		
			GameObject iceProj = (GameObject)Instantiate (IceProjectile, GunEnd.gameObject.transform.position, this.gameObject.transform.rotation);
			
			float randomNumberX = Random.Range (-strayFactor, strayFactor);
			float randomNumberY = Random.Range (-strayFactor, strayFactor);
			float randomNumberZ = Random.Range (-strayFactor, strayFactor);
			//var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
			iceProj.transform.Rotate (randomNumberX, randomNumberY, randomNumberZ);
			iceProj.GetComponent<Rigidbody> ().AddForce (iceProj.transform.forward * 700);

		}
	}

}
