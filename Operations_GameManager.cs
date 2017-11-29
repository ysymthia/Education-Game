using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;
using NCalc;

public class Operations_GameManager : MonoBehaviour
{
    
	public static Operations_GameManager instance;

	[Header ("Background")]
	public GameObject Background;

	[Header ("Title label")]
	public GameObject QuestionLabel;

	public GameObject mathematicalExpressionLabel;

	[Header ("Question position")]
	public GameObject QuestionPos_FruitCard;
	public GameObject QuestionPos_NumberGame;


	[Header ("Answer position")]
	public GameObject AnswerPositions_FruitCard;
	public GameObject AnswerPositions_NumberGame;

	[Header ("Blank item")]
	public GameObject BlankItem_Parent;

	// blank sprite position
	private GameObject blankItem_Placement;


	private string spriteName;

	private int fruitSprite;

	private string[] fruit_sprite_name = { 
		"apple",			
		"banana",		
		"orange",			
		"strawberry", 	
		"sun",					
		"bubble_green",		
		"bubble_blue"		
	};

	[Header ("Fruit card")]
	public GameObject FruitCard_Parent;


	public GameObject operatorsParent;

	public GameObject NumberGame_AnswerItem;

	private int QuestionOperator;


	private int GameType;
	// 0 --> number card or fruit card game
	// 1 --> numbers game


	private int numeral1;
	private int numeral2;
	private int numeral_result;


	// determinate if equal is on right --> 0 or left --> 1
	private int randomResultPos;

	// correct answer can be any numeral --> 1, 2 or 3
	private int correct_answer;

	private bool noSpamming;

	public bool isSpamming ()
	{
		return noSpamming;	
	}

	private int minNumber = 1;
	private int maxNumber = 11;

	private int display_0Fruit_1Number_2NumberWords_3NumberAndNumberWords_OnCard;

	private FruitCard_Object fruitCard;

	private GameObject useForPositioningOperators;
	private int blank_in_question_location;

	public string[] dateGame = {
		"How many days are in January?", 
		"How many days are in February (in leap year)?", 
		"How many days are in February (in common year)?", 
		"How many days are in March?", 
		"How many days are in April?", 
		"How many days are in May?", 
		"How many days are in June?", 
		"How many days are in July?", 
		"How many days are in August?", 
		"How many days are in September?", 
		"How many days are in October?", 
		"How many days are in November?", 
		"How many days are in December?",
		"How many months are in year?"

	};

	public int[] dateAnswer = {
		31, 
		29,
		28, 
		31,
		30,
		31,
		30,
		31,
		31,
		30,
		31,
		30,
		31,
		12
	};


	void Start ()
	{

		instance = this;

		fruitCard = new FruitCard_Object ();


		StartCoroutine (startNewGameDelay (0.0001f));


	}

	private void setFruitSprite ()
	{
		// set sprite for fruit_card
		fruitSprite = Random.Range (0, fruit_sprite_name.Length);

		// don't use sun sprite for this game
		while (fruitSprite == 4)
			fruitSprite = Random.Range (0, fruit_sprite_name.Length);

		fruitCard.setFruitSprite (fruitSprite);

		spriteName = fruit_sprite_name [fruitSprite];
	}

