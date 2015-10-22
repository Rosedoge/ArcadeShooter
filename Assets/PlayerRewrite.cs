
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


	public GameObject GameController;
	public GameObject GunArm, M4;
	
	GameObject GunEnd, NormalPos, ADSPos, GunBack;
	int tempAmmo = 0, tempReserve = 0;
	bool shooting = false, sonarEnabled = false, GunSwap = false;
	bool sonarSwap = false, Aiming = false, Reloading = false;
	int Ammo = 1000;
	int health = 10;
	float SavedRTimer, ShotTimer;
	float SavedTime, TouchTime = 0;
	//	bool Swapped2 = false, Swapped3 = false, Swapped4 = false;
	
	string ActiveGun = "M4A1";
	//Bullet Variance
	float strayFactor = 1;
	// Use this for initialization
	void Awake () {
		
		
		this.gameObject.GetComponent<SonarFx>().enabled = false;
		GunEnd = GameObject.FindGameObjectWithTag ("GunEndM4");
		GunEnd.gameObject.transform.forward = this.gameObject.transform.forward;

		GunBack = GameObject.FindGameObjectWithTag ("BackGun");

		NormalPos = GameObject.FindGameObjectWithTag ("NormalArmState");
		ADSPos = GameObject.FindGameObjectWithTag ("ADSState");
		
		SavedTime = Time.time;

		
	}

	void OnTriggerEnter(Collider other) {
		
		
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
		strayFactor = 0.5f;
	}
	
	void Controls() {
		if (ActiveGun == "M4A1") {
			if (Input.GetKeyDown (KeyCode.Mouse0) && shooting == false && Ammo >= 1) {
				M4.gameObject.GetComponent<M4Script>().Shoot (Aiming);
			}
		}

		if (Input.GetKeyDown(KeyCode.Mouse1) && Aiming == false) {
			//ADS();
			this.GetComponent<Camera>().enabled = false;
			ScopeCam.enabled = true;
			
		}
		if (Input.GetKeyUp (KeyCode.Mouse1)) {
			//GunArm.gameObject.transform.position = NormalPos.gameObject.transform.position;
			this.GetComponent<Camera>().enabled = true;
			ScopeCam.enabled = false;
			Aiming = false;
			strayFactor = 1;
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			Reloading = true;


			
		}
		if (Input.GetKeyUp (KeyCode.R)) {
			//GunArm.gameObject.transform.position = NormalPos.gameObject.transform.position;
			Aiming = false;
			strayFactor = 1;
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
		
		string ammotemp = "Ammo: ";
		string healthtemp = "";
		//Ammo

		for (int i = 1; i <= health; i++) {
			healthtemp = healthtemp + "█";
		}
		//AmmoTxt.text= "Ammo " + tempAmmo + " / " + tempReserve;
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
