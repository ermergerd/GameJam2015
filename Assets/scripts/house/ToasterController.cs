using UnityEngine;
using System.Collections;

public class ToasterController : MonoBehaviour {

	public float speed = 1.5f;
	private Vector3 start;
	private Vector3 end;
	private bool movingLeftToRight = true;
	private float toasterMovementBuffer = .2f;
	private int numberOfBreadHits = 0;
	private const int breadHitsToTransition = 10;

	// Use this for initialization
	void Start () {

		start = transform.position;
		end = new Vector3(9.58f, -2.4f, 0f);

	}
	
	// Update is called once per frame
	void Update () {

		if (movingLeftToRight) {
			transform.position = Vector3.Lerp(transform.position, end, Time.deltaTime * speed);
		} else {
			transform.position = Vector3.Lerp(transform.position, start, Time.deltaTime * speed);
		}

		if (transform.position.x >= end.x - toasterMovementBuffer && 
		    transform.position.y <= end.y + toasterMovementBuffer) {

			movingLeftToRight = false;

		} else if (transform.position.x <= start.x + toasterMovementBuffer && 
		           transform.position.y >= start.y - toasterMovementBuffer) {

			movingLeftToRight = true;

		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Bread hit!!!!");

		Destroy(other.gameObject);

		if (++numberOfBreadHits >= breadHitsToTransition) {
			Application.LoadLevel("driving");
		}
	}


}
