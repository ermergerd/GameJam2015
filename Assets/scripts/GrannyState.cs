using UnityEngine;
using System.Collections;

public class GrannyState : MonoBehaviour {

	int moneyCt;
	int breadCt;
	int candyCt;
	int drinkCt;
	bool hasGlasses;
	bool hasDentures;
	bool hasCane;
	//TODO meds

	int currentHunger; //die if 0, goes up over time
	int currentBloodPressure; //die if 100, goes up on harm events
	int currentBladder; //die if 100, goes up over time if hydrated
	int currentHydration; //die if 0, goes down over time

	void initGranny(){
		moneyCt = 10;
		breadCt = 0;
		candyCt = 0;
		drinkCt = 0;
		hasGlasses = false;
		hasDentures = false;
		hasCane = false;

		currentHunger = 10;
		currentBloodPressure = 20;
		currentBladder = 90;
		currentHydration = 50;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
