using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//This is a script to corelate text field with buttonCount variable.
public class ScriptTextFieldAmountButtonExhibition:MonoBehaviour{


	/*
	Get a children from this game object named Text.
	For some reason Unity is whining around of me using "as GameObject"
		to parse Transfrom from GameObect.
	Hence, here I am directly using Transform type data.
	*/
	private Transform transformChildText;

	//Get Text component from the children game object.
	private Text childText;

	//Get reference to panelButtonExhibition object within the scene.
	private GameObject panelButtonExhibition;
	//Get its script.
	private ScriptPanelButtonExhibition scriptPanelButtonExhibition;

	void Start(){

		//Assign the child "Text" game object that is a child to this game object.
		transformChildText = gameObject.transform.Find("Text");
		//Assign its Text component to childText variable.
		childText = transformChildText.gameObject.GetComponent<Text>();

		//Get PanelButtonExhibition game object.
		panelButtonExhibition = GameObject.Find("PanelButtonExhibition");
		//Get reference to its script.
		scriptPanelButtonExhibition = panelButtonExhibition.GetComponent<ScriptPanelButtonExhibition>();
	}

	void Update(){


		/*
		Assign childText to buttonCount variable in real time.
		And prevent buttonCount variable to go out of the max and min boundaries.
		*/
		if(childText.text != ""){


			if(int.Parse(childText.text) >= scriptPanelButtonExhibition.maxButtonCount){

				scriptPanelButtonExhibition.buttonCount = scriptPanelButtonExhibition.maxButtonCount;

			}
			else if(int.Parse(childText.text) <= scriptPanelButtonExhibition.minButtonCount){

				scriptPanelButtonExhibition.buttonCount = scriptPanelButtonExhibition.minButtonCount;

			}
			else{

				scriptPanelButtonExhibition.buttonCount = int.Parse(childText.text);

			}
			

		}


	}



}