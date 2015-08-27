using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/*
This script is for controlling caharacter position throughout
	the scene when a button is clicked.
*/
public class ScriptButtonExhibition:MonoBehaviour{

	/*
	Public reference to player in the screen.
	Since this game object is created via instantiate script,
		I need to take the reference to Player RectTransform
		internally.
	*/
	public RectTransform playerRectTransform;
	//Get player image component.
	private Image playerImage;

	//This game object Image.
	private Image image;
	//This game object RectTransform.
	private RectTransform rectTransform;

	/*
	Wether or not Player should be moved in to a new position or not.
	The thing is that the player need to adjust per - frame.
	Hence, when the resolution is changed the player will move accordingly.
	*/
	private bool movePlayer = false;

	/*
	Take a reference to this script.
	This is important to iterate all possible exhibition button in the screen.
	*/
	private ScriptButtonExhibition scriptButtonExhibition;

	//Get the ExhibitionDatabse game object within the scene and take its script component.
	private GameObject exhibitionDatabase;
	private ScriptExhibitionDatabase scriptExhibitionDatabase;
	//Get this exhibition explanation count.
	public int exhibitionExplanationAmount;
	/*
	Get this exhibitoin index.
	For example if this game object is ButtonExhibition19 
		the exhibition index will be 19.
	*/
	private int exhibitionIndexInt;
	private int exhibitionArrayLength;
	private int exhibitionArrayLengthPrevious;
	private string exhibitionIndexString;

	//Get the PanelLog game object within the scene and take its script component.
	private GameObject panelLog;
	private ScriptPanelLog scriptPanelLog;

	//Get reference to panel notification and its script component.
	private GameObject panelNotification;
	private ScriptPanelNotification scriptPanelNotification;

	/*
	An array of game object that contain all buttonExhibition game object
		in the screen.
	I will iterate all of these using FindGameObjectsWithTag().
	*/
	private GameObject[] buttonExhibitionArray;

	void Start(){

		/*
		Initial setting for this game object.
		The settings are like index setting and explanation settings.
		These meant so that this game object know which explanation belong to this game object.
		*/
		SetExhibition();

		//Get reference to this script for comparison purpose.
		scriptButtonExhibition = gameObject.GetComponent<ScriptButtonExhibition>();

		//Find a Player game object and then take its RectTransform component.
		playerRectTransform = GameObject.Find("Player").GetComponent<RectTransform>();
		//Get image player reference.
		playerImage = GameObject.Find("Player").GetComponent<Image>();

		//This game object Image.
		image = gameObject.GetComponent<Image>();
		//This game object RectTransform.
		rectTransform = gameObject.GetComponent<RectTransform>();

		//Get reference to panel log game object.
		panelLog = GameObject.Find("PanelLog");
		scriptPanelLog = panelLog.GetComponent<ScriptPanelLog>();

		//Assign value to panelNotification.
		panelNotification = GameObject.Find("PanelNotification");
		scriptPanelNotification = panelNotification.GetComponent<ScriptPanelNotification>();

		//Assign image based on gameObject.name.
		image.sprite = Resources.Load<Sprite>("AssetsImage/MuseumCards/" + gameObject.name);

	}

	void Update(){

		exhibitionArrayLength = scriptExhibitionDatabase.exhibitionArray.Length;
		if(exhibitionArrayLength != exhibitionArrayLengthPrevious){

			//If there is change in how many button exhibition in screen than re - set the whole buttons.
			SetExhibition();

			exhibitionArrayLengthPrevious = exhibitionArrayLength;

		}

		if(movePlayer){

			/*
			If a button is clicked then change player RectTransform position
				to this game object RectTransform position.
			*/
			playerRectTransform.position = rectTransform.position;
			GameObject.Find("Player").transform.SetParent(gameObject.transform);

		}

	}

