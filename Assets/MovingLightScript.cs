using UnityEngine;
using System.Collections;

public class MovingLightScript : MonoBehaviour {

	public GameObject Loc1;
	public GameObject Loc2;
	int WhereTo = 1;
	// Use this for initialization
	void Start () {
		this.gameObject.MoveTo (Loc1.gameObject.transform.position, 2f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (this.gameObject.transform.position == Loc1.gameObject.transform.position 
		 || this.gameObject.transform.position == Loc2.gameObject.transform.position) {

			if(WhereTo == 1){
				this.gameObject.MoveTo (Loc2.gameObject.transform.position, 2f, 0.5f);
				WhereTo = 2;

			}else{
				this.gameObject.MoveTo (Loc1.gameObject.transform.position, 3f, 0.1f);
				WhereTo = 1;
			}


		}else{ // it is in one of the locations{


		}
	}
}
