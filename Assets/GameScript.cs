using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {

	public Text GameText;
	public GameObject EnemyPrefab;
	public GameObject[] SpawnLocs;

	//local variables
	const int enemyLimit = 20;
	int curEnemy = 2; //spawns two to starts
	int curSpawnLoc = 0;


	// Use this for initialization
	void Start () {
		SpawnEnemy (1, SpawnLocs[1].transform); // one to start

	}
	
	// Update is called once per frame
	void Update () {


	}
	void SpawnEnemy(int NeededSpawns, Transform deadEnemy){
		if (NeededSpawns == 2) {
			GameObject Enemy = (GameObject)Instantiate (EnemyPrefab, deadEnemy.gameObject.transform.position,  SpawnLocs [curSpawnLoc].gameObject.transform.rotation);
			upCurSpawn ();
			GameObject Enemy2 = (GameObject)Instantiate (EnemyPrefab, deadEnemy.gameObject.transform.position,  SpawnLocs [curSpawnLoc].gameObject.transform.rotation);
			upCurSpawn ();
		} else if(NeededSpawns == 1) {
			GameObject Enemy = (GameObject)Instantiate (EnemyPrefab, deadEnemy.gameObject.transform.position,  SpawnLocs [curSpawnLoc].gameObject.transform.rotation);
			upCurSpawn ();

		}

	}
	void upCurSpawn(){
		curSpawnLoc++;
		if (curSpawnLoc >= 4) {
			curSpawnLoc = 0;
		}
		curEnemy++;

	}
	public void EnemyManager(Transform deadEnemy){
		Debug.Log("Spawned event");
		if ((curEnemy + 2) >= enemyLimit) {
			SpawnEnemy (1, deadEnemy);
		}else{
			SpawnEnemy(2, deadEnemy);

		}
	}

	public void EndGame(){
		GameText.text = "LOSER!";

	}
}
