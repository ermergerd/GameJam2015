using UnityEngine;
using System.Collections;

public class GrannyState : MonoBehaviour {

	private static GrannyState _instance;

	public static GrannyState instance{
		get{
			if(_instance==null){
				_instance = GameObject.FindObjectOfType<GrannyState>();

				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}

	void Awake() 
	{
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(this.gameObject);
		}
	}



	const float healthInterval = 5;

	[System.NonSerialized]
	public int moneyCt;

	[System.NonSerialized]
	public int breadCt;

	[System.NonSerialized]
	public int candyCt;

	[System.NonSerialized]
	public int drinkCt;

	[System.NonSerialized]
	public int medsCt;

	[System.NonSerialized]
	public bool hasGlasses;

	[System.NonSerialized]
	public bool hasDentures;

	[System.NonSerialized]
	public bool hasCane;
	//TODO meds

	[System.NonSerialized]
	public int currentHunger; //die if 0, goes up over time

	[System.NonSerialized]
	public int currentBloodPressure; //die if 100, goes up on harm events

	[System.NonSerialized]
	public int currentBladder; //die if 100, goes up over time if hydrated

	[System.NonSerialized]
	public int currentHydration; //die if 0, goes down over time

	private bool beatHouse = false;
	private bool beatDriving = false;
	private bool beatBingo = false;
	private bool beatStore = false;

	void initGranny(){
		moneyCt = 10;
		breadCt = 0;
		candyCt = 1;
		drinkCt = 0;
		medsCt = 0;
		hasGlasses = false;
		hasDentures = false;
		hasCane = false;

		currentHunger = 10;
		currentBloodPressure = 20;
		currentBladder = 70;
		currentHydration = 60;
	}

	private GrannyState(){
//		initGranny();
//		InvokeRepeating("grannyOneHealthInterval", healthInterval, healthInterval);
	}

	// Use this for initialization
	void Start () {
		Debug.Log("GrannyState Start");
		initGranny();
		InvokeRepeating("grannyOneHealthInterval", healthInterval, healthInterval);

	}
	
	// Update is called once per frame
	void Update () {

	}

	private void grannyOneHealthInterval(){
		Debug.Log("grannyOneHealthInterval");
		if(Application.loadedLevelName=="bingo_scene"){
			return;
		}
		currentHunger++;

		//hydration/bladder stuff
		if(currentHydration>=50){
			currentBladder++;
		}

		currentHydration--;

		grannyCheckDeath();

	}

	private void grannyCheckDeath(){
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

		if(currentBloodPressure<=0){
			grannyWin();
			return;
		}

	}

	private void grannyDeath(){
		//game over
		Application.LoadLevel("gameover");
	}

	private void grannyWin(){
		Application.LoadLevel("gamewin");
	}

	public void eatBread(){
		if(breadCt>0){
			currentHunger-=20;
			breadCt--;
		}
	}

	public void takeMeds(){
		if(medsCt>0){
			currentBloodPressure-=20;
			medsCt--;
		}
	}

	public void eatCandy(){
		if(candyCt>0){
			currentHunger-=5;
			candyCt--;
		}
	}

	public void drink(){
		if(drinkCt>0){
			currentHydration+=40;
			drinkCt--;
		}
	}

	public void loadNextLevel(){
		if(!beatHouse){
			beatHouse = true;
			Application.LoadLevel("driving");
		}else if(!beatDriving){
			beatDriving = true;
			Application.LoadLevel("bingo_scene");
		}else if(!beatBingo){
			beatBingo = true;
			Application.LoadLevel("shop");
		}else{ //if(!beatStore){

			beatStore = true;
			//random level!
			float numLevels = 4;
			float l = Random.value*4;

			if(l<1)
				Application.LoadLevel("wakeup_scene");
			else if(l<2)
				Application.LoadLevel("driving");
			else if(l<3)
				Application.LoadLevel("bingo_scene");
			else if(l<4)
				Application.LoadLevel("shop");


		}
	}
}
