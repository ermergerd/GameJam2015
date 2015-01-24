using UnityEngine;
using System.Collections;

public class SpawnRoads : MonoBehaviour {

	public GameObject road;

	// Use this for initialization
	void Start () {
		Vector3 pos = transform.position;
		for(int i=0; i<1000; i++){
			bool neg = Random.value>.5;
			float offset = Random.value *3;
			if(neg){
				offset*=-1;
			}
			pos = new Vector3(pos.x+offset, pos.y+6, pos.z);
			Instantiate(road, pos, Quaternion.identity);

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
