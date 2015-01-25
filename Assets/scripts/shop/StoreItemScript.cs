using UnityEngine;
using System.Collections;

public class StoreItemScript : MonoBehaviour {

	public Sprite breadSprite;
	public Sprite candySprite;
	public Sprite caneSprite;
	public Sprite drinkSprite;
	public Sprite dentureSprite;
	public Sprite glassesSprite;

	public TextMesh buttonText;

	enum StoreItem{
		bread,
		drink,
		cane,
		glasses,
		candy,
		dentures,
	}

	private StoreItem type;
	private int price;

	// Use this for initialization
	void Start () {
		//randomly determine what item
		type = GetRandomEnum<StoreItem>();
		//randomly determine cost
		price = (int)(Random.value * 15f);
		if(price==0)
			price = 1;
		//update sprite
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		switch(type){
		case StoreItem.bread:
			sr.sprite = breadSprite;
			break;
		case StoreItem.candy:
			sr.sprite = candySprite;
			break;
		case StoreItem.cane:
			sr.sprite = caneSprite;
			break;
		case StoreItem.drink:
			sr.sprite = drinkSprite;
			break;
		case StoreItem.glasses:
			sr.sprite = glassesSprite;
			break;
		case StoreItem.dentures:
			sr.sprite = dentureSprite;
			break;

		}
		//show price
		buttonText.text = "$"+price;
		buttonText.renderer.sortingOrder = 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver(){
		if(Input.GetButtonDown("Fire1")){
			if(GrannyState.instance.moneyCt >= price){
				//TODO $$ sound

				switch(type){
				case StoreItem.bread:
					GrannyState.instance.breadCt++;
					break;
				case StoreItem.candy:
					GrannyState.instance.candyCt++;
					break;
				case StoreItem.drink:
					GrannyState.instance.drinkCt++;
					break;
				case StoreItem.cane:
					GrannyState.instance.hasCane = true;
					break;
				case StoreItem.glasses:
					GrannyState.instance.hasGlasses = true;
					break;
				case StoreItem.dentures:
					GrannyState.instance.hasDentures = true;
					break;
				}
				GrannyState.instance.moneyCt-=price;

				Destroy(gameObject);
			}
		}
	}

	static T GetRandomEnum<T>()
	{
		System.Array A = System.Enum.GetValues(typeof(T));
		T V = (T)A.GetValue(UnityEngine.Random.Range(0,A.Length));
		return V;
	}
}
