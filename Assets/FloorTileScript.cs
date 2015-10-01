using UnityEngine;
using System.Collections;

public class FloorTileScript : MonoBehaviour {

	public Sprite[] images;
	// Use this for initialization
	void Start () {
		InvokeRepeating("SwapColors", 0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SwapColors(){
		int rand = Random.Range (0, 6);
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = images [rand];
	}
}
