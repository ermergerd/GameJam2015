using UnityEngine;
using System.Collections;

public class KitchenController : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GameObject toaster = GameObject.Find("Toaster");
		SpriteRenderer toasterSprite = toaster.GetComponent<SpriteRenderer>();
		ToasterController toasterControl = toaster.GetComponent<ToasterController>();
		CircleCollider2D toasterCollider = toaster.GetComponent<CircleCollider2D>();

		if (GrannyState.instance.hasGlasses == true) {
			Debug.Log ("Granny got her glasses, hide em!!!");
			Destroy(GameObject.Find ("kitchen-blur"));

			// make the toaster easier to hit
			toasterSprite.color = new Color(1f, 1f, 1f, 1f);
			toasterControl.speed = 3f;
			toasterCollider.radius = 3.94f;

		} else {

			// make the toaster harder to hit
			toasterSprite.color = new Color(1f, 1f, 1f, 0.2f);
			toasterControl.speed = 6f;
			toasterCollider.radius = 1.0f;

		}
	}
}