	IEnumerator startNewGameDelay (float delay)
	{
		

		// disallow users from spamming
		noSpamming = true;

		yield return new WaitForSeconds (delay);
		Utilities.destroyAllObjectsWithTagName ("numberitem", "fruititem", "outlineitem");

		// set bg color
		Background.GetComponent<UISprite> ().color = Colors.generateRandomBackgroundColor ();
		mathematicalExpressionLabel.SetActive (false);
		setFruitSprite ();
		QuestionLabel.GetComponent<UILabel> ().fontSize = 30;
		minNumber = 1;
		maxNumber = 11;

		GameType = Random.Range (0, 4); 
		// 0 --> number card or fruit card game
		// 1 --> numbers game
		// 2 --> matemathical expressions
		// 3 --> days and months

		// FIXME for testing
		// GameType = 3;

		QuestionOperator = Random.Range (0, 3);
		// 0 --> addition
		// 1 --> subtraction
		// 2 --> multiply

		// FIXME for testing
		// QuestionOperator = 2;

		if (UIManager.gameSelected == 0) {
			GameType = Random.Range (0, 2);
			QuestionOperator = Random.Range (0, 2);

		} else if (UIManager.gameSelected == 1) {
			GameType = Random.Range (0, 2);
			QuestionOperator = 2;

		}else if (UIManager.gameSelected == 3) {
			GameType = 2;

		}else if (UIManager.gameSelected == 4) {
			GameType = 3;

		}


		display_0Fruit_1Number_2NumberWords_3NumberAndNumberWords_OnCard = Random.Range (0, 4);



		if (QuestionOperator == 2 || GameType == 1)
			display_0Fruit_1Number_2NumberWords_3NumberAndNumberWords_OnCard = 1;

		if (GameType == 0)
			startNewGame_GameType0 ();
		else if (GameType == 1)
			startNewGame_GameType1 ();
		else if (GameType == 2)
			startNewGame_GameType2 ();
		else if (GameType == 3)
			startNewGame_GameType3 ();
		

		yield return new WaitForSeconds (1.5f);
		noSpamming = false;
	}

	/////////////////////////////////////////////////////////
	///////////// ADD SUB MULT  /////////////////////////////
	/////////////////////////////////////////////////////////

	private void generateOperator_Numeral_1_2_3Result ()
	{
		
		////////////////////////////////////////////////////
		///////////// ADD and SUB //////////////////////////
		////////////////////////////////////////////////////

		int counterForInfinityLoop = 0;

		if (QuestionOperator == 0 || QuestionOperator == 1) {

			while (true) {

				counterForInfinityLoop++;

				numeral1 = Random.Range (minNumber, maxNumber); 
				numeral2 = Random.Range (minNumber, maxNumber);

				if (numeral1 + numeral2 < maxNumber)
					break;

				if (counterForInfinityLoop > 100) {
					Debug.LogError ("INFINITY LOOP HERE");
					break;
				}
			}


			if (Random.Range (0, 2) == 0) {
				int swap = numeral1;
				numeral1 = numeral2;
				numeral2 = swap;
			}

			numeral_result = numeral1 + numeral2;	

			// if substraction
			if (QuestionOperator == 1) {

				numeral1 = numeral_result;
				numeral_result = numeral1 - numeral2;	

				if (Random.Range (0, 2) == 0) {
					int swap = numeral_result;
					numeral_result = numeral2;
					numeral2 = swap;
				}
			}

            //Multiply

		} else if (QuestionOperator == 2) {

			numeral1 = Random.Range (1, 11);
			numeral2 = Random.Range (1, 11);

			// swapping values of numeral1 and numeral2
			if (Random.Range (0, 2) == 0) {
				int swap = numeral1;
				numeral1 = numeral2;
				numeral2 = swap;
			}


			numeral_result = numeral1 * numeral2;	

		} 

	}

	private void setInstructionLabelText ()
	{
		
		if (QuestionOperator == 0)
			QuestionLabel.GetComponent<UILabel> ().text = "Addition";
		else if (QuestionOperator == 1)
			QuestionLabel.GetComponent<UILabel> ().text = "Subtraction";
		else if (QuestionOperator == 2)
			QuestionLabel.GetComponent<UILabel> ().text = "Multiply";
		
	}


	private void positionQuestionItem_GameType0 ()
	{

		randomResultPos = Random.Range (0, 2);	//set sum show type

		useForPositioningOperators = QuestionPos_FruitCard;


		// equal sign
		GameObject equal = NGUITools.AddChild (gameObject, operatorsParent.transform.GetChild (3).gameObject);
		equal.SetActive (true);
		equal.transform.localScale = Vector3.one * .8f;

		//////////////////////////////////////////////////////
		///////////// POSITION EQUALS SIGN LR ////////////////
		//////////////////////////////////////////////////////

		GameObject questionOperator = NGUITools.AddChild (gameObject, operatorsParent.transform.GetChild (QuestionOperator).gameObject);
		questionOperator.SetActive (true);
		questionOperator.transform.localScale = Vector3.one * .8f;

		// display equals sign on left --> result = num1 +-*/ num2 
		if (randomResultPos == 0) {
			equal.transform.localPosition = useForPositioningOperators.transform.GetChild (3).transform.localPosition;
			questionOperator.transform.localPosition = useForPositioningOperators.transform.GetChild (4).transform.localPosition;
		}

		// display equals sign on right -- > num1 +-*/ num2 = result
		else if (randomResultPos == 1) {
			questionOperator.transform.localPosition = useForPositioningOperators.transform.GetChild (3).transform.localPosition;
			equal.transform.localPosition = useForPositioningOperators.transform.GetChild (4).transform.localPosition;
		}




	}

