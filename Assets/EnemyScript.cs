using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public GameObject goal;
	
	void Start () {
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = goal.gameObject.transform.position; 
	}
	
	// Update is called once per frame
	void Update () {
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = goal.gameObject.transform.position; 
	}

	void OnCollisionEnter(Collision col){
		Debug.Log ("WAH");
		if (col.gameObject.tag == "Bullet") {

			Destroy(this.gameObject);
		}

	}
}
