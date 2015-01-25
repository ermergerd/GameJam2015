using UnityEngine;
using System.Collections;

public class StoreExit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver(){
		if(Input.GetButtonDown("Fire1")){
			GrannyState.instance.loadNextLevel();
		}

	}

}
