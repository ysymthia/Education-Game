using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCard_Object { 

	private int fruitSprite;
	public void setFruitSprite(int fruitSprite){
		this.fruitSprite = fruitSprite;
	}

	public void useFruitCard (GameObject obj, int number, string spriteName){

		if(number > 10)
			Debug.LogError ("useFruitCard error! number to big! --> " + number);
		else if(number < 0)
			Debug.LogError ("useFruitCard error! negative number! --> " + number);

		obj.SetActive (true);

		obj.transform.GetChild (2).GetChild (number).gameObject.SetActive (true);
		obj.transform.GetChild (3).GetComponent<UILabel> ().text = number.ToString ();

		// change sprite of fruit objects
		for (int i = 0; i < obj.transform.GetChild (2).GetChild (number).childCount; i++) {
			Utilities.setTextOutline_DisplayedOnSprite (obj.transform.GetChild (2).GetChild (number).GetChild (i).gameObject, spriteName, fruitSprite);
		}

	}




}
