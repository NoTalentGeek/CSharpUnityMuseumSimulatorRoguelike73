using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
This script is for managing anything player game object related.
For example, this script can reset player to initial position
	regardless of screen resolution.
*/
public class ScriptPlayer:MonoBehaviour{

	//Whether or not the player reset button is pressed.
	private bool playerReset = false;

	/*
	Player game object should know their parent they are in.
	So that we need to take reference at real time in which game object
		this player become a chiold for.
	*/
	public GameObject parent = null;

	//Get reference to border and its script for layouting purposes.
	private GameObject border;
	private ScriptBorder scriptBorder;

	//Hold all button exhibition within the scene.
	private GameObject[] buttonExhibitionArray;

	//Get this game object image.
	private Image image;

	//RectTransform of this game object.
	private RectTransform rectTransform;

	void Start(){

		//Get this game object image component.
		image = gameObject.GetComponent<Image>();

		//Get the RectTransform component of this game object.
		rectTransform = gameObject.GetComponent<RectTransform>();

		//Assign value of border game object and its script.
		border = GameObject.Find("Border");
		scriptBorder = border.GetComponent<ScriptBorder>();
		
	}

	void Update(){

		//Get to know this game object parent.
		parent = gameObject.transform.parent.gameObject;

		//Change the sizeDelta based on the Screen.width.
		rectTransform.sizeDelta = new Vector2(Screen.height/5, Screen.height/5);

	}

	//Put the reset button function here.
	public void ButtonReset(){

		buttonExhibitionArray = GameObject.FindGameObjectsWithTag("ButtonExhibition");
		gameObject.transform.SetParent(GameObject.Find("MainCanvas").transform);

		//Disable the image component from this game object.
		image.enabled = false;

		//Set button exhibition boolean variable to false.
		for(int i = 0; i < buttonExhibitionArray.Length; i ++){

			buttonExhibitionArray[i].GetComponent<ScriptButtonExhibition>().SetMovePlayer(false);

		}
		
	}

}
