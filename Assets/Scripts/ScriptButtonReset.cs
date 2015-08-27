using UnityEngine;
using System.Collections;
public class ScriptButtonReset:MonoBehaviour{

	//Get reference to panel notification.
	private GameObject panelNotification;
	private ScriptPanelNotification scriptPanelNotification;

	void Start(){

		//Put reference to panel notification.
		panelNotification = GameObject.Find("PanelNotification");
		scriptPanelNotification = panelNotification.GetComponent<ScriptPanelNotification>();

	}

	void Update(){}

	private void SetNotification(string _notification){

		scriptPanelNotification.textText.text = _notification;

	}

	public void ButtonReset(){

		SetNotification("Thank you for your visit!");

	}

}