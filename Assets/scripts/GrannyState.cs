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

	public int moneyCt;
	public int breadCt;
	public int candyCt;
	public int drinkCt;
	public bool hasGlasses;
	public bool hasDentures;
	public bool hasCane;
	//TODO meds

	public int currentHunger; //die if 0, goes up over time
	public int currentBloodPressure; //die if 100, goes up on harm events
	public int currentBladder; //die if 100, goes up over time if hydrated
	public int currentHydration; //die if 0, goes down over time

	private bool beatHouse1 = false;
	private bool beatHouse2 = false;
	private bool beatDriving = false;
	private bool beatBingo = false;
	private bool beatStore = false;

	void initGranny(){
		moneyCt = 10;
		breadCt = 0;
		candyCt = 1;
		drinkCt = 0;
		hasGlasses = false;
		hasDentures = false;
		hasCane = false;

		currentHunger = 10;
		currentBloodPressure = 20;
		currentBladder = 90;
		currentHydration = 50;
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

	}

	private void grannyDeath(){
		//game over
		Application.Quit();
		Debug.Break();
	}

	public void eatBread(){
		if(breadCt>0){
			currentHunger-=20;
			breadCt--;
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
		if(!beatHouse2){
			beatHouse1 = true;
			Application.LoadLevel("kitchen");
		}else if(!beatDriving){
			beatHouse2 = true;
			Application.LoadLevel("driving");
		}else if(!beatBingo){
			beatDriving = true;
			Application.LoadLevel("bingo_scene");
		}else if(!beatStore){
			beatBingo = true;
			Application.LoadLevel("shop");
		}else{
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
