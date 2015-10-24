using UnityEngine;
using System.Collections;

public class ShooterEnemyScript : MonoBehaviour {

	//Public

	public GameObject[] WayPoints; // for Patrol;
	public Material[] Colors;
	//Private
	public bool Attacking = false;
	bool PlayerTriggered = false, inSight = false;
	NavMeshAgent agent;
	Vector3 LastSeenPos, LastSeenPos2;
	//Seen Player Timer
	float LastSeenTime, WanderTime; //if it gets over 30 sec, stop chasing
	Vector3 target; //Player Locations
	Vector3 startPosition;
	int wanderRange = 10, health = 6;

	void Awake(){
		//Get the NavMeshAgent so we can send it directions and set start position to the initial location
	
		this.gameObject.GetComponent<MeshRenderer> ().material = Colors [0];
		agent = GetComponent<NavMeshAgent>();

		startPosition = this.transform.position;
		//Start Wandering
		//InvokeRepeating("Wander", 1f, 5f);
	}
	void Wander(){
		//Pick a random location within wander-range of the start position and send the agent there
		Vector3 destination = startPosition + new Vector3(Random.Range (-wanderRange, wanderRange), 
		                                                  0, 
		                                                  Random.Range (-wanderRange, wanderRange));
		NewDestination(destination);
		Attacking = false;
	}

	public void NewDestination(Vector3 targetPoint){
		//Sets the agents new target destination to the position passed in
		agent.SetDestination (targetPoint);

	}


	public void Damage(int amount){
		health -= amount;
		//probably works
		//You can use negative to heal
	}



	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "Player") {
	
			Vector3 direction = col.transform.position - gameObject.transform.position;
			float angle = Vector3.Angle(direction, transform.forward);
			
			// If the angle between forward and where the player is, is less than half the angle of view...
			if(angle < 120 * 0.5f)
			{
				RaycastHit hit;
				
				// ... and if a raycast towards the player hits something...
				if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, 15)) //Sphere Collider Radius
				{
					Debug.DrawLine(this.transform.position,hit.point,Color.green);

					// ... and if the raycast hits the player...
					if(hit.collider.gameObject.tag == "MainCamera")
					{
						inSight = true;
						// ... the player is in sight.
						PlayerTriggered = true;
						
						// Set the last global sighting is the players current position.
						LastSeenPos = hit.transform.position;
						//Debug.Log("I can See the player");
					}
				}
			}
		}


	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {

			inSight = false;
		}
	}

	
	void Update ()
	{
		if (Attacking) {
			this.gameObject.GetComponent<MeshRenderer> ().material = Colors [1];
		} else {
			this.gameObject.GetComponent<MeshRenderer> ().material = Colors [0];
		}
		if (!PlayerTriggered) {
			if (Time.time - WanderTime > 5) {
				Wander ();
				WanderTime = Time.time;
			}

		} else {
			Chasing ();
		}

		//Health
		if (health <= 0) {
			Destroy(gameObject);

		}
	
	}
	
	
	void Shooting ()
	{
	
	
	}
	void Chasing ()
	{
		Attacking = true;
		//He's got to move to the updating LastSeenPos
		//Debug.Log ("I'm chasing her!");
		if (Time.time - LastSeenTime > 2) {
			if (LastSeenPos != LastSeenPos2) {
				//Debug.Log("I'll get here now!");
				NewDestination (LastSeenPos);
			} else {
				//Debug.Log ("Damn... she's gone?");
				PlayerTriggered = false;
			}
			LastSeenPos2 = LastSeenPos;
			LastSeenTime = Time.time;
		}
	}
	
	
	void Patrolling ()
	{

	}
}
