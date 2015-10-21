using UnityEngine;
using System.Collections;

public class ExplosiveBarrel : MonoBehaviour {

	public int health = 5;
	public GameObject ExplosionPrefab;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			//Call Explosion, destroy this
			//use script in explosion to deal with shit
			//GameObject iceProj = (GameObject)Instantiate (IceProjectile, GunEnd.gameObject.transform.position, this.gameObject.transform.rotation);
			GameObject Explosion = (GameObject)Instantiate (ExplosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
			Destroy(this.gameObject);

		}
	
	}

	void OnCollisionEnter(Collision col){
		//Debug.Log ("WAH");
		if (col.gameObject.tag == "Bullet") {
			health-=1;
			Destroy(col.gameObject);
		}
	}
		
}