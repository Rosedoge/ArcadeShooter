using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	float speed = 300f;
	float timer = 0f;
	bool HitEnemy = false;
  void start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 velocity = this.gameObject.transform.forward;
		Ray ray = new Ray(this.gameObject.transform.position, this.gameObject.transform.forward);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, (Time.deltaTime * speed )* .1f)){
			Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
//			velocity = 2 * ( Vector3.Dot( velocity, Vector3.Normalize( hit.normal ) ) ) * Vector3.Normalize( hit.normal ) - velocity; //Following formula  v' = 2 * (v . n) * n - v
//			
//			velocity *= -1; 
			this.gameObject.GetComponent<Rigidbody>().AddForce(reflectDir * 500f);
		}

		timer += 1.0F * Time.deltaTime;
		if (timer >= 10)
		{
			GameObject.Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Enemy" && HitEnemy == false) {
//			//col.gameObject.GetComponent<EnemyScript>().Kill();
//			HitEnemy = true;
			GameObject.Destroy(this.gameObject);

		}



	}


}

