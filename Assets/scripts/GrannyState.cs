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
		initGranny();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void grannyOneHealthInterval(){

		currentHunger++;

		//hydration/bladder stuff
		if(currentHydration>50){
			currentBladder++;
		}

		currentHydration--;

		grannyCheckDeath();

	}

	void grannyCheckDeath(){
		if(currentHunger>=100){
			grannyDeath();
			return;
		}
		if(currentBladder>=100){
			grannyDeath();
			return;
		}
		if(currentBloodPressure>=100){
			grannyDeath();
			return;
		}
		if(currentHydration<=0){
			grannyDeath();
			return;
		}

	}

	void grannyDeath(){
		//game over
	}

	void eatBread(){
		if(breadCt>0){
			currentHunger-=20;
			breadCt--;
		}
	}

	void eatCandy(){
		if(candyCt>0){
			currentHunger-=5;
			candyCt--;
		}
	}

	void drink(){
		if(drinkCt>0){
			currentHydration+=40;
			drinkCt--;
		}
	}

}