	private void positionCorrectAnswerOutline_GameType0 ()
	{

		blank_in_question_location = Random.Range (0, 3);


		// display equals sign on left --> result = num1 +-*/ num2 
		if (randomResultPos == 0) {

			if (blank_in_question_location == 0)
				correct_answer = numeral_result;
			else if (blank_in_question_location == 1)
				correct_answer = numeral1;
			else if (blank_in_question_location == 2)
				correct_answer = numeral2;


			// display equals sign on right -- > num1 +-*/ num2 = result
		} else if (randomResultPos == 1) {

			if (blank_in_question_location == 0)
				correct_answer = numeral1;
			else if (blank_in_question_location == 1)
				correct_answer = numeral2;
			else if (blank_in_question_location == 2)
				correct_answer = numeral_result;

		}

		blankItem_Placement = NGUITools.AddChild (gameObject, BlankItem_Parent.transform.GetChild (0).gameObject);
		blankItem_Placement.transform.localPosition = useForPositioningOperators.transform.GetChild (blank_in_question_location).localPosition;
		blankItem_Placement.GetComponent<UISprite> ().color = Color.white;	
		blankItem_Placement.SetActive (true);

	}

	private void createQuestionItems_GameType0 ()
	{

		for (int i = 0; i < 3; i++) {

			if (i == blank_in_question_location)//if it is outline location
				continue;

			//set number of question (depending of sum position)
			int number = 1;
			if (randomResultPos == 0) {//sum = num1 + num2
				if (i == 0)
					number = numeral_result; // number is number we will display, set to result
				else if (i == 1)
					number = numeral1;
				else if (i == 2)
					number = numeral2;
			} else if (randomResultPos == 1) {	//num1 + num2 = sum
				if (i == 0)
					number = numeral1;
				else if (i == 1)
					number = numeral2;
				else if (i == 2)
					number = numeral_result;
			}
			//set item parameter

			if (display_0Fruit_1Number_2NumberWords_3NumberAndNumberWords_OnCard == 0)
				createFruitCard (i, number, i, true);
			else
				createNumberCard (i, number, i, true);


		}
	}



	// create new game that displays fruit card item in question --> (not sentence question)
	void startNewGame_GameType0 ()
	{

		setInstructionLabelText ();

		// move label little up
		QuestionLabel.transform.localPosition = new Vector3 (0, 290, 0);

		generateOperator_Numeral_1_2_3Result ();

		positionQuestionItem_GameType0 ();
		positionCorrectAnswerOutline_GameType0 ();
		StartCoroutine (ItemAnimation (blankItem_Placement, blankItem_Placement.transform.localPosition, -1));
		createQuestionItems_GameType0 ();

		int location = Random.Range (0, 4);

		int answerPosChild = Random.Range (0, AnswerPositions_FruitCard.transform.childCount);


		if (display_0Fruit_1Number_2NumberWords_3NumberAndNumberWords_OnCard == 0)
			createFruitCard (location, correct_answer, answerPosChild);
		else
			createNumberCard (location, correct_answer, answerPosChild);
       

        //create correct answer
		// Debug.Log ("correct_answer " + correct_answer);

		HashSet<int> noDuplicateAnswers = new HashSet<int> ();
		noDuplicateAnswers.Add (correct_answer);

		if (QuestionOperator == 2) {
			minNumber = 1;
			maxNumber = 101;
		}

		int counterForInfi1nityLoop = 0;

		for (int i = 0; i < 4; i++) {
			if (i == location)//if this is correct answer's location
				continue;

			counterForInfi1nityLoop++;

			int num = Random.Range (1, 10);		

			if (noDuplicateAnswers.Add (num)) {

				if (display_0Fruit_1Number_2NumberWords_3NumberAndNumberWords_OnCard == 0)
					createFruitCard (i, num, answerPosChild);
				else
					createNumberCard (i, num, answerPosChild);

			} else
				i--;

			if (counterForInfinityLoop > 100) {
				Debug.LogError ("INFINITY LOOP HERE");
				break;
			}

		}


	}


