using UnityEngine;
using System.Collections;

public class GrannyController : MonoBehaviour {

	public float speed = 1.5f;
	private Vector3 target;
	
	void Start () {
		target = transform.position;
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			target.z = transform.position.z;
		}
		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Granny OnTriggerEnter Called");


		if(other.name=="glasses"){
			audio.Play ();
			Destroy(other.gameObject);
		}
	}
	
}