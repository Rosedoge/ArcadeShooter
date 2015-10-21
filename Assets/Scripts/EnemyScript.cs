using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public GameObject goal;
	public GameObject Controller;

	public int health; //public for prefab purposes and balancing ease

	void Start () {
//		Controller = GameObject.FindGameObjectWithTag ("Finish");
//		goal = GameObject.FindGameObjectWithTag ("MainCamera");
//		NavMeshAgent agent = GetComponent<NavMeshAgent>();
//		agent.destination = goal.gameObject.transform.position; 
	}
	
	// Update is called once per frame
	void Update () {
//		NavMeshAgent agent = GetComponent<NavMeshAgent>();
//		agent.destination = goal.gameObject.transform.position; 
	}

	public void Damage(int amount){
		health += amount;
		//probably works
		//You can use negative to heal
	}


	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Explosion") {
			gameObject.GetComponent<Rigidbody>().AddExplosionForce(20000f,col.gameObject.transform.position, 10f);
		}
	}

	void OnCollisionEnter(Collision col){
		//Debug.Log ("WAH");
		if (col.gameObject.tag == "Bullet") {

			Damage (5);
		}

	}
}