	void startNewGame_GameType1 ()
	{
		
		setInstructionLabelText ();


		generateOperator_Numeral_1_2_3Result ();
		correct_answer = numeral_result;

		//////////////////////////////////////////////////////
		///////////// POSITION QUESTION ITEMS ////////////////
		//////////////////////////////////////////////////////


		// we will instantiate object and set activate to see +-*/ sign!
		GameObject obj = NGUITools.AddChild (gameObject, QuestionPos_NumberGame);
		obj.SetActive (true);

		for (int i = 0; i < 2; i++) {
			obj.transform.GetChild (i).gameObject.SetActive (true);
		}


		GameObject operatorSign = NGUITools.AddChild (obj.transform.GetChild (3).gameObject, operatorsParent.transform.GetChild (QuestionOperator).gameObject);
		operatorSign.SetActive (true);

		operatorSign.transform.localPosition = obj.transform.GetChild(2).localPosition;
		operatorSign.transform.localPosition += Vector3.down * 50f;

		//////////////////////////////////////////////////
		///////// CREATE QUESTION ITEM ///////////////////
		//////////////////////////////////////////////////


		for (int i = 0; i < 2; i++) {

			//set number of question (depending of sum position)
			int number = 1;

			if (i == 0)
				number = numeral1;
			else if (i == 1)
				number = numeral2;
						
			// display label next to objects
			// on random chance label will display no label, number or number words -- MD fix

			obj.transform.GetChild (i).GetComponent<UILabel> ().text = number.ToString ();

		}

		// create location for correct answer 
		int location = Random.Range (0, 4);

		// create answer positions
		int answerPosChild = Random.Range (0, AnswerPositions_NumberGame.transform.childCount);

		createNumberCard (location, correct_answer, answerPosChild);

		//////////////////////////////////////////////
		///////// CREATE CORRECT ANSWER //////////////
		//////////////////////////////////////////////

		// Debug.Log ("correct_answer " + correct_answer);

		HashSet<int> noDuplicateAnswers = new HashSet<int> ();
		noDuplicateAnswers.Add (correct_answer);

		int counterForInfinityLoop = 0;

		if (QuestionOperator == 2) {
			minNumber = 1;
			maxNumber = 101;
		}

		for (int i = 0; i < 4; i++) {

			if (i == location)//if this is correct answer's location
				continue;

			counterForInfinityLoop++;

			int num = Random.Range (minNumber, maxNumber);

			if (noDuplicateAnswers.Add (num)) {
				createNumberCard (i, num, answerPosChild);

			} else
				i--;

			if (counterForInfinityLoop > 100) {
				Debug.LogError ("INFINITY LOOP HERE");
				break;
			}

		}


	}

