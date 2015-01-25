using UnityEngine;
using System.Collections;

public class GrannyController : MonoBehaviour {

	public float speed = 2.0f;
	public GameObject bullet;
	public Sprite grannyWithGlasses;

	private const float grannySpeedWithCane = 4.0f;
	private Vector3 target;
	private const string myGlasses = "glasses";
	private const int ohThereItIsAudio = 0;
	private const int whereAreMyGlassesAudio = 1;
	private const int flushingAudio = 2;
	private AudioSource[] aSources;
	private GameObject bathroomDoor;
	private GameObject bathroomDoorBlur;
	private bool takingAPee;
	private SpriteRenderer grannyImage;

	// granny can only fire bread every 1.5 seconds
	private const float coolDownTimeLimit = 1.5f;
	private float coolDownBulletTimeLeft = coolDownTimeLimit;

	void Start () {
		target = transform.position;
		aSources = GetComponents<AudioSource>();
	 	grannyImage = gameObject.GetComponent<SpriteRenderer>();

		if (Application.loadedLevelName == "wakeup_scene") {
			bathroomDoor = GameObject.Find("door");
			bathroomDoorBlur = GameObject.Find("door-blur");
			takingAPee = false;
		}

		if (GrannyState.instance.hasGlasses) {
			grannyImage.sprite = grannyWithGlasses;
		}

		if (Application.loadedLevelName == "wakeup_scene" && 
		    GrannyState.instance.hasGlasses == false) {

			aSources[whereAreMyGlassesAudio].Play();

		}

		if (GrannyState.instance.hasCane) {
			speed = grannySpeedWithCane;
		}

	}
	
	void Update () {

		if (coolDownBulletTimeLeft > 1) {
			
			Debug.Log(coolDownBulletTimeLeft);
			coolDownBulletTimeLeft -= Time.deltaTime;
			
		} 

		if (Input.GetMouseButtonDown(0)) {
			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			target.z = transform.position.z;

			if (Application.loadedLevelName == "kitchen") {

				if (coolDownBulletTimeLeft > 1) {

					Debug.Log(coolDownBulletTimeLeft);
					coolDownBulletTimeLeft -= Time.deltaTime;

				} else {

					Debug.Log(coolDownBulletTimeLeft);

					GameObject temp = (GameObject) Instantiate(bullet, transform.position, Quaternion.identity);
					temp.rigidbody2D.velocity = Vector2.right * 20.0f;

					coolDownBulletTimeLeft = coolDownTimeLimit;
				}

			}
		}

		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

		if (takingAPee) {
			if (aSources[flushingAudio].isPlaying==false) {
				gameObject.renderer.enabled = true;
				bathroomDoorBlur.renderer.enabled = false;
				bathroomDoor.renderer.enabled = false;
				takingAPee = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		Debug.Log ("Granny OnTriggerEnter2D Called");

		if (other.name == myGlasses) {

			aSources[ohThereItIsAudio].Play();
			Destroy(other.gameObject);
			GrannyState.instance.hasGlasses = true;
			Destroy(GameObject.Find ("bedroom-blur"));
			grannyImage.sprite = grannyWithGlasses;

		} else if (other.name == "kitchen door boundary") {

			// transition between the kitchen and the bedroom
			if (Application.loadedLevelName == "kitchen") {

				Application.LoadLevel("wakeup_scene");

			} else {

				Application.LoadLevel("kitchen");

			}
			//GrannyState.instance.loadNextLevel();

		} else if (other.name == "bathroom door boundary") {

			if (GrannyState.instance.hasGlasses) {
				bathroomDoor.renderer.enabled = true;
			} else {
				bathroomDoorBlur.renderer.enabled = true;
			}

			gameObject.renderer.enabled = false;
			aSources[flushingAudio].Play();
			GrannyState.instance.currentBladder = 0;
			takingAPee = true;

		} else if (other.name == "fireball2" ||
		           other.name == "fireball2(Clone)") {

			audio.Play ();
			GrannyState.instance.currentBloodPressure += 3;
			Destroy(other.gameObject);

		}
	}
}