using UnityEngine;
using System.Collections;


/*
This script is used for managing size of the
	Vertical Layout Group.
*/
public class ScriptPanelMainUI:MonoBehaviour{

	private int panelHeight = 90;

	private RectTransform rectTransform;

	//Get reference to border and its script for layouting purposes.
	private GameObject border;
	private ScriptBorder scriptBorder;

	//Use this for initialization.
	void Start(){

		//Get RectTransform component of this game object.
		rectTransform = gameObject.GetComponent<RectTransform>();

		//Assign value of border game object and its script.
		border = GameObject.Find("Border");
		scriptBorder = border.GetComponent<ScriptBorder>();
		
	}
	
	//Update is called once per frame.
	void Update(){
		rectTransform.anchoredPosition = new Vector2(0, scriptBorder.borderHorizontalHeight);
		rectTransform.sizeDelta = new Vector2(Screen.width - (scriptBorder.borderHorizontalHeight*2), panelHeight);
	}
}