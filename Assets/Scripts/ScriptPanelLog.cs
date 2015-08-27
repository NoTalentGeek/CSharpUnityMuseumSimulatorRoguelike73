using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/*
This script is for creating and controlling log panel.
This log panel will be handled so that it will be like notification.
This log panel will show record of everything that this player has done.
However, after the reset button pressed the log panel is cleared.
*/
public class ScriptPanelLog:MonoBehaviour{

	/*
	Example of Magic the Gathering Card Proportion.
	With this proportion you input the height of the card.
	*/
	private float panelProportion = (334.0f/239.0f);

	//Create a copy of each exhibition that is in visitedExhibitionList.
	public class ExhibitionCopy{

		/*
		Check whether this copy of exhibition is properly visited.
		What I meant by properly visited is this copy of exhibition has been tapped and rolled.
		Hence, this copy of exhibition has received its explanation and visitor gets next target.
		*/
		public bool properlyVisited = false;

		/*
		rolledNumber is a random number generated from ScriptRoll20D.cs
		PENDING: I am not sure yet whether this variable is still necessary or not.
		*/
		public int rolledNumber = -1;

		/*
		A variable to holds exhibition name, since this type data is basically a copy of a 
			buttonExhibition so it must has an unique identifier.
		In this case, I use its exhibition name as its unique indentifier.
		*/
		public string exhibitionName = "";

		/*
		Random explanation this copy of exhibition gets from tapping dice.
		The explanation given is taken from exhibition database.
		*/
		public string randomExplanation = "";

		/*
		PENDING: I should have make the length of this array to be dynamic
			(it is not necessary to be a List).
		PENDING: What I should have done here is creating a reference to exhibitionDatabase to take
			the amount of explanation per exhibition there.
		*/
		public int[] targetExhibitionIndex = new int[2];

		//Constructor.
		public ExhibitionCopy(){

			/*
			I assigned all index from targetExhibitIndex to -1 for make me easier to do if statement later on.
			I hope I can assign null to int but unfortunately in C# I cannot do that.
			*/
			for(int i = 0; i < targetExhibitionIndex.Length; i ++){

				targetExhibitionIndex[i] = -1;


			}
		}

	}

	/*
	A private list of all exhibition that has been visited.
	I make a copy of it into my newly created class.
	*/
	private ExhibitionCopy exhibitionCopyTemporary; //Temporary variable to hold the copy of exhibitiob button.
	public List<ExhibitionCopy> exhibitionCopyList;
	//List of visited exhibition name.
	public List<string> visitedExhibitionList;
	private int visitedExhibitionListCount;
	private int visitedExhibitionListCountPrevious;

	/*
	This game object's child and this game object component.
	It is important to get control of each child and its vital component within this game object
		so that I can control its visibility and activity with code.
	Get this game object Image component.
	*/
	private Image image;
	//This boolean variable is for switching the visibility of this panel.
	private int imageSwitch = -1;
	//Get this game object RectTransform component.
	private RectTransform rectTransform;
	//Get this scroll panel reference from the scene.
	private Transform childPanelScrollTextLog;
	private Image childPanelScrollTextLogImage;
	//Get game object of this childTextClickToDismiss.
	private Transform childTextClickToDismiss;
	//Get the childTextClickToDismiss's text component.
	private Text childTextClickToDismissText;
	//Get childTextLog game object.
	private Transform childTextLog;
	private Text childTextLogText;

	//Add reference variable to exhibition database game object.
	private GameObject exhibitionDatabase;
	private ScriptExhibitionDatabase scriptExhibitionDatabase;

	//Reference to panel content show cards.
	private GameObject panelContentShowCards;
	private ScriptPanelContentShowCards scriptPanelContentShowCards;
	private string explanation;
	private string explanationPrevious;

	//Get reference to panel notification and its script component.
	private GameObject panelNotification;
	private ScriptPanelNotification scriptPanelNotification;

