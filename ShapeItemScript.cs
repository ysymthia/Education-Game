using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeItemScript : MonoBehaviour
{
    public int GameType;
    public int ShapeType = -1;

    public bool isStop;//for idle action

    float idleDelayTime;
    private IEnumerator idleMainCoroutine;

    public float scale = 1;

	bool isClicked = false;

    void Start() {
    }

	public void onTouchClock(){
		if (isClicked == false) {

			isClicked = true;
			
		}


    }
}
