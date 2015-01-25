using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class BottomMenu : MonoBehaviour {
	
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

	// Use this for initialization
	void Start () {
		invBread.onClick.AddListener(() => GrannyState.instance.eatBread());
		invCandy.onClick.AddListener(() => GrannyState.instance.eatCandy());
	}
	
	// Update is called once per frame
	void Update () {
		hungerSlider.value = GrannyState.instance.currentHunger;
		bloodSlider.value = GrannyState.instance.currentBloodPressure;
		hydrationSlider.value = GrannyState.instance.currentHydration;
		bladderSlider.value = GrannyState.instance.currentBladder;
		
		invMoney.GetComponentInChildren<Text>().text = ""+GrannyState.instance.moneyCt;
		invBread.GetComponentInChildren<Text>().text = ""+GrannyState.instance.breadCt;
		invCandy.GetComponentInChildren<Text>().text = ""+GrannyState.instance.candyCt;
		invDrink.GetComponentInChildren<Text>().text = ""+GrannyState.instance.drinkCt;
		
		invGlasses.gameObject.SetActive(GrannyState.instance.hasGlasses);
		invCane.gameObject.SetActive(GrannyState.instance.hasCane);
		invDentures.gameObject.SetActive(GrannyState.instance.hasDentures);
	}
}