	private void startNewGame_GameType2(){

		QuestionLabel.GetComponent<UILabel> ().text = "Calculate";

		// move label little up
		QuestionLabel.transform.localPosition = new Vector3 (0, 290, 0);



		int numbersForExpression = Random.Range (0, 2) == 0 ? 3 : 4;

		string expression = "";

		for (int i = 0; i < numbersForExpression; i++) {

			int number = Random.Range (minNumber, maxNumber);

			expression += number;

			CONTUNUEWORK:
			int operatorionSign = Random.Range (0, 4);

			if (i + 1 == numbersForExpression)
				break;

			if (operatorionSign == 0)
				expression += " + ";
			else if (operatorionSign == 1)
				expression += " - ";
			else if (operatorionSign == 2)
				expression += " * ";
			else if (operatorionSign == 3)
				expression += " : ";

			if (operatorionSign == 3) {

				int divider = -1;
				bool dividerFound = false;
				for (int j = 2; j < number; j++) {

					divider = j;

					if (number % divider == 0) {
						dividerFound = true;
						break;

					}

				}

				if (dividerFound == false)
					divider = 1;

				expression += divider;
				i++;
				goto CONTUNUEWORK;

			}

		}

		mathematicalExpressionLabel.GetComponent<UILabel> ().text = expression;
		mathematicalExpressionLabel.SetActive (true);

		correct_answer = Evaluate (expression);
		// Debug.Log (correct_answer);

		// always use only numbers
		display_0Fruit_1Number_2NumberWords_3NumberAndNumberWords_OnCard = 1;

		// generateOperator_Numeral_1_2_3Result ();

		int location = Random.Range (0, 4);

		int answerPosChild = Random.Range (0, AnswerPositions_FruitCard.transform.childCount);

		createNumberCard (location, correct_answer, answerPosChild);
			
		//////////////////////////////////////////////
		///////// CREATE CORRECT ANSWER //////////////
		//////////////////////////////////////////////

		// Debug.Log ("correct_answer " + correct_answer);

		HashSet<int> noDuplicateAnswers = new HashSet<int> ();
		noDuplicateAnswers.Add (correct_answer);

		int counterForInfi1nityLoop = 0;

		for (int i = 0; i < 4; i++) {

			if (i == location)//if this is correct answer's location
				continue;

			counterForInfi1nityLoop++;

			int num = Random.Range (correct_answer - 5, correct_answer + 5);		

			if (noDuplicateAnswers.Add (num)) {
				createNumberCard (i, num, answerPosChild);

			} else
				i--;

			if (counterForInfinityLoop > 100) {
				Debug.LogError ("INFINITY LOOP HERE");
				break;
			}

		}

	}

	private void startNewGame_GameType3(){

		int currentQuestion = Random.Range (0, dateGame.Length);

		QuestionLabel.GetComponent<UILabel> ().text = dateGame[currentQuestion];
		QuestionLabel.GetComponent<UILabel> ().fontSize = 50;

		// move label little up
		QuestionLabel.transform.localPosition = new Vector3 (0, 200, 0);


		correct_answer = dateAnswer [currentQuestion];
		// Debug.Log (correct_answer);

		// always use only numbers
		display_0Fruit_1Number_2NumberWords_3NumberAndNumberWords_OnCard = 1;

		// generateOperator_Numeral_1_2_3Result ();

		int location = Random.Range (0, 4);

		int answerPosChild = Random.Range (0, AnswerPositions_FruitCard.transform.childCount);

		createNumberCard (location, correct_answer, answerPosChild);

		//////////////////////////////////////////////
		///////// CREATE CORRECT ANSWER //////////////
		//////////////////////////////////////////////

		// Debug.Log ("correct_answer " + correct_answer);

		HashSet<int> noDuplicateAnswers = new HashSet<int> ();
		noDuplicateAnswers.Add (correct_answer);

		int counterForInfi1nityLoop = 0;

		for (int i = 0; i < 4; i++) {

			if (i == location)//if this is correct answer's location
				continue;

			counterForInfi1nityLoop++;

			int num = Random.Range (correct_answer - 3, correct_answer + 2);		

			if (noDuplicateAnswers.Add (num)) {
				createNumberCard (i, num, answerPosChild);

			} else
				i--;

			if (counterForInfinityLoop > 100) {
				Debug.LogError ("INFINITY LOOP HERE");
				break;
			}

		}

	}

	private int Evaluate(string expr){

		expr = expr.Replace (" ", "");
		expr = expr.Replace (":", "/");

		try{
			Expression exp = new Expression (expr);
			int i = int.Parse (exp.Evaluate ().ToString ());
			return i;
		}catch(System.FormatException e){
			return 0;
		}catch(EvaluationException ex){
			return 0;
		}catch(System.ArgumentException ex){
			return 0;
		}
	}

	private void createFruitCard (int location, int number, int answerPosChild, bool isQuestionItem = false)
	{

		GameObject useForQuestionPosition = QuestionPos_FruitCard;
		GameObject fruitCard = FruitCard_Parent.transform.GetChild (0).gameObject;
		GameObject new_obj = NGUITools.AddChild (gameObject, fruitCard);
		new_obj.SetActive (true);

		if (isQuestionItem)
			new_obj.transform.localPosition = useForQuestionPosition.transform.GetChild (location).localPosition;
		else
			new_obj.transform.localPosition = AnswerPositions_FruitCard.transform.GetChild (answerPosChild).GetChild (location).localPosition;//
		

		useFruitCard (new_obj, number);
		new_obj.GetComponent<Operations_FruitCards_ItemScript> ().number = number;//
		new_obj.GetComponent<Operations_FruitCards_ItemScript> ().isQuestionItem = isQuestionItem;

		StartCoroutine (ItemAnimation (new_obj, new_obj.transform.localPosition, -1));


	}

