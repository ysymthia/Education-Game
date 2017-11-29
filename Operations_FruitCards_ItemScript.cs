using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operations_FruitCards_ItemScript : MonoBehaviour{

	public int number;

	public bool isQuestionItem;

	public bool isClicked = false;

	//if user click this item, check if correct or incorrect
	public void onClickItem (){

		if (Operations_GameManager.instance.isSpamming ())
			return;
			
		if (isQuestionItem)
			return;

		if (isClicked == false) {
			isClicked = true;
			Operations_GameManager.instance.checkAnswer (gameObject);

		}

	}

	public void startFailAction ()
	{
		StartCoroutine (FailAction ());//start moving down action
		Destroy (gameObject, 1.5f);
	}

	IEnumerator FailAction ()
	{
		yield return new WaitForSeconds (0.05f);

		// object will rotate clockwise a bit
		gameObject.GetComponent<TweenRotation> ().from = new Vector3 (0, 0, 0);
		gameObject.GetComponent<TweenRotation> ().to = new Vector3 (0, 0, -30);
		gameObject.GetComponent<TweenRotation> ().method = UITweener.Method.EaseIn;
		gameObject.GetComponent<TweenRotation> ().duration = 0.15f;
		gameObject.GetComponent<TweenRotation> ().ResetToBeginning ();
		gameObject.GetComponent<TweenRotation> ().enabled = true;
		yield return new WaitForSeconds (0.15f);

		// object will rotate clockwise a bit
		gameObject.GetComponent<TweenRotation> ().from = new Vector3 (0, 0, -30);
		gameObject.GetComponent<TweenRotation> ().to = new Vector3 (0, 0, 30);
		gameObject.GetComponent<TweenRotation> ().duration = 0.15f;
		gameObject.GetComponent<TweenRotation> ().method = UITweener.Method.EaseInOut;
		gameObject.GetComponent<TweenRotation> ().ResetToBeginning ();
		gameObject.GetComponent<TweenRotation> ().enabled = true;
		yield return new WaitForSeconds (0.15f);

		// object will rotate clockwise a bit
		gameObject.GetComponent<TweenRotation> ().from = new Vector3 (0, 0, 30);
		gameObject.GetComponent<TweenRotation> ().to = new Vector3 (0, 0, -20);
		gameObject.GetComponent<TweenRotation> ().duration = 0.1f;
		gameObject.GetComponent<TweenRotation> ().method = UITweener.Method.EaseInOut;
		gameObject.GetComponent<TweenRotation> ().ResetToBeginning ();
		gameObject.GetComponent<TweenRotation> ().enabled = true;
		yield return new WaitForSeconds (0.1f);

		// object will rotate clockwise a bit
		gameObject.GetComponent<TweenRotation> ().from = new Vector3 (0, 0, -20);
		gameObject.GetComponent<TweenRotation> ().to = new Vector3 (0, 0, 20);
		gameObject.GetComponent<TweenRotation> ().duration = 0.1f;
		gameObject.GetComponent<TweenRotation> ().method = UITweener.Method.EaseInOut;
		gameObject.GetComponent<TweenRotation> ().ResetToBeginning ();
		gameObject.GetComponent<TweenRotation> ().enabled = true;
		yield return new WaitForSeconds (0.1f);

		// object will rotate clockwise a bit
		gameObject.GetComponent<TweenRotation> ().from = new Vector3 (0, 0, 20);
		gameObject.GetComponent<TweenRotation> ().to = new Vector3 (0, 0, 0);
		gameObject.GetComponent<TweenRotation> ().duration = 0.1f;
		gameObject.GetComponent<TweenRotation> ().method = UITweener.Method.EaseInOut;
		gameObject.GetComponent<TweenRotation> ().ResetToBeginning ();
		gameObject.GetComponent<TweenRotation> ().enabled = true;
		yield return new WaitForSeconds (0.1f);

		yield return new WaitForSeconds (0.1f); // wait an extra tick

		// object will rotate clockwise a bit
		gameObject.GetComponent<TweenRotation> ().from = new Vector3 (0, 0, 0);
		gameObject.GetComponent<TweenRotation> ().to = new Vector3 (0, 0, -720);
		gameObject.GetComponent<TweenRotation> ().duration = 0.5f;
		gameObject.GetComponent<TweenRotation> ().method = UITweener.Method.EaseIn;
		gameObject.GetComponent<TweenRotation> ().ResetToBeginning ();
		gameObject.GetComponent<TweenRotation> ().enabled = true;

		// Tween to move wrong answer off-screen (down)
		gameObject.GetComponent<TweenPosition> ().from = gameObject.transform.localPosition;
		gameObject.GetComponent<TweenPosition> ().to = new Vector3 (gameObject.transform.localPosition.x, gameObject.transform.position.y - 500, gameObject.transform.position.z); // move incorrect answer down offscreen
		gameObject.GetComponent<TweenPosition> ().duration = 0.5f;
		gameObject.GetComponent<TweenPosition> ().method = UITweener.Method.EaseIn;
		gameObject.GetComponent<TweenPosition> ().ResetToBeginning ();
		gameObject.GetComponent<TweenPosition> ().PlayForward ();
		gameObject.GetComponent<TweenPosition> ().enabled = true;
		yield return new WaitForSeconds (0.5f);


		Vector2 size = NGUITools.screenSize;

		if (gameObject.transform.localPosition.y < (size.y) * -1)
			Destroy (this.gameObject);
		else
			StartCoroutine (FailAction ());
	}

	public void startSuccessAction (Vector3 target)
	{
		StartCoroutine (success (target)); //if user click correct answer, it moves to outline sprite's position
	}

	IEnumerator success (Vector3 target){

		Vector3	currentScale = gameObject.transform.localScale;

		gameObject.GetComponent<TweenPosition> ().from = gameObject.transform.localPosition;

		gameObject.GetComponent<TweenPosition> ().to = target; // move correct answer to final position -- center screen below black rect
		
		gameObject.GetComponent<TweenPosition> ().ResetToBeginning ();
		gameObject.GetComponent<TweenPosition> ().PlayForward ();



		int ansRandomTween;
		ansRandomTween = Random.Range (1, 6);
		if (ansRandomTween == 1) {
			// object will rotate clockwise one time
			gameObject.GetComponent<TweenRotation> ().from = new Vector3 (0, 0, 0);
			gameObject.GetComponent<TweenRotation> ().to = new Vector3 (0, 0, -360);
			gameObject.GetComponent<TweenRotation> ().duration = 0.6f;
			gameObject.GetComponent<TweenRotation> ().ResetToBeginning ();
			gameObject.GetComponent<TweenRotation> ().enabled = true;
			yield return new WaitForSeconds (0.6f);

		} else if (ansRandomTween == 2) {
			
			// object will rotate counter-clockwise one time
			gameObject.GetComponent<TweenRotation> ().from = new Vector3 (0, 0, -360);
			gameObject.GetComponent<TweenRotation> ().to = new Vector3 (0, 0, 0);
			gameObject.GetComponent<TweenRotation> ().duration = 0.6f;
			gameObject.GetComponent<TweenRotation> ().ResetToBeginning ();
			gameObject.GetComponent<TweenRotation> ().enabled = true;
			yield return new WaitForSeconds (0.6f);

		} else if (ansRandomTween == 3) {
			// object will flip on y axis like this: right side up, upside down, rightside up
			gameObject.GetComponent<TweenScale> ().from = currentScale;
			gameObject.GetComponent<TweenScale> ().to = new Vector3 (currentScale.x, -currentScale.y, 1);
			gameObject.GetComponent<TweenScale> ().duration = 0.3f;
			gameObject.GetComponent<TweenScale> ().ResetToBeginning ();
			gameObject.GetComponent<TweenScale> ().enabled = true;
			yield return new WaitForSeconds (0.3f);

			gameObject.GetComponent<TweenScale> ().from = new Vector3 (currentScale.x, -currentScale.y, 1);
			gameObject.GetComponent<TweenScale> ().to = currentScale;
			gameObject.GetComponent<TweenScale> ().duration = 0.3f;
			gameObject.GetComponent<TweenScale> ().ResetToBeginning ();
			gameObject.GetComponent<TweenScale> ().enabled = true;
			yield return new WaitForSeconds (0.3f);

		} else if (ansRandomTween == 4) {
			// so object will flip on x axis like this: facing correct, facing backwards, facing correct
			gameObject.GetComponent<TweenScale> ().from = currentScale;
			gameObject.GetComponent<TweenScale> ().to = new Vector3 (-currentScale.x, currentScale.y, 1);
			gameObject.GetComponent<TweenScale> ().duration = 0.3f;
			gameObject.GetComponent<TweenScale> ().ResetToBeginning ();
			gameObject.GetComponent<TweenScale> ().enabled = true;
			yield return new WaitForSeconds (0.3f);

			gameObject.GetComponent<TweenScale> ().from = new Vector3 (-currentScale.x, currentScale.y, 1);
			gameObject.GetComponent<TweenScale> ().to = currentScale;
			gameObject.GetComponent<TweenScale> ().duration = 0.3f;
			gameObject.GetComponent<TweenScale> ().ResetToBeginning ();
			gameObject.GetComponent<TweenScale> ().enabled = true;
			yield return new WaitForSeconds (0.3f);
            

		} else if (ansRandomTween == 5) {
			// so object will squash and stretch proportions like this: tall and thin, short and wide, tall and thin, short and wide, normal
			gameObject.GetComponent<TweenScale> ().from = currentScale;
			gameObject.GetComponent<TweenScale> ().to = new Vector3 (currentScale.x * 0.8f, currentScale.y * 1.2f, 1);
			gameObject.GetComponent<TweenScale> ().duration = 0.1f;
			gameObject.GetComponent<TweenScale> ().ResetToBeginning ();
			gameObject.GetComponent<TweenScale> ().enabled = true;
			yield return new WaitForSeconds (0.1f);

			gameObject.GetComponent<TweenScale> ().from = new Vector3 (currentScale.x * 0.8f, currentScale.y * 1.2f, 1);
			gameObject.GetComponent<TweenScale> ().to = new Vector3 (currentScale.x * 1.2f, currentScale.y * 0.8f, 1);
			gameObject.GetComponent<TweenScale> ().duration = 0.125f;
			gameObject.GetComponent<TweenScale> ().ResetToBeginning ();
			gameObject.GetComponent<TweenScale> ().enabled = true;
			yield return new WaitForSeconds (0.125f);

            gameObject.GetComponent<TweenScale>().from = new Vector3(currentScale.x * 1.2f, currentScale.y * 0.8f, 1);
			gameObject.GetComponent<TweenScale> ().to = new Vector3 (currentScale.x * 0.9f, currentScale.y * 1.1f, 1);
			gameObject.GetComponent<TweenScale> ().duration = 0.125f;
			gameObject.GetComponent<TweenScale> ().ResetToBeginning ();
			gameObject.GetComponent<TweenScale> ().enabled = true;
			yield return new WaitForSeconds (0.125f);

			gameObject.GetComponent<TweenScale> ().from = new Vector3 (currentScale.x * 0.9f,currentScale.y *  1.1f, 1);
			gameObject.GetComponent<TweenScale> ().to = new Vector3 (currentScale.x * 1.1f,currentScale.y *  0.9f, 1);
			gameObject.GetComponent<TweenScale> ().duration = 0.125f;
			gameObject.GetComponent<TweenScale> ().ResetToBeginning ();
			gameObject.GetComponent<TweenScale> ().enabled = true;
			yield return new WaitForSeconds (0.125f);

			gameObject.GetComponent<TweenScale> ().from = new Vector3 (currentScale.x *1.1f, currentScale.y * 0.9f, 1);
			gameObject.GetComponent<TweenScale> ().to = currentScale;
			gameObject.GetComponent<TweenScale> ().duration = 0.125f;
			gameObject.GetComponent<TweenScale> ().ResetToBeginning ();
			gameObject.GetComponent<TweenScale> ().enabled = true;
			yield return new WaitForSeconds (0.125f);

		}


	}
}