	private void SetExhibition(){

		//Get reference to ExhibitionDatabase game object.
		exhibitionDatabase = GameObject.Find("ExhibitionDatabase");

		//Null reference checking.
		if(exhibitionDatabase != null){

			//Get reference to ScriptExhibitionDatabase inside ExhibitionDatabase game object.
			scriptExhibitionDatabase =
				exhibitionDatabase.GetComponent<ScriptExhibitionDatabase>();

			exhibitionArrayLength =
				scriptExhibitionDatabase.exhibitionArray.Length;

			/*
			Get this game object exhibition string.
			What I meant by exhibition string is a string that has substracted from
				this game object name so that it only take the number of this ButtonExhibition
				game object.
			In my case, in this program only buttons those are not ButtonExhibitionPrime has
				exhibition index because, ButtonExhibitionPrime is always visited no matter what.
			*/
			if(gameObject.name != "ButtonExhibitionPrime"){
				exhibitionIndexString = gameObject.name.Replace("ButtonExhibition", "");
				exhibitionIndexInt = int.Parse(exhibitionIndexString);

				//If there is explanation given for this button exhibition.
				if(exhibitionIndexInt <= exhibitionArrayLength - 1){
					exhibitionExplanationAmount =
						scriptExhibitionDatabase.exhibitionArray[exhibitionIndexInt].explanationArray.Length;
				}
				//If there is no explanation given for this button exhibition in particular.
				else if(exhibitionIndexInt > exhibitionArrayLength - 1){

					/*
					Just give the value to -1, I think I can also make the value
						as null.
					But whatever~.
					*/
					exhibitionExplanationAmount = -1;

				}

			}

			//Specifically for ButtonExhibitionPrime I set the index to 0.
			else if(gameObject.name == "ButtonExhibitionPrime"){

				exhibitionIndexInt = 0;
				//If there is explanation given for this button exhibition.
				if(exhibitionIndexInt <= exhibitionArrayLength - 1){
					exhibitionExplanationAmount =
						scriptExhibitionDatabase.exhibitionArray[exhibitionIndexInt].explanationArray.Length;
				}
				//If there is no explanation given for this button exhibition in particular.
				else if(exhibitionIndexInt > exhibitionArrayLength - 1){

					/*
					Just give the value to -1, I think I can also make the value
						as null.
					But whatever~.
					*/
					exhibitionExplanationAmount = -1;

				}

			}

		}
		else{

			exhibitionExplanationAmount = -1;

			//Print debug error message.
			Debug.Log("ERROR: GameObject ExhibitionDatabase could not be found!");

		}	

	}

	//A button function that is executed from this game object.
	public void ButtonExhibition(){

		//Activate/un - hide the player image component.
		playerImage.enabled = true;

		buttonExhibitionArray = GameObject.FindGameObjectsWithTag("ButtonExhibition");
		if(scriptPanelLog.visitedExhibitionList.Count == 0 && gameObject.name != "ButtonExhibitionPrime"){
			
			//Set notification to visit primary exhibition first.
			SetNotification(
				"Welcome to "
				+ gameObject.transform.Find("Text").GetComponent<Text>().text
				+ ", please visit Exhibition Prime"
			);

		}

		/*
		Here is where the magic happened.
		I iterate all the exhibition button in the scene and then
			change variable of movePlayer to false but this game object.
		Hence you can only have one exhibition button to have movePlayer = true
			at a time.
		*/
		for(int i = 0; i < buttonExhibitionArray.Length; i ++){

			if(buttonExhibitionArray[i].GetComponent<ScriptButtonExhibition>() != scriptButtonExhibition){

				buttonExhibitionArray[i].GetComponent<ScriptButtonExhibition>().SetMovePlayer(false);

			}
			else{

				/*
				This is the desired destination of the player in this case.
				You need to set this to true and then add the corresponding object
					to the list.
				*/
				buttonExhibitionArray[i].GetComponent<ScriptButtonExhibition>().SetMovePlayer(true);

				if(scriptPanelLog.visitedExhibitionList.Count == 0 && buttonExhibitionArray[i].name == "ButtonExhibitionPrime"){

					scriptPanelLog.AddToVisitedExhibitionList(buttonExhibitionArray[i].name);
					
				}
				else if(scriptPanelLog.visitedExhibitionList.Count > 0){
					//Uncommment this if you want visitor to visits unique exhibition per turn.
					/*
					Prevent the same exhibition to be added twice to the list.
					Need to iterate every other member within the list.
					*/
					/*
					bool sameExhibition = false;
					for(int j = 0; j < scriptPanelLog.visitedExhibitionList.Count; j ++){
						if(buttonExhibitionArray[i].name == scriptPanelLog.visitedExhibitionList[j]){
							sameExhibition = true;
						}
					}
					if(!sameExhibition){
						scriptPanelLog.AddToVisitedExhibitionList(buttonExhibitionArray[i].name);
					}
					*/
					scriptPanelLog.AddToVisitedExhibitionList(buttonExhibitionArray[i].name);
				}

			}

		}

	}

	//Setter for movePlayer variable.
	public void SetMovePlayer(bool _value){

		movePlayer = _value;

	}

	//Set a string to be displayed in notification panel.
	private void SetNotification(string _notification){

		//I feel retarded writing a code below.
		scriptPanelNotification.textText.text = _notification;

	}

}