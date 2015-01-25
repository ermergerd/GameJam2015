using UnityEngine;
using System.Collections;

public class BedroomController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (GrannyState.instance.hasGlasses) {
			Debug.Log ("Granny got her glasses, hide em!!!");
			Destroy(GameObject.Find ("bedroom-blur"));
			Destroy(GameObject.Find ("glasses"));
		}
	}
}
