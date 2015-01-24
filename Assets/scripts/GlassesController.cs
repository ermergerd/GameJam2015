using UnityEngine;
using System.Collections;

public class GlassesController : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Glasses OnTriggerEnter2D Called");
		
		//audio.Play();
		
		//Destroy(gameObject, 2.0f);
	}

//	void OnTriggerEnter2D () {
//
//		Debug.Log ("Glasses OnTriggerEnter2D Called");
//
//		audio.Play();
//
//		Destroy(gameObject, 2.0f);
//			
//		//Destroy (GameObject.FindGameObjectWithTag("glasses"));
//
//	}

}
