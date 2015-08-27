using UnityEngine;
using System.Collections;

/*
A script to manage a panel that holds scroll for buttos
	exhibition.
*/
public class ScriptScrollButtonExhibition:MonoBehaviour{

	//A variable to take a reference from this game object RectTransform.
	private RectTransform rectTransform;

	//Get reference to border and its script for layouting purposes.
	private GameObject border;
	private ScriptBorder scriptBorder;
	//Get reference to panel notification.
	private GameObject panelNotification;
	private ScriptPanelNotification scriptPanelNotification;

	void Start(){

		//Get the reference from this game object RectTransform component.
		rectTransform = gameObject.GetComponent<RectTransform>();

		//Assign value of border game object and its script.
		border = GameObject.Find("Border");
		scriptBorder = border.GetComponent<ScriptBorder>();
		//Panel notification.
		panelNotification = GameObject.Find("PanelNotification");
		scriptPanelNotification = panelNotification.GetComponent<ScriptPanelNotification>();

	}

	void Update(){

		//Set the position of this game object on screen.
		rectTransform.anchoredPosition = new Vector2(0, (-scriptBorder.borderHorizontalHeight - scriptPanelNotification.rt.sizeDelta.y));

		/*
		Adjust the size of this game object according to the height of PanelMainUI and
			Player game object.
		*/
		rectTransform.sizeDelta = new Vector2(
			Screen.width - (scriptBorder.borderHorizontalHeight*2),
			Screen.height - (
				GameObject.Find("PanelMainUI").GetComponent<RectTransform>().sizeDelta.y
				+ (scriptBorder.borderHorizontalHeight*2)
				+ scriptPanelNotification.rt.sizeDelta.y
			)
		);

	}

}