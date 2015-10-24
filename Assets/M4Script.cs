using UnityEngine;
using System.Collections;

public class M4Script : MonoBehaviour {

	public GameObject BulletPrefab;
	public GameObject GunEnd;
	int Ammo;
	const int MaxAmmoInMag = 30;
	const int MaxAmmoInReserve = 300;
	public int AmmoInMag { get { return Ammo; } set { Ammo = value; } }
	public int AmmoInReserve;
	//float strayFactor = 1;
	// Use this for initialization
	void Start () {
		
		AmmoInMag = 30;
		AmmoInReserve = 300;
	}
	
	// Update is called once per frame
	void Update () {
		//yield return new WaitForSeconds (0.3f);
		
	}
	
	public void Reload(){
		if (Ammo < 30 && AmmoInReserve > 0) {
			if (AmmoInReserve - (30 - Ammo) >= 0) {
				AmmoInReserve = AmmoInReserve - (30 - Ammo);
				Ammo = 30;
			} else {
				Ammo = AmmoInReserve;
				AmmoInReserve = 0;
			}
		}
		
	}
	
	public void Shoot(float strayFactor){
		if (AmmoInMag >= 1) {
			AmmoInMag -= 1;
			//GameObject iceProj = new GameObject ();
			GameObject iceProj = (GameObject)Instantiate (BulletPrefab, GunEnd.gameObject.transform.position, GunEnd.gameObject.transform.rotation);
			// clone.rigidbody.AddForce(clone.transform.forward * shootForce);
			
			//iceProj.transform.rotation = this.GetComponentInChildren<Transform> ().transform.rotation;
			//iceProj.GetComponent<Rigidbody> ().AddForce (iceProj.transform.forward * 500f); //Ice balls are slower
			//iceProj.GetComponent<BulletScript>().type = "Ice";
			//iceProj.GetComponent<BulletScript>().type = "Ice";
			iceProj.transform.forward = GunEnd.gameObject.transform.forward;
			float randomNumberX = Random.Range (-strayFactor, strayFactor);
			float randomNumberY = Random.Range (-strayFactor, strayFactor);
			float randomNumberZ = Random.Range (-strayFactor, strayFactor);
			//var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
			iceProj.transform.Rotate (randomNumberX, randomNumberY, randomNumberZ);
			iceProj.GetComponent<Rigidbody> ().AddForce (iceProj.transform.forward * 500);
		}
	}
}
