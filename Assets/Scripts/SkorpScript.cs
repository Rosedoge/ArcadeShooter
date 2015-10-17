using UnityEngine;
using System.Collections;

public class SkorpScript : MonoBehaviour {

	public GameObject BulletPrefab;
	public GameObject GunEnd;
	int Ammo;
	const int MaxAmmoInMag = 25;
	const int MaxAmmoInReserve = 225;
	public int AmmoInMag { get { return Ammo; } set { Ammo = value; } }
	public int AmmoInReserve;
	float strayFactor = 1;
	//float strayFactor;
	// Use this for initialization
	void Start () {

		Ammo = 25;
		//AmmoInMag = 2500;
		AmmoInReserve = 100;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Reload(){

		//yield return new WaitForSeconds (0.3f);
		if (Ammo < 25 && AmmoInReserve > 0) {
			if (AmmoInReserve - (25 - Ammo) >= 0) {
				AmmoInReserve = AmmoInReserve - (25 - Ammo);
				Ammo = 25;
			} else {
				Ammo = AmmoInReserve;
				AmmoInReserve = 0;
			}
		}
	}
	
	public void Shoot(bool ADS){
		if (Ammo >= 1) {
			Ammo -= 1;
			//GameObject iceProj = new GameObject ();
			GameObject iceProj = (GameObject)Instantiate (BulletPrefab, GunEnd.gameObject.transform.position, GunEnd.gameObject.transform.rotation);

			if(ADS){
				strayFactor = 1.5f;
			}else{
				strayFactor = 3f;
			}
			
			float randomNumberX = Random.Range (-strayFactor, strayFactor);
			float randomNumberY = Random.Range (-strayFactor, strayFactor);
			float randomNumberZ = Random.Range (-strayFactor, strayFactor);
			//var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
			iceProj.transform.Rotate (randomNumberX, randomNumberY, randomNumberZ);
			iceProj.GetComponent<Rigidbody> ().AddForce (-iceProj.gameObject.transform.forward * 700);
		}
	}
}
