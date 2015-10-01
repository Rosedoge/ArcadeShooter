using UnityEngine;
using System.Collections;

public class StrobeScript : MonoBehaviour {

	
	Light _light;
	public float onTime = 0.1f;
	public float offTime = 0.1f;
	
	void Start()
	{
		_light = this.gameObject.GetComponent<Light>();
		Toggle();
	}
	
	void Toggle()
	{
		if(!_light) return;
		
		_light.enabled = !_light.enabled;
		
		if(_light.enabled)
			Invoke("Toggle", onTime);
		else Invoke("Toggle", offTime);
		
	}

	

}