	//Use this for initialization.
	void Start(){

		/*
		These code below mostly to get reference to this game object component or
			this game object's child component.
		Get this game object Image and RectTransform component.
		*/
		image = gameObject.GetComponent<Image>();
		rectTransform = gameObject.GetComponent<RectTransform>();
		//Fill in childPanelScrollTextLog and take its Image component.
		childPanelScrollTextLog = gameObject.transform.Find("PanelScrollTextLog");
		childPanelScrollTextLogImage = childPanelScrollTextLog.gameObject.GetComponent<Image>();
		//Get the childTextClickToDismiss game object.
		childTextClickToDismiss = gameObject.transform.Find("TextClickToDismiss");
		//Get childTextClickToDismiss's text component.
		childTextClickToDismissText = childTextClickToDismiss.gameObject.GetComponent<Text>();
		//Get the childTextLog game object and its text component.
		childTextLog = gameObject.transform.Find("PanelScrollTextLog").Find("TextLog");
		childTextLogText = childTextLog.gameObject.GetComponent<Text>();
		//Put reference to exhibitionDatabase.
		exhibitionDatabase = GameObject.Find("ExhibitionDatabase");
		scriptExhibitionDatabase = exhibitionDatabase.GetComponent<ScriptExhibitionDatabase>();
		//Panel content show cards.
		panelContentShowCards = GameObject.Find("PanelContentShowCards");
		scriptPanelContentShowCards = panelContentShowCards.GetComponent<ScriptPanelContentShowCards>();
		//Assign value to panelNotification.
		panelNotification = GameObject.Find("PanelNotification");
		scriptPanelNotification = panelNotification.GetComponent<ScriptPanelNotification>();

	}

