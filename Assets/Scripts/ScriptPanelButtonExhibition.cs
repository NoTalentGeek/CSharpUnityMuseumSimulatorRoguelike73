using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/*
Script to instantiate exhibition buttons and
	adjust their position.
To - do list:
	* Make a scroll bar for everything above screen size.
	* Make a the alignment to be adjusted in middle if the screen
		is not capable of holding them all fitly.
	* Or, make the size of the buttons adjustable depeding on screen size.
*/

public class ScriptPanelButtonExhibition:MonoBehaviour{

	/*
	Example of Magic the Gathering Card Proportion.
	With this proportion you input the height of the card.
	*/
	private float panelProportion = (334.0f/239.0f);

	public int buttonCount = 30; //Current button in the screen.
	//Set up maximum button on screen and minimum button on screen.
	public int maxButtonCount = 50;
	public int minButtonCount = 0;
	//To detect when there is change in ow many buttons on screen.
	private int previousButtonCount = 0;
	//Reference to button exhibition prefab.
	public GameObject buttonExhibitionReference;
	/*
	A variable that handle as a temporary button exhibition game object before
		it get into list.
	*/
	private GameObject buttonExhibition;

	/*
	Lesson learnt here!
	If you have a public object you want to make a List of.
	That List must also be in public.
	For example the code below if you change into private will
		result in infinite loop error.
	*/
	public List<GameObject> buttonExhibitionList;
	private RectTransform rectTransform;

	//Get the grid layout group component.
	private GridLayoutGroup gridLayoutGroup;

	//Get reference to border and its script for layouting purposes.
	private GameObject border;
	private ScriptBorder scriptBorder;

	//Variables to holds game object Panel Log.
	private GameObject panelLog;
	private ScriptPanelLog scriptPanelLog;

	//Variables to holds game object Player.
	private GameObject player;
	private ScriptPlayer scriptPlayer;



	void Start(){


		/*
		Get the reference from the RectTransform component within this
			game object.
		*/
		rectTransform = gameObject.GetComponent<RectTransform>();

		//Get reference from a GridLayoutGroup from this game object.
		gridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();

		//Assign value of border game object and its script.
		border = GameObject.Find("Border");
		scriptBorder = border.GetComponent<ScriptBorder>();

		//Assign value to the panelLog and scriptPanelLog.
		panelLog = GameObject.Find("PanelLog");
		scriptPanelLog = panelLog.GetComponent<ScriptPanelLog>();

		//Assign value to player and scriptPlayer,.
		player = GameObject.Find("Player");
		scriptPlayer = player.GetComponent<ScriptPlayer>();


	}



	void Update(){

		//Bound the value of buttonCount.
		if(buttonCount >= maxButtonCount){ buttonCount = maxButtonCount; }
		else if(buttonCount <= minButtonCount){ buttonCount = minButtonCount; }


		//Change the width and the height of cells in GridLayoutGroup according to screen size.
		gridLayoutGroup.cellSize = new Vector2(
			((Screen.width - (scriptBorder.borderHorizontalHeight*2))/5),
			(((Screen.width - (scriptBorder.borderHorizontalHeight*2))/5)*panelProportion)
		);

		//If button count is zero then delete all available buttons within the screen.
		if(buttonCount == 0){

			//Reset the Player game object everytime there is a value change.
			scriptPanelLog.ButtonReset();
			scriptPlayer.ButtonReset();

			for(int i = 0; i < previousButtonCount; i ++){

				Destroy(buttonExhibitionList[i]);

			}

			buttonExhibitionList = new List<GameObject>();
			//Remove all component within the list, if any.
			buttonExhibitionList.RemoveAll((o)=>o == null);

			/*
			Make the previousButtonCount to be equal with the buttonCount
				, thus the trigger can be activated again.
			*/
			previousButtonCount = buttonCount;

		}

		/*
		If the buttonCount is not zero, do some checking before creating
			new exhibition buttons.
		*/
		if(
			previousButtonCount != buttonCount
			&& buttonCount != 0
		){

			//Reset the Player game object everytime there is a value change.
			scriptPanelLog.ButtonReset();
			scriptPlayer.ButtonReset();

			//Destroy the previously created buttons.
			for(int i = 0; i < previousButtonCount; i ++){
				Destroy(buttonExhibitionList[i]);
			}

			buttonExhibitionList = new List<GameObject>();
			buttonExhibitionList.RemoveAll((o)=>o == null);

			/*
			Create new buttons based on how much value on buttonCount
				variable.
			*/
			for(int i = 0; i < buttonCount; i ++){

				/*
				Instantiate local game variables to get reference to
					Text game object and Text component inside it.
				*/
				Transform childTransform;
				Text childText;

				/*
				Instantiate the game object as a parent to a
					canvas object that is already in screen.
				*/
				buttonExhibition = Instantiate(
					buttonExhibitionReference,
					new Vector3(0, 0, 0),
					Quaternion.identity
				) as GameObject;

				/*
				Assign childTransform and its Text game component.
				This childTransform is a Transform component from a child of
					this game object.
				*/
				childTransform = buttonExhibition.transform.Find("Text");
				childText = childTransform.gameObject.GetComponent<Text>();

				//Button name convention.
				childText.text = "Exhibition " + i;
				if(childText.text == "Exhibition 0"){

					childText.text = "Exhibition Prime";

				}

				//Some naming convention.
				buttonExhibition.name = "ButtonExhibition" + i;
				if(buttonExhibition.name == "ButtonExhibition0"){

					buttonExhibition.name = "ButtonExhibitionPrime";

				}

				//Set parent and add it to List.
				buttonExhibition.transform.SetParent(gameObject.transform);
				buttonExhibitionList.Add(buttonExhibition);

			}

			/*
			Make the previousButtonCount to equal the button count,
				thus can be triggered again.
			*/
			previousButtonCount = buttonCount;

		}

	}

}