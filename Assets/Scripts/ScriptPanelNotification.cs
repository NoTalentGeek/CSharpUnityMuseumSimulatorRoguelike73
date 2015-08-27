using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ScriptPanelNotification:MonoBehaviour{

	//Get the screen resolution width and height, then check for change.
	private int screenWidth;
	private int screenHeight;
	private int screenWidthPrevious = 0;
	private int screenHeightPrevious = 0;

	//Make reference of position.
	private int yPosition = 0;
	private int yPositionDestination;
	private int yPositionOrigin = 0;

	//Get this game object RectTransform.
	public RectTransform rt;

	/*
	Get reference to this game object child game object and its text
		component.
	*/
	private Transform text;
	public Text textText;
	private string textHolder;
	private string textHolderPrevious;

	//Get reference to Border game object in the screen.
	private GameObject border;
	private ScriptBorder scriptBorder;

	//Get reference to button reset game object and its script component.
	private GameObject buttonReset;
	private RectTransform buttonResetRT;

	void Start(){
	
		//Assign RectTransform for this game object.
		rt = gameObject.GetComponent<RectTransform>();
		//Assign reference to text component of child.
		text = gameObject.transform.Find("Text");
		textText = text.gameObject.GetComponent<Text>();
		//Assign reference to Border game object.
		border = GameObject.Find("Border");
		scriptBorder = border.GetComponent<ScriptBorder>();
		//Assign button reset.
		buttonReset = GameObject.Find("ButtonReset");
		buttonResetRT = buttonReset.GetComponent<RectTransform>();
		//Put the first initial text in textHolder variable.
		textHolder = textText.text;
		textHolderPrevious = textHolder;

	}
	void Update(){
	
		screenWidth = Screen.width;
		screenHeight = Screen.height;

		//Always update the text.
		textHolder = textText.text;
		if(textHolder != textHolderPrevious){
			
			textHolderPrevious = textHolder;

		}

		/*
		Assign the sizeDelta of this game object according to Border
			and screen size.
		But before that we need to know if there is a change in screen
			resolution.
		*/
		if(
			(
				(screenWidth != screenWidthPrevious)
				|| (screenHeight != screenHeightPrevious)
			)
			&&
			(
				(scriptBorder.borderHorizontalHeight != 0)
				&&
				(buttonResetRT.sizeDelta.y != 0)
			)	
		){
		
			rt.sizeDelta = new Vector2(
				screenWidth
			       		- (scriptBorder.borderHorizontalHeight*2),
				buttonResetRT.sizeDelta.y
			);

			screenWidthPrevious = screenWidth;
			screenHeightPrevious = screenHeight;
		
		}

		//Set the position of this game object within the scene.
		rt.anchoredPosition = new Vector2(
			0,
			(-1 * scriptBorder.borderHorizontalHeight)
		);

	}

}
