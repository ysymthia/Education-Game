using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour{
	
	public static IEnumerator randomTweenAnimation (GameObject go, Vector3 currentScale, float delay = 0.01f)
	{

		//Debug.Log ("START randomTweenAnimation");
		int ansRandomTween = Random.Range (1, 6);

		yield return new WaitForSeconds (delay);

		while(go.GetComponent<TweenRotation> ().isActiveAndEnabled || go.GetComponent<TweenScale> ().isActiveAndEnabled)
			yield return new WaitForSeconds (.05f);

		yield return new WaitForSeconds (.1f);

		// FIXME for testing
		//ansRandomTween = 6;

		if (ansRandomTween == 1) {
			go.GetComponent<TweenRotation> ().from = new Vector3 (0, 0, 0);
			go.GetComponent<TweenRotation> ().to = new Vector3 (0, 0, -360);
			go.GetComponent<TweenRotation> ().duration = 0.6f;
			go.GetComponent<TweenRotation> ().ResetToBeginning ();
			go.GetComponent<TweenRotation> ().PlayForward ();
			go.GetComponent<TweenRotation> ().enabled = true;
			yield return new WaitForSeconds (0.6f);

		} else if (ansRandomTween == 2) {

			// object will rotate counter-clockwise one time
			go.GetComponent<TweenRotation> ().from = new Vector3 (0, 0, -360);
			go.GetComponent<TweenRotation> ().to = new Vector3 (0, 0, 0);
			go.GetComponent<TweenRotation> ().duration = 0.6f;
			go.GetComponent<TweenRotation> ().ResetToBeginning ();
			go.GetComponent<TweenRotation> ().PlayForward ();
			go.GetComponent<TweenRotation> ().enabled = true;
			yield return new WaitForSeconds (0.6f);

		} else if (ansRandomTween == 3) {
			// object will flip on y axis like this: right side up, upside down, rightside up
			go.GetComponent<TweenScale> ().from = currentScale;
			go.GetComponent<TweenScale> ().to = new Vector3 (currentScale.x, -currentScale.y, 1);
			go.GetComponent<TweenScale> ().duration = 0.3f;
			go.GetComponent<TweenScale> ().ResetToBeginning ();
			go.GetComponent<TweenScale> ().PlayForward ();
			go.GetComponent<TweenScale> ().enabled = true;
			yield return new WaitForSeconds (0.3f);

			go.GetComponent<TweenScale> ().from = new Vector3 (currentScale.x, -currentScale.y, 1);
			go.GetComponent<TweenScale> ().to = currentScale;
			go.GetComponent<TweenScale> ().duration = 0.3f;
			go.GetComponent<TweenScale> ().ResetToBeginning ();
			go.GetComponent<TweenScale> ().PlayForward ();
			go.GetComponent<TweenScale> ().enabled = true;
			yield return new WaitForSeconds (0.3f);

		} else if (ansRandomTween == 4) {
			// so object will flip on x axis like this: facing correct, facing backwards, facing correct
			go.GetComponent<TweenScale> ().from = currentScale;
			go.GetComponent<TweenScale> ().to = new Vector3 (-currentScale.x, currentScale.y, 1);
			go.GetComponent<TweenScale> ().duration = 0.3f;
			go.GetComponent<TweenScale> ().ResetToBeginning ();
			go.GetComponent<TweenScale> ().PlayForward ();
			go.GetComponent<TweenScale> ().enabled = true;
			yield return new WaitForSeconds (0.3f);

			go.GetComponent<TweenScale> ().from = new Vector3 (-currentScale.x, currentScale.y, 1);
			go.GetComponent<TweenScale> ().to = currentScale;
			go.GetComponent<TweenScale> ().duration = 0.3f;
			go.GetComponent<TweenScale> ().ResetToBeginning ();
			go.GetComponent<TweenScale> ().PlayForward ();
			go.GetComponent<TweenScale> ().enabled = true;
			yield return new WaitForSeconds (0.3f);


		} else if (ansRandomTween == 5) {
			// so object will squash and stretch proportions like this: tall and thin, short and wide, tall and thin, short and wide, normal
			go.GetComponent<TweenScale> ().from = currentScale;
			go.GetComponent<TweenScale> ().to = new Vector3 (currentScale.x * 0.8f, currentScale.y * 1.2f, 1);
			go.GetComponent<TweenScale> ().duration = 0.1f;
			go.GetComponent<TweenScale> ().ResetToBeginning ();
			go.GetComponent<TweenScale> ().enabled = true;
			yield return new WaitForSeconds (0.1f);

			go.GetComponent<TweenScale> ().from = new Vector3 (currentScale.x * 0.8f, currentScale.y * 1.2f, 1);
			go.GetComponent<TweenScale> ().to = new Vector3 (currentScale.x * 1.2f, currentScale.y * 0.8f, 1);
			go.GetComponent<TweenScale> ().duration = 0.125f;
			go.GetComponent<TweenScale> ().ResetToBeginning ();
			go.GetComponent<TweenScale> ().PlayForward ();
			go.GetComponent<TweenScale> ().enabled = true;
			yield return new WaitForSeconds (0.125f);

			go.GetComponent<TweenScale> ().from = new Vector3 (currentScale.x * 1.2f, currentScale.y * 0.8f, 1);
			go.GetComponent<TweenScale> ().to = new Vector3 (currentScale.x * 0.9f, currentScale.y * 1.1f, 1);
			go.GetComponent<TweenScale> ().duration = 0.125f;
			go.GetComponent<TweenScale> ().ResetToBeginning ();
			go.GetComponent<TweenScale> ().PlayForward ();
			go.GetComponent<TweenScale> ().enabled = true;
			yield return new WaitForSeconds (0.125f);

			go.GetComponent<TweenScale> ().from = new Vector3 (currentScale.x * 0.9f, currentScale.y * 1.1f, 1);
			go.GetComponent<TweenScale> ().to = new Vector3 (currentScale.x * 1.1f, currentScale.y * 0.9f, 1);
			go.GetComponent<TweenScale> ().duration = 0.125f;
			go.GetComponent<TweenScale> ().ResetToBeginning ();
			go.GetComponent<TweenScale> ().PlayForward ();
			go.GetComponent<TweenScale> ().enabled = true;
			yield return new WaitForSeconds (0.125f);

			go.GetComponent<TweenScale> ().from = new Vector3 (currentScale.x * 1.1f, currentScale.y * 0.9f, 1);
			go.GetComponent<TweenScale> ().to = currentScale;
			go.GetComponent<TweenScale> ().duration = 0.125f;
			go.GetComponent<TweenScale> ().ResetToBeginning ();
			go.GetComponent<TweenScale> ().PlayForward ();
			go.GetComponent<TweenScale> ().enabled = true;
			yield return new WaitForSeconds (0.125f);
		
		} else if (ansRandomTween == 6) {

			int randRotation = Random.Range (0, 2) == 0 ? 1 : -1;

			for (int i = 1; i <= 3; i++) {
				
				go.GetComponent<TweenRotation> ().from.z = 360 * randRotation;
				go.GetComponent<TweenRotation> ().to.z = 0;
				go.GetComponent<TweenRotation> ().method = UITweener.Method.EaseOut;
				go.GetComponent<TweenRotation> ().delay = .1f;
				go.GetComponent<TweenRotation> ().duration = 0.6f;
				go.GetComponent<TweenRotation> ().ResetToBeginning ();
				go.GetComponent<TweenRotation> ().PlayForward ();
				go.GetComponent<TweenRotation> ().enabled = true;
				yield return new WaitForSeconds (0.6f);
			}


		}

	}
}