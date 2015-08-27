using UnityEngine;
using System.Collections;
public class ScriptBorder:MonoBehaviour{

	//Layouting script.
	private GameObject borderBottom;
	private GameObject borderTop;
	private GameObject borderLeft;
	private GameObject borderRight;
	private GameObject borderSideBottomLeft;
	private GameObject borderSideBottomRight;
	private GameObject borderSideTopLeft;
	private GameObject borderSideTopRight;
	private RectTransform rectTransform;
	private RectTransform borderBottomRT;
	private RectTransform borderTopRT;
	private RectTransform borderLeftRT;
	private RectTransform borderRightRT;
	private RectTransform borderSideBottomLeftRT;
	private RectTransform borderSideBottomRightRT;
	private RectTransform borderSideTopLeftRT;
	private RectTransform borderSideTopRightRT;
	private float borderHorizontalWidth;
	public float borderHorizontalHeight;

	private int screenWidth;
	private int screenHeight;
	private int screenWidthPrevious;
	private int screenHeightPrevious;

	void Start(){

		borderBottom = gameObject.transform.Find("BorderBottom").gameObject;
		borderTop = gameObject.transform.Find("BorderTop").gameObject;
		borderLeft = gameObject.transform.Find("BorderLeft").gameObject;
		borderRight = gameObject.transform.Find("BorderRight").gameObject;
		borderSideBottomLeft = gameObject.transform.Find("BorderSideBottomLeft").gameObject;
		borderSideBottomRight = gameObject.transform.Find("BorderSideBottomRight").gameObject;
		borderSideTopLeft = gameObject.transform.Find("BorderSideTopLeft").gameObject;
		borderSideTopRight = gameObject.transform.Find("BorderSideTopRight").gameObject;
		rectTransform = gameObject.GetComponent<RectTransform>();
		borderBottomRT = borderBottom.GetComponent<RectTransform>();
		borderTopRT = borderTop.GetComponent<RectTransform>();
		borderLeftRT = borderLeft.GetComponent<RectTransform>();
		borderRightRT = borderRight.GetComponent<RectTransform>();
		borderSideBottomLeftRT = borderSideBottomLeft.GetComponent<RectTransform>();
		borderSideBottomRightRT = borderSideBottomRight.GetComponent<RectTransform>();
		borderSideTopLeftRT = borderSideTopLeft.GetComponent<RectTransform>();
		borderSideTopRightRT = borderSideTopRight.GetComponent<RectTransform>();

	}
	void Update(){

		screenWidth = Screen.width;
		screenHeight = Screen.height;
		borderHorizontalWidth = Screen.width;
		borderHorizontalHeight = Screen.width/30;
		rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
		borderBottomRT.sizeDelta = new Vector2(borderHorizontalWidth - borderHorizontalHeight, borderHorizontalHeight);
		borderTopRT.sizeDelta = new Vector2(borderHorizontalWidth - borderHorizontalHeight, borderHorizontalHeight);
		borderLeftRT.sizeDelta = new Vector2(borderHorizontalHeight, screenHeight);
		borderRightRT.sizeDelta = new Vector2(borderHorizontalHeight, screenHeight);
		borderSideBottomLeftRT.sizeDelta = new Vector2(borderHorizontalHeight, borderHorizontalHeight);
		borderSideBottomRightRT.sizeDelta = new Vector2(borderHorizontalHeight, borderHorizontalHeight);
		borderSideTopLeftRT.sizeDelta = new Vector2(borderHorizontalHeight, borderHorizontalHeight);
		borderSideTopRightRT.sizeDelta = new Vector2(borderHorizontalHeight, borderHorizontalHeight);

	}

}