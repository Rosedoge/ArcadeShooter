using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {

	public Text GameText;
	public GameObject EnemyPrefab;
	public GameObject[] SpawnLocs;

	//local variables
	const int enemyLimit = 20;
	public int curEnemy = 2; //spawns two to starts
	int curSpawnLoc = 0;
	private bool over = false;



	// Use this for initialization
	void Start () {
		SpawnEnemy (1, SpawnLocs[1].transform); // one to start

	}
	
	// Update is called once per frame
	void Update () {


	}
	void SpawnEnemy(int NeededSpawns, Transform deadEnemy){
		Vector3 temp;
		if (NeededSpawns == 2) {
			GameObject Enemy = (GameObject)Instantiate (EnemyPrefab, deadEnemy.gameObject.transform.position,  SpawnLocs [curSpawnLoc].gameObject.transform.rotation);
			upCurSpawn ();
			temp = deadEnemy.gameObject.transform.position;
			temp += new Vector3(0,0,1);
			GameObject Enemy2 = (GameObject)Instantiate (EnemyPrefab, temp,  SpawnLocs [curSpawnLoc].gameObject.transform.rotation);
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
		//Debug.Log("Spawned event");
		if (curEnemy < enemyLimit) {
			if ((curEnemy + 2) >= enemyLimit) {
				SpawnEnemy (1, deadEnemy);
			} else if ((curEnemy + 2) <= enemyLimit) {
				SpawnEnemy (2, deadEnemy);

			}
			Debug.Log (curEnemy);
		}
	}

	public void EndGame(){
		GameText.text = "Parting is such sweet sorrow!";
		Application.LoadLevel ("EndScene");
	}
}
