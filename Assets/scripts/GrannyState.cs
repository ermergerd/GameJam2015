using UnityEngine;
using UnityEngine.UI;
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

	public Slider hungerSlider;
	public Slider bloodSlider;
	public Slider bladderSlider;
	public Slider hydrationSlider;
	//public GridLayoutGroup inventory;
	public Button invMoney;
	public Button invBread;
	public Button invCandy;
	public Button invDrink;
	public Button invGlasses;
	public Button invDentures;
	public Button invCane;
	//public Button invMoney;


	const float healthInterval = 5;

	public static int moneyCt;
	public static int breadCt;
	public static int candyCt;
	public static int drinkCt;
	public static bool hasGlasses;
	public static bool hasDentures;
	public static bool hasCane;
	//TODO meds

	public static int currentHunger; //die if 0, goes up over time
	public static int currentBloodPressure; //die if 100, goes up on harm events
	public static int currentBladder; //die if 100, goes up over time if hydrated
	public static int currentHydration; //die if 0, goes down over time

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

	public 

	// Use this for initialization
	void Start () {
		Debug.Log("GrannyState Start");
		initGranny();
		InvokeRepeating("grannyOneHealthInterval", healthInterval, healthInterval);


	}
	
	// Update is called once per frame
	void Update () {
		hungerSlider.value = currentHunger;
		bloodSlider.value = currentBloodPressure;
		hydrationSlider.value = currentHydration;
		bladderSlider.value = currentBladder;

		invMoney.GetComponentInChildren<Text>().text = ""+moneyCt;
		invBread.GetComponentInChildren<Text>().text = ""+breadCt;
		invCandy.GetComponentInChildren<Text>().text = ""+candyCt;
		invDrink.GetComponentInChildren<Text>().text = ""+drinkCt;

		invGlasses.gameObject.SetActive(hasGlasses);
		invCane.gameObject.SetActive(hasCane);
		invDentures.gameObject.SetActive(hasDentures);

	}

	private void grannyOneHealthInterval(){
		Debug.Log("grannyOneHealthInterval");
		currentHunger++;

		//hydration/bladder stuff
		if(currentHydration>50){
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
	}

	public static void eatBread(){
		if(breadCt>0){
			currentHunger-=20;
			breadCt--;
		}
	}

	public static void eatCandy(){
		if(candyCt>0){
			currentHunger-=5;
			candyCt--;
		}
	}

	public static void drink(){
		if(drinkCt>0){
			currentHydration+=40;
			drinkCt--;
		}
	}

}
