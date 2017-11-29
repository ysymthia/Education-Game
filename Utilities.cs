using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Utilities : MonoBehaviour {

	private static string[] NumberWords = {
		"zil",
		"one",
		"two",
		"three",
		"four",
		"five",
		"six",
		"sev",
		"eight",
		"nine",
		"ten"
	};

	public static string makeNumberWord (int number){
		if (number > 10)
			Debug.LogError (number + " is too big!");
		return NumberWords [number];
	}

	public static void destroyAllObjectsWithTagName (params string[] list){

		GameObject[] array;
		for (int i = 0; i < list.Length; i++) {

			try{
				
			array = GameObject.FindGameObjectsWithTag (list[i]);
			foreach (GameObject obj in array)
				Destroy (obj);

			}catch(UnityException e){

			}
		}
	}

	public static void setTextOutline_DisplayedOnSprite (GameObject child, string spriteName, int fruitSprite = -1){

		child.GetComponent<UISprite> ().spriteName = spriteName;
		if (child.transform.childCount > 0) {
			child.transform.GetChild (0).localPosition = Vector3.zero;
			child.transform.GetChild (0).GetComponent<UILabel> ().effectStyle = UILabel.Effect.Outline;
			child.transform.GetChild (0).GetComponent<UILabel> ().effectDistance = new Vector2 (2f, 2f);

			if (fruitSprite == 0) {
				Vector3 myPos = child.transform.GetChild (0).GetComponent<UILabel> ().transform.localPosition;
				myPos.y -= 20f;
				child.transform.GetChild (0).GetComponent<UILabel> ().transform.localPosition = myPos;
			}
		}
			
	}



}
