using UnityEngine;
using System.Collections;

public class SpawnRoads : MonoBehaviour {

	public GameObject road;

	const int roadSegCt = 500;

	// Use this for initialization
	void Start () {
		Vector3 pos = transform.position;
		GameObject temp = null;
		bool neg = Random.value>.5;
		for(int i=0; i<roadSegCt; i++){
			if(i%5==0){
				neg = Random.value>.5;
			}
			float offset = Random.value *3;
			if(neg){
				offset*=-1;
			}
			pos = new Vector3(pos.x+offset, pos.y+6, pos.z);
			temp = (GameObject)Instantiate(road, pos, Quaternion.identity);
		}

		pos = new Vector3(pos.x, pos.y+6, pos.z);
		temp = (GameObject)Instantiate(Resources.Load("drivingfinish"), pos, Quaternion.identity);
		temp.name = "finishline";

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