	void Update(){

		childTextLogText.fontSize = Screen.height*14/380;

		//Adjust this panel size so that it is as big as a standard Magic The Gathering cards.
		rectTransform.sizeDelta = new Vector2((Screen.width/3), ((Screen.width/3)*panelProportion));

		/*
		Control this game object visibility based on imageSwicth variable.
		These code below is intended to control visibility of this game object and its
			child.
		So that Log button can be used to toggle this game object and its child visibility and
			whether or not these game object is active or not.
		*/
		if(imageSwitch == 1){

			childPanelScrollTextLogImage.enabled = true;
			childTextClickToDismissText.enabled = true;
			childTextLogText.enabled = true;
			image.enabled = true;

		}
		else if(imageSwitch == -1){

			childPanelScrollTextLogImage.enabled = false;
			childTextClickToDismissText.enabled = false;
			childTextLogText.enabled = false;
			image.enabled = false;

		}

		//PENDING: I think I can remove visitedExhibitionListCount and just use visitedExhibitiobList.Count for later on.
		visitedExhibitionListCount = visitedExhibitionList.Count;

		/*
		This codes below is for controlling how many exhibition have been visited by the player.
		The exhibition list will be reset when the reset button pressed.
		*/
		if(visitedExhibitionListCount <= 0){

			/*
			This is the default text if there is nothing in the list or if the reset button ppressed.
			Hence, you need to set the list to  empty when you want this default text to be displayed.
			*/
			childTextLogText.text = "You have not visited any exhibition yet!\nPlease visit the primary exhibition.";

			/*
			If the list count is 0, then there is no exhibition has been visited by the visitor,
				hence, here I initiated the List.
			*/
			exhibitionCopyList = new List<ExhibitionCopy>();

		}

		/*
		If there is a thing at least one, within the list this program will showed how many and what
			kind of exhibition has visited.
		*/
		else if(visitedExhibitionListCount > 0){

			//Check so that only one exhibition could enter the list everytime there is a change in visitedExhibitionListCount.
			if(visitedExhibitionListCount != visitedExhibitionListCountPrevious){

				//Create a temporary holder for a copy of newly entered exhibition.
				exhibitionCopyTemporary = new ExhibitionCopy();
				//Assign the name of newly entered/visited exhibition.
				exhibitionCopyTemporary.exhibitionName = visitedExhibitionList[visitedExhibitionList.Count - 1];
				//Put this temporary exhibition into the list, so that next time we can use it again.
				exhibitionCopyList.Add(exhibitionCopyTemporary);

				if(visitedExhibitionListCount == 1){

					//Add notification when visitor visited new exhibition.
					SetNotification(
						"Welcome to " 
						+ GameObject.Find(exhibitionCopyTemporary.exhibitionName).gameObject.transform.Find("Text").gameObject.GetComponent<Text>().text
						+ ", please tap your dice."
					);

				}
				else{

					int indexLastGameObject = -1;
					//Backward loop through all visitedExhibitionList to find out the latest properly visited exhibition.
					for(int i = visitedExhibitionList.Count - 1; i >= 0; i --){
						if(
							exhibitionCopyList[i].properlyVisited == true
							&& indexLastGameObject == -1
						){
							indexLastGameObject = i;
						}
					}

					if(
						indexLastGameObject != -1 &&
						(exhibitionCopyTemporary.exhibitionName == "ButtonExhibition" + exhibitionCopyList[indexLastGameObject].targetExhibitionIndex[0]
						|| exhibitionCopyTemporary.exhibitionName == "ButtonExhibition" + exhibitionCopyList[indexLastGameObject].targetExhibitionIndex[1])
					){
						
						//Add notification when visitor visited new exhibition.
						SetNotification(
							"Welcome to " 
							+ GameObject.Find(exhibitionCopyTemporary.exhibitionName).gameObject.transform.Find("Text").gameObject.GetComponent<Text>().text
							+ ", please tap your dice."
						);

					}
					else if(indexLastGameObject == -1){

						string currentExhibition = 
							GameObject.Find(exhibitionCopyTemporary.exhibitionName).gameObject.transform.Find("Text").gameObject.GetComponent<Text>().text;

						if(currentExhibition != "Exhibition Prime"){

							SetNotification(
								"Welcome to " 
								+ currentExhibition
								+ ", please visit Exhibition Prime."
							);

						}
						else if(currentExhibition == "Exhibition Prime"){

							SetNotification(
								"Welcome to " 
								+ currentExhibition
								+ ", please tap your dice."
							);
							
						}

					}
					else{

						//Add notification when visitor visited new exhibition.
						SetNotification(
							"Welcome to " 
							+ GameObject.Find(exhibitionCopyTemporary.exhibitionName).gameObject.transform.Find("Text").gameObject.GetComponent<Text>().text
							+ ", please visit Exhibition "
							+ exhibitionCopyList[indexLastGameObject].targetExhibitionIndex[0]
							+ " or "
							+ exhibitionCopyList[indexLastGameObject].targetExhibitionIndex[1]
							+ "."
						);

					}

				}

				//Make sure this thing can only happened once.
				visitedExhibitionListCountPrevious = visitedExhibitionListCount;

			}

			//Check whether or not the latest exhibition visited is already tapped and received its random explanation.
			if(exhibitionCopyList[exhibitionCopyList.Count - 1].randomExplanation != ""){

				SetNotification(
					exhibitionCopyList[exhibitionCopyList.Count - 1].randomExplanation
					+ ", please shake your dice."
				);
				explanation = exhibitionCopyList[exhibitionCopyList.Count - 1].randomExplanation;
				if(explanation != explanationPrevious){

					scriptPanelContentShowCards.AddToExplanationHashSet(
						exhibitionCopyList[exhibitionCopyList.Count - 1].randomExplanation,
						exhibitionCopyList[exhibitionCopyList.Count - 1].exhibitionName
					);
					explanationPrevious = explanation;

				}

				/*
				If the latest exhibition has received its random explanation, then check whether or not dice has rolled within
					this exhibition.
				*/
				if(exhibitionCopyList[exhibitionCopyList.Count - 1].rolledNumber != -1){

					/*
					If this exhibition is not button exhibition prime, which means the latest visited exhibition is a normal
						exhibition.
					*/
					if(exhibitionCopyList[exhibitionCopyList.Count - 1].exhibitionName == "ButtonExhibitionPrime"){

						//PENDING: I should have make this dynamic, so that it can handle dynamic amount of explanation.
						if(exhibitionCopyList[exhibitionCopyList.Count - 1].rolledNumber <= scriptExhibitionDatabase.exhibitionArray[0].threshold){
							

							exhibitionCopyList[exhibitionCopyList.Count - 1].targetExhibitionIndex[0] =
								scriptExhibitionDatabase.exhibitionArray[0].targetExhibitionArray[0];
							exhibitionCopyList[exhibitionCopyList.Count - 1].targetExhibitionIndex[1] =
								scriptExhibitionDatabase.exhibitionArray[0].targetExhibitionArray[1];


						}
						else if(exhibitionCopyList[exhibitionCopyList.Count - 1].rolledNumber > scriptExhibitionDatabase.exhibitionArray[0].threshold){

							exhibitionCopyList[exhibitionCopyList.Count - 1].targetExhibitionIndex[0] =
								scriptExhibitionDatabase.exhibitionArray[0].targetExhibitionArray[2];
							exhibitionCopyList[exhibitionCopyList.Count - 1].targetExhibitionIndex[1] =
								scriptExhibitionDatabase.exhibitionArray[0].targetExhibitionArray[3];

						}

					}

					/*
					These codes below are intended when the latest exhibitiob user visited is a exhibitiob prime.
					Basically the same code unless the index is 0 no matter what happened.
					*/
					else{

						//PENDING: Same like above, I need to make this dynamic, perhaps using loop and change this into a List instead of using array.
						if(exhibitionCopyList[exhibitionCopyList.Count - 1].rolledNumber <= scriptExhibitionDatabase.exhibitionArray[0].threshold){

							exhibitionCopyList[exhibitionCopyList.Count - 1].targetExhibitionIndex[0] =
								scriptExhibitionDatabase.exhibitionArray[
									int.Parse(exhibitionCopyList[exhibitionCopyList.Count - 1].exhibitionName.Replace("ButtonExhibition", ""))
								].targetExhibitionArray[0];
							exhibitionCopyList[exhibitionCopyList.Count - 1].targetExhibitionIndex[1] =
								scriptExhibitionDatabase.exhibitionArray[
									int.Parse(exhibitionCopyList[exhibitionCopyList.Count - 1].exhibitionName.Replace("ButtonExhibition", ""))
								].targetExhibitionArray[1];


						}

						else if(exhibitionCopyList[exhibitionCopyList.Count - 1].rolledNumber > scriptExhibitionDatabase.exhibitionArray[0].threshold){

							exhibitionCopyList[exhibitionCopyList.Count - 1].targetExhibitionIndex[0] =
								scriptExhibitionDatabase.exhibitionArray[
									int.Parse(exhibitionCopyList[exhibitionCopyList.Count - 1].exhibitionName.Replace("ButtonExhibition", ""))
								].targetExhibitionArray[2];
							exhibitionCopyList[exhibitionCopyList.Count - 1].targetExhibitionIndex[1] =
								scriptExhibitionDatabase.exhibitionArray[
									int.Parse(exhibitionCopyList[exhibitionCopyList.Count - 1].exhibitionName.Replace("ButtonExhibition", ""))
								].targetExhibitionArray[3];

						}

					}

					string[] targetExhibitionTemp = new string[2];
					//Fixing the primary exhibition.
					for(int i = 0; i < 2; i ++){
						if(exhibitionCopyList[exhibitionCopyList.Count - 1].targetExhibitionIndex[i] == 0){
							targetExhibitionTemp[i] = "Prime";
						}
						else{
							targetExhibitionTemp[i] = "" + exhibitionCopyList[exhibitionCopyList.Count - 1].targetExhibitionIndex[i];
						}
					}

					SetNotification(

						"You get "
						+ exhibitionCopyList[exhibitionCopyList.Count - 1].rolledNumber
						+ ", please visit exhibition "
						+ targetExhibitionTemp[0]
						+ " or "
						+ targetExhibitionTemp[1]
						+ "."
					);

				}

			}

			/*
			Temporary string to display in panel log.
			This string will be iterated through all visited exhibition when there is a change within visitedExhibitionListCount.
			*/
			string text = "";

			//Loop through copy of exhibition.
			for(int i = 0; i < exhibitionCopyList.Count; i ++){

				/*
				Check whether or not a copy of exhibition is a properly visited or not.
				Basically if a copy of exhibition has received explanation, rolled number, and destined targets
					you can say that that copy of exhibition is properly visited.
				*/
				if(exhibitionCopyList[i].exhibitionName != ""){

					if(exhibitionCopyList[i].randomExplanation != ""){

						if(exhibitionCopyList[i].rolledNumber != -1){

							if(
								exhibitionCopyList[i].targetExhibitionIndex[0] != -1 &&
								exhibitionCopyList[i].targetExhibitionIndex[1] != -1
							){

								exhibitionCopyList[i].properlyVisited = true;

							}
						}

					}

				}

				/*
				Temporary string that will held either blank string or not.
				Basically if a copy of an exhibition has all these string filled then
					this particular copy of an exhibition is properly visited.
				*/
				string _exhibitionName = "";
				string _randomExplanation = "";
				string _targetExhibitionIndex = "";
				string _targetExhibitionIndex0 = "";
				string _targetExhibitionIndex1 = "";

				if(exhibitionCopyList[i].exhibitionName != ""){

					/*
					If this exhibition is not the first exhibition visited then put line break in
						the front of the string.
					*/
					if(i > 0){

						_exhibitionName = "\n" + exhibitionCopyList[i].exhibitionName;

					}

					/*
					If this is the first exhibition visited, means this exhibition is exhibition prime,
						then it is not necessary to put line break in front of the string.
					*/
					else{

						_exhibitionName = exhibitionCopyList[i].exhibitionName;

					}

					//If this exhibition is already get its explanation then it is also eligible for getting target exhibition.
					if(exhibitionCopyList[i].randomExplanation != ""){

						_randomExplanation = "\n    " + exhibitionCopyList[i].randomExplanation;

						//If there is number rolled then assign target exhibition.
						if(exhibitionCopyList[i].rolledNumber != -1){

							//PENDING: Change this into dynamic if statement.
							if(
								exhibitionCopyList[i].targetExhibitionIndex[0] != -1 &&
								exhibitionCopyList[i].targetExhibitionIndex[1] != -1
							){

								_targetExhibitionIndex0 = exhibitionCopyList[i].targetExhibitionIndex[0].ToString();
								_targetExhibitionIndex1 = exhibitionCopyList[i].targetExhibitionIndex[1].ToString();
								_targetExhibitionIndex = "\n    Please visit exhibition " + _targetExhibitionIndex0 + " or " + _targetExhibitionIndex1;

							}

						}

					}

				}

				//Accumulate all text.
				text = text + _exhibitionName + _randomExplanation + _targetExhibitionIndex;

			}

			//Display all text in the panel log.
			childTextLogText.text = text;

		}

	}

