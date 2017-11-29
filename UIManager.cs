using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour { 

	public static int gameSelected = -1;

	private static UIManager _instance = null;

	public static UIManager instance {

		get {
			return _instance;
		}
	}

	public GameObject LevelSelection;
	public GameObject Operation_Panel;
	public GameObject Geometry_Panel;
	public GameObject TopPanel;

	private GameObject buttonsParent;
	private GameObject linesParent;
	private GameObject backBtn;
	private UILabel points_label;
	private GameObject newLevelUnlockText;

	private int counterForLines = 0;
	// Use this for initialization

	void FixedUpdate(){

		if (Input.GetKeyDown (KeyCode.Q))
			PlayerPrefs.DeleteAll ();
	}

	void Start () {
		_instance = this;


		buttonsParent = LevelSelection.transform.GetChild (0).gameObject;
		linesParent = LevelSelection.transform.GetChild (1).gameObject;

		newLevelUnlockText = TopPanel.transform.GetChild (3).gameObject;
		newLevelUnlockText.SetActive (false);
		backBtn = TopPanel.transform.GetChild (0).gameObject;
		backBtn.SetActive (false);
		points_label = TopPanel.transform.GetChild (1).GetComponent<UILabel>();
		incrementNumberOfTotalCoins (0);


		for (int i = 0; i < buttonsParent.transform.childCount; i++) {
			buttonsParent.transform.GetChild (i).GetComponent<UISprite> ().spriteName = "frame_level_lock";
			buttonsParent.transform.GetChild (i).GetChild (0).gameObject.SetActive (false);
		}

		for (int i = 0; i < linesParent.transform.childCount; i++) {
			linesParent.transform.GetChild (i).GetComponent<UISprite> ().spriteName = "process_line_01_n";
		}


		buttonsParent.transform.GetChild (0).GetComponent<UISprite> ().spriteName = "";
		buttonsParent.transform.GetChild (0).GetChild (0).gameObject.SetActive (true);

		for (int i = 1; i < 5; i++) {

			if (PlayerPrefs.GetInt ("Level" + i, 0) == 1) {

				buttonsParent.transform.GetChild (i).GetComponent<UISprite> ().spriteName = "";
				buttonsParent.transform.GetChild (i).GetChild (0).gameObject.SetActive (true);

				if (i == 1 || i == 2 || i == 4) {
					linesParent.transform.GetChild (counterForLines).GetComponent<UISprite> ().spriteName = "process_line_01_f";

					counterForLines++;
				}

			} else
				break;

		}

	}

	public void incrementNumberOfTotalCoins(int value = 1){

		int totalPoints =	PlayerPrefs.GetInt ("totalPoints", 0);
		totalPoints += value;
		PlayerPrefs.SetInt ("totalPoints", totalPoints);

		points_label.text = totalPoints.ToString ();
	}

	public int incrementNumberOfPointsThisLevel(){

		int currentPointsOfThisLevel =	PlayerPrefs.GetInt ("Level" + UIManager.gameSelected + " number of points", 0);
		currentPointsOfThisLevel++;
		PlayerPrefs.SetInt ("Level" + UIManager.gameSelected + " number of points", currentPointsOfThisLevel);
		return currentPointsOfThisLevel;
	}

	public void backBtnClicked(){

		Destroy (currentGamePanel);
		LevelSelection.SetActive (true);
		backBtn.SetActive (false);

	}

	public void unlockNewLevel(int unlockLevel){

		if (unlockLevel >= 5)
			return;

		// Debug.Log (PlayerPrefs.GetInt ("Level" + unlockLevel, 0));
		if (PlayerPrefs.GetInt ("Level" + unlockLevel, 0) == 1)
			return;

		PlayerPrefs.SetInt ("Level" + unlockLevel, 1);

		Debug.Log ("new level unlocked!");

		StartCoroutine (newLevelUnlockedAnim ());

		buttonsParent.transform.GetChild (unlockLevel).GetComponent<UISprite> ().spriteName = "";
		buttonsParent.transform.GetChild (unlockLevel).GetChild (0).gameObject.SetActive (true);

		PlayerPrefs.SetInt ("Level" + unlockLevel, 1);

		if (unlockLevel == 1 || unlockLevel == 2 || unlockLevel == 4) {
			linesParent.transform.GetChild (counterForLines).GetComponent<UISprite> ().spriteName = "process_line_01_f";

			counterForLines++;
		}
	}

	IEnumerator newLevelUnlockedAnim(){


		newLevelUnlockText.SetActive (true);
		/*newLevelUnlockText.GetComponent<TweenAlpha> ().from = 0;
		newLevelUnlockText.GetComponent<TweenAlpha> ().to = 1;
		newLevelUnlockText.GetComponent<TweenAlpha> ().delay = 0;
		newLevelUnlockText.GetComponent<TweenAlpha> ().duration = 1f;*/
		newLevelUnlockText.GetComponent<TweenAlpha> ().PlayForward ();

		yield return new WaitForSeconds (2f);
		newLevelUnlockText.GetComponent<TweenAlpha> ().PlayReverse ();
	}

	public GameObject currentGamePanel;

	public void btnClicked(GameObject btn){

		if (btn.GetComponent<UISprite> ().spriteName == "frame_level_lock")
			return;

		backBtn.SetActive (true);
		gameSelected = int.Parse (btn.GetComponentInChildren<UILabel>().text) - 1;

		LevelSelection.SetActive (false);

		if (gameSelected != 2) 
			currentGamePanel =	NGUITools.AddChild (transform.GetChild (0).gameObject, Operation_Panel);
		else 
			currentGamePanel=	NGUITools.AddChild (transform.GetChild (0).gameObject, Geometry_Panel);
		
		currentGamePanel.SetActive (true);
	}

}
