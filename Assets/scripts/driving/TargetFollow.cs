using UnityEngine;
using System.Collections;

public class TargetFollow : MonoBehaviour {
	
	public Transform Target;
	public float DistanceFromTarget;
	public float OffsetY;

	// Use this for initialization
	void Start () {
		//transform.Rotate(new Vector3(180,0,0));
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Target.position + new Vector3(0, 0, DistanceFromTarget);
		transform.LookAt(Target);
		transform.position = new Vector3(transform.position.x, transform.position.y+OffsetY, transform.position.z);


	}
}