	private void useFruitCard (GameObject new_obj, int number)
	{

		spriteName = fruit_sprite_name [fruitSprite];

		fruitCard.useFruitCard (new_obj, number, spriteName);
		

	}

	private int counterForInfinityLoop = 0;



	/// <summary>
	/// Creates the number card.
	/// Answer position child. (never going to be used if isQuestionItem = true!
	/// </summary>
	private void createNumberCard (int location, int number, int answerPosChild, bool isQuestionItem = false)
	{

		// Debug.Log ("Location " + location + " NUMBER " + number);

		GameObject obj = null;

		if (GameType == 1)
			obj = NGUITools.AddChild (gameObject, NumberGame_AnswerItem);
		else
			obj = NGUITools.AddChild (gameObject, FruitCard_Parent.transform.GetChild (0).gameObject);



		// question items position for grade 0
		if (isQuestionItem)
			obj.transform.localPosition = QuestionPos_FruitCard.transform.GetChild (location).localPosition;//
		else
			obj.transform.localPosition = AnswerPositions_FruitCard.transform.GetChild (answerPosChild).GetChild (location).localPosition;//


		obj.transform.GetChild (4).gameObject.SetActive (true);
        
		obj.GetComponentInChildren<UISprite> ().color = Colors.generateRandomRoundRectColor ();

		// set number word
		if (display_0Fruit_1Number_2NumberWords_3NumberAndNumberWords_OnCard == 2) {
			obj.transform.GetChild (4).GetComponent<UILabel> ().text = "" + Utilities.makeNumberWord (number);//
			obj.transform.GetChild (4).GetComponent<UILabel> ().fontSize = 60;

			if (number < 100)
				obj.transform.GetChild (4).GetComponent<UILabel> ().fontSize = 80;

			// set number
		} else if (display_0Fruit_1Number_2NumberWords_3NumberAndNumberWords_OnCard == 1) {
			obj.transform.GetChild (4).GetComponent<UILabel> ().text = "" + number;//

            

		} else if (display_0Fruit_1Number_2NumberWords_3NumberAndNumberWords_OnCard == 3 && GameType == 0) {
			obj.transform.GetChild (4).gameObject.SetActive (false);
			obj.transform.GetChild (5).gameObject.SetActive (true);

			obj.transform.GetChild (5).GetChild (0).GetComponent<UILabel> ().text = "" + number;//
			obj.transform.GetChild (5).GetChild (1).GetComponent<UILabel> ().text = Utilities.makeNumberWord (number);

			// if numberword is in one line
			if (obj.transform.GetChild (5).GetChild (1).GetComponent<UILabel> ().text.Contains ("\n") == false) {
				obj.transform.GetChild (5).GetChild (0).localPosition += Vector3.down * 10f;
			}


		} else if (GameType == 3) {
			obj.transform.GetChild (4).GetComponent<UILabel> ().text = "" + number;//

           
		}

		obj.transform.GetChild (4).GetComponent<UILabel> ().color = Color.white;

		// backside!
		obj.transform.GetChild (3).GetComponent<UILabel> ().text = "" + number;

		obj.GetComponent<Operations_FruitCards_ItemScript> ().number = number;
		obj.GetComponent<Operations_FruitCards_ItemScript> ().isQuestionItem = isQuestionItem;
		obj.SetActive (true);


		StartCoroutine (ItemAnimation (obj, obj.transform.localPosition));

	}



