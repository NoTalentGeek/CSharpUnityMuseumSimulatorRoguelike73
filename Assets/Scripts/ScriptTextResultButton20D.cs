using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
This function is a button function to generate random number
	for the 20 sided dice.
*/
public class ScriptTextResultButton20D:MonoBehaviour{

	//Determine the boundaries of the random number generated.
	public int randomMin = 1;
	public int randomMax = 20;

	//A variable to hold the generated random number.
	public int randomNumber;

	/*
	Grab the Transform of the child object and then
		take its Text component
	*/
	private Transform child;
	private Text childText;

	/*
	Get reference to panel log game object.
	This script needs to pass random number to panel log.
	*/
	private GameObject panelLog;
	private ScriptPanelLog scriptPanelLog;

	/*
	This button need to know wether the explanation is already given or not.
	So in order to do that this game object need to know the state of current exhibition
		explanation, like whether or not the explanation is already given.
	*/
	private GameObject player;
	private GameObject playerParent;
	private ScriptPlayer scriptPlayer;

	void Start(){
		
		/*
		Get child game object from this game object variable
			and take its Text component.
		*/
		child = gameObject.transform.Find("Text");
		childText = child.gameObject.GetComponent<Text>();

		//Assign all references.
		panelLog = GameObject.Find("PanelLog");
		scriptPanelLog = panelLog.GetComponent<ScriptPanelLog>();
		player = GameObject.Find("Player");
		scriptPlayer = player.GetComponent<ScriptPlayer>();

	}
	void Update(){

		//Set reference so this program know what is player
		playerParent = scriptPlayer.parent;

	}

	public void ButtonRoll20D(){

		//Generate random number.
		randomNumber = Random.Range(randomMin, randomMax);
		if(randomNumber < 10){

			childText.text = "0" + randomNumber;

		}
		else{ childText.text = "" + randomNumber; }

		//This function is to check the current state of visited exhibition.
		if(playerParent.name != "MainCanvas"){

			if(scriptPanelLog.exhibitionCopyList.Count >= 1){
				//Put the rolled number into panel log latest exhibitionCopyList.
				if(
					scriptPanelLog.exhibitionCopyList[scriptPanelLog.exhibitionCopyList.Count - 1].rolledNumber == -1
				){

					scriptPanelLog.exhibitionCopyList[scriptPanelLog.exhibitionCopyList.Count - 1].rolledNumber =
						randomNumber;

				}
			}

		}

	}

}