using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ScriptBackgroundMainCanvas:MonoBehaviour{

	//Get reference to Border game object and its script.
	private GameObject border;
	private ScriptBorder scriptBorder;

	//Get reference to this game object RectTransform component.
	private RectTransform rectTransform;

	void Start(){

		//Fill in reference for Border game object.
		border = GameObject.Find("Border");
		scriptBorder = border.GetComponent<ScriptBorder>();

		//Take reference to this game object Image component.
		rectTransform = gameObject.GetComponent<RectTransform>();

	}
	void Update(){
		rectTransform.sizeDelta = new Vector2(
			Screen.width - scriptBorder.borderHorizontalHeight, Screen.height - scriptBorder.borderHorizontalHeight
		);
	}

}