	IEnumerator ItemAnimation (GameObject obj, Vector3 target, int direction = 1)
	{
		// Debug.Log ("////////////ItemAnimation");

		// Debug.Log ((obj.GetComponent<TweenPosition> () == null) + " OBJ NAME: " + obj.name);

		yield return new WaitForSeconds (0.001f);
		// create incorrect answer Tween
		int x = Random.Range (0, 1200);
		int y = (1200 - x) * (-1) * direction;
		int plusOrMinusX = Random.Range (0, 2);

		if (plusOrMinusX == 0) {
			x = x * -1;
		}

		// Debug.Log (obj.name);

		obj.GetComponent<TweenPosition> ().from = new Vector3 (x, y, 0);
		obj.GetComponent<TweenPosition> ().to = target;
		obj.GetComponent<TweenPosition> ().duration = 0.5f;
		obj.GetComponent<TweenPosition> ().ResetToBeginning ();
		obj.GetComponent<TweenPosition> ().enabled = true;

		obj.GetComponent<TweenRotation> ().from = new Vector3 (0, 0, -720);
		obj.GetComponent<TweenRotation> ().to = new Vector3 (0, 0, 0);
		obj.GetComponent<TweenRotation> ().duration = 0.5f;
		obj.GetComponent<TweenRotation> ().ResetToBeginning ();
		obj.GetComponent<TweenRotation> ().enabled = true;
		yield return new WaitForSeconds (0.5f);
	}



	//if user touches the number sprite, check
	public void checkAnswer (GameObject item)
	{

		// we actually don't need this but this is good way to block user for spamming if you forget 
		// to put on right place
		if (noSpamming)
			return;

		int number = item.GetComponent<Operations_FruitCards_ItemScript> ().number;

		// if user taps correct number
		if (number == correct_answer) {

			// set depth above question items
			item.GetComponentInChildren<UISprite> ().depth = 1000;
			Transform frontside = item.transform.GetChild (2);


			foreach (Transform child in frontside) {

				if (child.GetComponent<UISprite> () != null) {
					child.GetComponent<UISprite> ().depth = 1001;

					if (child.transform.childCount > 0) {

						foreach (Transform childInDepth in child)
							childInDepth.GetComponent<UILabel> ().depth = 1002;

					}

					// that means we are working with fruitCard_10
				} else {
					foreach (Transform childInDepth in child)
						childInDepth.GetComponent<UISprite> ().depth = 1001;

				}

			}

			UIManager.instance.incrementNumberOfTotalCoins ();
			int currentPointsOfThisLevel = UIManager.instance.incrementNumberOfPointsThisLevel ();

			if (currentPointsOfThisLevel >= 10) {
				UIManager.instance.unlockNewLevel (UIManager.gameSelected + 1);
			}

			item.transform.GetChild (3).GetComponent<UILabel> ().depth = 1003;
			item.transform.GetChild (4).GetComponent<UILabel> ().depth = 1003;

			

			if (item.transform.childCount > 20)
				item.transform.localScale = new Vector3 (.75f, .75f, 1);

			if (GameType == 0)
				item.SendMessage ("startSuccessAction", blankItem_Placement.transform.localPosition);
			else
				item.SendMessage ("startSuccessAction", new Vector3 (0f, -100f, 0f));

			GameObject[] array = GameObject.FindGameObjectsWithTag ("numberitem"); //destroy all objects with numberitem tag
			foreach (GameObject obj in array) {

				if (obj.GetComponent<Operations_FruitCards_ItemScript> () != null && obj.GetComponent<Operations_FruitCards_ItemScript> ().number != correct_answer
				    && obj.GetComponent<Operations_FruitCards_ItemScript> ().isQuestionItem == false)
					Destroy (obj);
			}


			StartCoroutine (startNewGameDelay (2f));	



		}
		// if user touches incorrect number

		else {

			int currentPointsOfThisLevel =	PlayerPrefs.GetInt ("Level" + UIManager.gameSelected + " number of points", 0);
			int totalPoints =	PlayerPrefs.GetInt ("totalPoints", 0);

			// Debug.Log (item.transform.GetChild (item.transform.childCount - 1).name);

			// TODO increment number of points
			// TODO increment number of right answer

			item.transform.GetChild (1).GetComponent<UILabel> ().text = "X";
			item.transform.GetChild (1).GetComponent<UILabel> ().color = Color.red;


			StartCoroutine (stopAndRestartSpamming ());
			item.SendMessage ("startFailAction");//moving down action
		}
	}


	IEnumerator stopAndRestartSpamming ()
	{

		yield return new WaitForSeconds (0.01f);
		noSpamming = true;

		yield return new WaitForSeconds (1f);
		noSpamming = false;

	}



}
