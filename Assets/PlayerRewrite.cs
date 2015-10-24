
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerRewrite: MonoBehaviour {
	public Text AmmoTxt;
	public Text HealthTxt;
	public GameObject IceProjectile;

	public Camera ScopeCam;
	public GameObject ScopeLens;
	public AudioClip clipShoot;

	public GameObject[] FlashObjects;
	public GameObject GameController;
	public GameObject GunArm, M4;
	
	GameObject GunEnd, NormalPos, ADSPos, GunBack;
	int tempAmmo = 0, tempReserve = 0;
	bool shooting = false, sonarEnabled = false, GunSwap = false;
	bool sonarSwap = false, Aiming = false, Reloading = false;
	int Ammo = 1000;
	public int health = 10;
	float SavedRTimer, ShotTimer;
	float SavedTime, TouchTime = 0;
	//	bool Swapped2 = false, Swapped3 = false, Swapped4 = false;
	
	string ActiveGun = "M4A1";
	//Bullet Variance
	float strayFactor = 1;
	// Use this for initialization
	void Start(){
		this.GetComponent<Camera> ().enabled = true;
		ScopeCam.enabled = false;

	}
	void Awake () {
		
		for(int x = 0;x < 3; x++){ // Turn off Flashes
			FlashObjects[x].gameObject.GetComponent<MeshRenderer>().enabled = false;;
		}
		this.gameObject.GetComponent<SonarFx>().enabled = false;
		GunEnd = GameObject.FindGameObjectWithTag ("GunEndM4");
		GunEnd.gameObject.transform.forward = this.gameObject.transform.forward;

		GunBack = GameObject.FindGameObjectWithTag ("BackGun");

		NormalPos = GameObject.FindGameObjectWithTag ("NormalArmState");
		ADSPos = GameObject.FindGameObjectWithTag ("ADSState");
		
		SavedTime = Time.time;

		
	}
	/// <summary>
	/// Raises the collision enter event.
	/// Activates Whenever collided with
	///Ammo Box
	/// Adds Ammo to the Player's Guns
	///
	/// Enemy
	/// 	If it has Attack = true then it can hurt the player, to allow 'stealth' elements
	/// </summary>
	/// <param name="col">Col.</param>

	void OnCollisionEnter(Collision col){


	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "AmmoBox") {
			int rando = Random.Range (20, 45);
			M4.gameObject.GetComponent<M4Script> ().AmmoInReserve += rando;
			Destroy (other.gameObject);
		} else 	if (other.gameObject.tag == "Enemy") {


		}
		
	}


	// Update is called once per frame
	void Update () {
		
		//SavedRTimer = Time.time;
		Controls ();
		UpdateUI ();
		//AudioUpdate ();
		
	}

	void ADS(){
		GunArm.gameObject.transform.position = ADSPos.gameObject.transform.position;
		Aiming = true;
		strayFactor = 0.2f;
	}
	
	void Controls() {
		if (ActiveGun == "M4A1") {
			if (Input.GetKey (KeyCode.Mouse0) && M4.gameObject.GetComponent<M4Script>().AmmoInMag >= 1) {
				if(Time.time - ShotTimer > 0.1f){
					//.Play ();
					for(int x = 0;x < 3; x++){
						FlashObjects[x].gameObject.GetComponent<MeshRenderer>().enabled = true;
					}
					M4.gameObject.GetComponent<M4Script>().Shoot ( strayFactor);
					ShotTimer = Time.time;
				}else{
					for(int x = 0;x < 3; x++){
						FlashObjects[x].gameObject.GetComponent<MeshRenderer>().enabled = false;;
					}
				}

			}else{
				for(int x = 0;x < 3; x++){
					FlashObjects[x].gameObject.GetComponent<MeshRenderer>().enabled = false;;
				}
			}
			}

//		if (Input.GetKeyUp (KeyCode.Mouse0)) {/// When the Player runs out of bullets, it keeps the final draw of the flash
//			//So this just hard ends it.
//			for(int x = 0;x < 3; x++){
//				FlashObjects[x].gameObject.GetComponent<MeshRenderer>().enabled = false;;
//			}
//			
//		}
		if (Input.GetKeyDown(KeyCode.Mouse1) && Aiming == false) {
			//ADS();
			GunArm.gameObject.transform.position = ADSPos.gameObject.transform.position;
			this.GetComponent<Camera>().enabled = false;
			ScopeCam.enabled = true;
			strayFactor = 0.2f;
			
		}
		if (Input.GetKeyUp (KeyCode.Mouse1)) {
			GunArm.gameObject.transform.position = NormalPos.gameObject.transform.position;
			this.GetComponent<Camera>().enabled = true;
			ScopeCam.enabled = false;
			Aiming = false;
			strayFactor = 1;
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			Reloading = true;
			if (ActiveGun == "M4A1") {
				M4.gameObject.GetComponent<M4Script>().Reload();
				//Reloading = M1911.gameObject.GetComponent<M1911Script>().Reload;
				//reloadTimer = Time.time + 2f;
				
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

		
	}
	void gunSwap(){

	}
	
	void UpdateUI() {
		int tempAmmo = 0, tempReserve = 0;
		//string ammotemp = "Ammo: ";
		string healthtemp = "";
		//Ammo
		if (ActiveGun == "M4A1") {
			tempAmmo = M4.gameObject.GetComponent<M4Script> ().AmmoInMag;
			tempReserve = M4.gameObject.GetComponent<M4Script> ().AmmoInReserve;
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

	}
	
}