	public void AddToVisitedExhibitionList(string _exhibitionName){

		visitedExhibitionList.Add(_exhibitionName);

	}

	//A public function to a button to show the log panel.
	public void ButtonLog(){

		imageSwitch = (imageSwitch*-1);

	}

	//A public function for reset button that will reset the list element.
	public void ButtonReset(){

		//Remove all copy of exhibition from list.
		exhibitionCopyList = new List<ExhibitionCopy>();
		exhibitionCopyList.RemoveAll((o)=>o == null);

		//Reset the list.
		visitedExhibitionList = new List<string>();
		//Remove all component within the list, if any.
		visitedExhibitionList.RemoveAll((o)=>o == null);

	}

	/*
	This function is meant to give this button exhibition random explanation and target next division if a number is
		rolled.
	*/
	public void ButtonTap20D(){

		if(exhibitionCopyList.Count > 0){

			/*
			First thing first we need to check if this game object is other than ButtonExhibitionPrime.
			Because if this button is not button exhibition prime than it has previous index comparison as
				this game object is at least the second index from all button exhibitions within the scene.
			*/
			if(exhibitionCopyList[exhibitionCopyList.Count - 1].exhibitionName != "ButtonExhibitionPrime"){

				/*
				Check if this game object has its set of explanations in ExhibitionDatabase.
				If, so I have made this program so that exhibitionExplanationAmount returns any number other than -1
					if this ButtonExhibition has explanation in ExhibitionDatabase.
				*/
				if(
					GameObject.Find(
						exhibitionCopyList[exhibitionCopyList.Count - 1].exhibitionName
					).GetComponent<ScriptButtonExhibition>().exhibitionExplanationAmount != -1
				){

					/*
					Temporary local variable to detect last rolled game object.
					Which means what is game object that is properly visited before this game object visited.
					What I meant by properly visited is a ButtonExhibition that has tapped and rolled to get
						its explanation and its target exhibition.
					*/
					GameObject lastRolledGameObject = null;

					//Backward loop through all visitedExhibitionList to find out the latest properly visited exhibition.
					for(int i = visitedExhibitionList.Count - 1; i >= 0; i --){

						/*
						We know the latest properly visited exhibition by take look on its targetExhibitionList.
						targetExhibitionList is the last thing assigned before visitor got to another exhibition.
						So, if an exhibition has its target exhibition assigned, we can sure that it is a properly visited exhibition.
						So we do backward loop to find the first exhibition that has targetExhibitionList.
						*/
						if(
							exhibitionCopyList[i].properlyVisited == true &&
							lastRolledGameObject == null
						){
							/*
							We basically just need to do loop up until lastRolledGameObject is not null.
							After it gets its value assigned we practicaly do not need the loop anymore.
							*/
							lastRolledGameObject = GameObject.Find(exhibitionCopyList[i].exhibitionName);

							//Generate random index of explanation.
							int randomExplanationIndex = Random.Range(0, GameObject.Find(exhibitionCopyList[exhibitionCopyList.Count - 1].exhibitionName).GetComponent<ScriptButtonExhibition>().exhibitionExplanationAmount);

							/*
							PENDING: I need to change the if statement below if I want to make dynamic entry of target exhibition.
							This if statement is to check whether the player is in correct exhibition, if so give explanation.
							*/
							if(
								exhibitionCopyList[exhibitionCopyList.Count - 1].exhibitionName == "ButtonExhibition" + exhibitionCopyList[i].targetExhibitionIndex[0] ||
								exhibitionCopyList[exhibitionCopyList.Count - 1].exhibitionName == "ButtonExhibition" + exhibitionCopyList[i].targetExhibitionIndex[1]
							){
								
								exhibitionCopyList[exhibitionCopyList.Count - 1].randomExplanation =
									scriptExhibitionDatabase.exhibitionArray[
										int.Parse(
											exhibitionCopyList[exhibitionCopyList.Count - 1].exhibitionName.Replace("ButtonExhibition", "")
										)
									].explanationArray[randomExplanationIndex];
								
							}

						}

					}

				}

				//If there is no explanation found in database.
				else{ exhibitionCopyList[exhibitionCopyList.Count - 1].randomExplanation = "No Explanation!"; }

			}
			/*
			If this gameObject is ButtonExhibition prime, then it is not necessary to do checking
				on previous exhibition and last properly visited exhibition.
			*/
			else if(exhibitionCopyList[exhibitionCopyList.Count - 1].exhibitionName == "ButtonExhibitionPrime"){

				if(
					GameObject.Find(
						exhibitionCopyList[exhibitionCopyList.Count - 1].exhibitionName
					).GetComponent<ScriptButtonExhibition>().exhibitionExplanationAmount != -1
				){

					int randomExplanationIndex = Random.Range(0, GameObject.Find(exhibitionCopyList[exhibitionCopyList.Count - 1].exhibitionName).GetComponent<ScriptButtonExhibition>().exhibitionExplanationAmount);
					exhibitionCopyList[exhibitionCopyList.Count - 1].randomExplanation =
						scriptExhibitionDatabase.exhibitionArray[0].explanationArray[randomExplanationIndex];
						
				}
				else{ exhibitionCopyList[exhibitionCopyList.Count - 1].exhibitionName = "No Explanation!"; }

			}

		}

	}

	//Set a string to be displayed in notification panel.
	private void SetNotification(string _notification){

		//I feel retarded writing the code below.
		scriptPanelNotification.textText.text = _notification;

	}

}