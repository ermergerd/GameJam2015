using UnityEngine;
using System.Collections;

public class BedroomController : MonoBehaviour {

	private GameObject bathroomDoor;
	private GameObject bathroomDoorBlur;

	// Use this for initialization
	void Start () {

		if (GrannyState.instance.hasGlasses==true) {

			Debug.Log ("Granny got her glasses, hide em!!!");
			Destroy(GameObject.Find ("bedroom-blur"));
			Destroy(GameObject.Find ("glasses"));

		}

		// always hide the bathroom doors when entering the bedroom
		bathroomDoor = GameObject.Find("door");
		bathroomDoorBlur = GameObject.Find("door-blur");
		bathroomDoor.renderer.enabled = false;
		bathroomDoorBlur.renderer.enabled = false;

	}
}
