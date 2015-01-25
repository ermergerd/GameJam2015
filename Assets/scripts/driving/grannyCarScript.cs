using UnityEngine;
using System.Collections;

public class grannyCarScript : MonoBehaviour {

	private float speed = 9;
	private Vector3 target;
	private AudioSource[] aSources;

	// Use this for initialization
	void Start () {
		target = transform.position;

		TrailRenderer tr = transform.GetChild(0).GetComponent<TrailRenderer>();
		tr.sortingLayerName = "car";
		tr.sortingOrder = 0;
		tr = transform.GetChild(1).GetComponent<TrailRenderer>();
		tr.sortingLayerName = "car";
		tr.sortingOrder = 0;

		aSources = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButtonDown("Fire1")){
			speed = 9f;
		}

		if(Input.GetButton("Fire1")){
			speed+=.1f;

			Vector3 origPosition = transform.position;

			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			target.z = transform.position.z;



			transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

			Vector3 moveDirection = transform.position - origPosition;
			if (moveDirection != Vector3.zero) 
			{
				float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90;
				transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			}

			//rigidbody2D.AddForce(Input.mousePosition.normalized);

		}

		if(Input.GetButtonUp("Fire1")){
			speed = 9f;
		}

		if(speed > 200){
			//TODO more audio!
		}else if (speed > 150){

		}else if (speed > 100){

		}else if (speed > 50){
			if(!aSources[1].isPlaying)
				aSources[1].Play();
		}

	}

	void OnCollisionEnter2D(Collision2D collision) {
		Debug.Log("crash @ speed: " + speed);
		speed = 0;
		if(collision.gameObject.name=="finishline"){
			//go to bingo!
			Application.LoadLevel("bingo_scene");
		}else{
			GrannyState.currentBloodPressure+=20;
			//play crash sound
			if(!aSources[0].isPlaying)
				aSources[0].Play();
		}
	}
}
