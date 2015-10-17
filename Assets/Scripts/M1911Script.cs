using UnityEngine;
using System.Collections;

public class M1911Script : MonoBehaviour {

	public GameObject BulletPrefab;
	public GameObject GunEnd;
	int Ammo;
	const int MaxAmmoInMag = 7;
	const int MaxAmmoInReserve = 70;
	public int AmmoInMag { get { return Ammo; } set { Ammo = value; } }
	public int AmmoInReserve;
	float strayFactor = 1;
	//float strayFactor;
	// Use this for initialization
	void Start () {
	
		AmmoInMag = 7;
		AmmoInReserve = 21;
	}
	
	// Update is called once per frame
	void Update () {
		//yield return new WaitForSeconds (0.3f);

	}

	public void Reload(){
		if (Ammo < 7 && AmmoInReserve > 0) {
			if (AmmoInReserve - (7 - Ammo) >= 0) {
				AmmoInReserve = AmmoInReserve - (7 - Ammo);
				Ammo = 7;
			} else {
				Ammo = AmmoInReserve;
				AmmoInReserve = 0;
			}
		}

	}

	public void Shoot(bool ADS){
		if (AmmoInMag >= 1) {
			AmmoInMag -= 1;
			//GameObject iceProj = new GameObject ();
			GameObject iceProj = (GameObject)Instantiate (BulletPrefab, GunEnd.gameObject.transform.position, GunEnd.gameObject.transform.rotation);
			// clone.rigidbody.AddForce(clone.transform.forward * shootForce);
		
			//iceProj.transform.rotation = this.GetComponentInChildren<Transform> ().transform.rotation;
			//iceProj.GetComponent<Rigidbody> ().AddForce (iceProj.transform.forward * 500f); //Ice balls are slower
			//iceProj.GetComponent<BulletScript>().type = "Ice";
			//iceProj.GetComponent<BulletScript>().type = "Ice";
			if(ADS){
				strayFactor = 0.5f;
			}else{
				strayFactor = 1;
			}
		
			float randomNumberX = Random.Range (-strayFactor, strayFactor);
			float randomNumberY = Random.Range (-strayFactor, strayFactor);
			float randomNumberZ = Random.Range (-strayFactor, strayFactor);
			//var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
			iceProj.transform.Rotate (randomNumberX, randomNumberY, randomNumberZ);
			iceProj.GetComponent<Rigidbody> ().AddForce (iceProj.transform.forward * 700);
		}
	}
}
