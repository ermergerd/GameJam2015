using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	private float splashOnScreenTimeLimit = 4.0f;

	// Update is called once per frame
	void Update () {
		if (splashOnScreenTimeLimit > 1) {
			
			Debug.Log(splashOnScreenTimeLimit);
			splashOnScreenTimeLimit -= Time.deltaTime;
			
		} else {
			Application.LoadLevel ("wakeup_scene");
		}
	}
}
