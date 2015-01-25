using UnityEngine;
using System.Collections;

public class ToasterController : MonoBehaviour {

	public float speed = 1.5f;
	public GameObject fireball1;

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

	void FixedUpdate() {

		// randomly chuck fireballs
		System.Random r = new System.Random();
		int rInt = r.Next(0, 100); 
		
		if (rInt == 99) {
			this.ThrowFireball();
		}
	}

	void ThrowFireball() {
		GameObject temp = (GameObject) Instantiate(fireball1, transform.position, Quaternion.identity);
		temp.rigidbody2D.velocity = -Vector2.right * 15.0f;
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("Bread hit!!!!");

		if (other.gameObject.name == "bread" || 
		    other.gameObject.name == "bread(Clone)") {

			Destroy(other.gameObject);
			
			GrannyState.instance.breadCt++;
			//GrannyState.instance.eatBread();
			
			if (++numberOfBreadHits >= breadHitsToTransition) {

				//Application.LoadLevel("driving");
				GrannyState.instance.loadNextLevel();

			}
		}
	}


}
