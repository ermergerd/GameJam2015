using UnityEngine;
using System.Collections;

public class GrannyController : MonoBehaviour {

	public float speed = 1.5f;
	private Vector3 target;
	private const string myGlasses = "glasses";
	private const int ohThereItIsAudio = 0;
	private const int whereAreMyGlassesAudio = 1;
	private AudioSource[] aSources;
	public GameObject bullet;

	void Start () {
		target = transform.position;
		aSources = GetComponents<AudioSource>();

		if (Application.loadedLevelName == "wakeup_scene" && 
		    GrannyState.instance.hasGlasses == false) {

			aSources[whereAreMyGlassesAudio].Play();

		}
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			target.z = transform.position.z;

			if (Application.loadedLevelName == "kitchen") {

				GameObject temp = (GameObject) Instantiate(bullet, transform.position, Quaternion.identity);
				temp.rigidbody2D.velocity = Vector2.right * 20.0f;


			}
		}

		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
	
	}

	void OnTriggerEnter2D(Collider2D other) {

		Debug.Log ("Granny OnTriggerEnter2D Called");

		if (other.name == myGlasses) {

			aSources[ohThereItIsAudio].Play();
			Destroy(other.gameObject);
			GrannyState.instance.hasGlasses = true;
			Destroy(GameObject.Find ("bedroom-blur"));

		} else if (other.name == "kitchen door boundary") {

			// transition between the kitchen and the bedroom
//			if (Application.loadedLevelName == "kitchen") {
//
//				Application.LoadLevel("wakeup_scene");
//
//			} else {
//
//				Application.LoadLevel("kitchen");
//
//			}
			GrannyState.instance.loadNextLevel();

		} else if (other.name == "bathroom door boundary") {

			GrannyState.instance.currentBladder = 0;

		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		//target = transform.position;
		Debug.Log ("Granny collision detected");

	}
	
}