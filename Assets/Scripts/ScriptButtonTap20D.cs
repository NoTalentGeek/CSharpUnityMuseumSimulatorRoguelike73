using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ScriptButtonTap20D:MonoBehaviour{

	//Get button component from this game object.
	private Button button;
	//We need to detect the change in parent.
	private GameObject parent;
	private GameObject parentPrevious;
	//Get the player game object from the scene.
	private GameObject player;
	private ScriptPlayer scriptPlayer;

	void Start(){
		button = gameObject.GetComponent<Button>();
		player = GameObject.Find("Player");
		scriptPlayer = player.GetComponent<ScriptPlayer>();
	}

	void Update(){

		/*
		parent = scriptPlayer.parent;
		//Check wether there is change in player parent or not.
		if(parent != parentPrevious){

			if(parent.name != "MainCanvas"){
				//Set the listener of a button.
				button.GetComponent<Button>().onClick.AddListener(
					() => { scriptPlayer.parent.GetComponent<ScriptButtonExhibition>().ButtonTap20D(); }
				);
				parentPrevious = parent;	
			}

		}
		*/
		
	}

}