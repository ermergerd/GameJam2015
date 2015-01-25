using UnityEngine;
using System.Collections;

public class KitchenController : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GameObject toaster = GameObject.Find("Toaster");
		SpriteRenderer toasterSprite = toaster.GetComponent<SpriteRenderer>();
		ToasterController toasterControl = toaster.GetComponent<ToasterController>();

		if (GrannyState.instance.hasGlasses == true) {
			Debug.Log ("Granny got her glasses, hide em!!!");
			Destroy(GameObject.Find ("kitchen-blur"));

			toasterSprite.color = new Color(1f, 1f, 1f, 1f);
			toasterControl.speed = 3f;

		} else {
			
			toasterSprite.color = new Color(1f, 1f, 1f, 0.3f);
			toasterControl.speed = 6f;

		}
	}
}
