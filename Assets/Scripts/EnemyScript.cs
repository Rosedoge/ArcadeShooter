using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public GameObject goal;
	public GameObject Controller;

	void Start () {
		Controller = GameObject.FindGameObjectWithTag ("Finish");
		goal = GameObject.FindGameObjectWithTag ("MainCamera");
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = goal.gameObject.transform.position; 
	}
	
	// Update is called once per frame
	void Update () {
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = goal.gameObject.transform.position; 
	}

	public void Kill(Vector3 Impact){
		Controller.GetComponent<GameScript>().EnemyManager(this.gameObject.transform, Impact);
		Controller.GetComponent<GameScript>().curEnemy -=1;
		Debug.Log("Spawned?");
		Destroy(this.gameObject);

	}

	void OnCollisionEnter(Collision col){
		//Debug.Log ("WAH");
		if (col.gameObject.tag == "Bullet") {

			Kill (col.gameObject.transform.position);
		}

	}
}
