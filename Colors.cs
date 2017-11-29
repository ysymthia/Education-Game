using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors{

	private static Color[] BackgroundColors = {
		new Color (0.95f, 0.5f, 0.9f), // pink
		
	};

	private static Color[] RoundRectColors = { 
		new Color (.55f, .05f, 0.9f), // purple 
	};


	public static Color generateRandomBackgroundColor ()
	{

		return BackgroundColors [Random.Range (0, BackgroundColors.Length)];
	}

	public static Color generateRandomRoundRectColor ()
	{

	return RoundRectColors [Random.Range (0, RoundRectColors.Length)];
	}


}
