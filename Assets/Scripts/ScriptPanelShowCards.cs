using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ScriptPanelShowCards:MonoBehaviour{

	private int imageSwitch = -1;

	private Image image;
	private RectTransform rt;

	private Transform childTextClickToDismiss;
	private Text childTextClickToDismissText;
	private Transform panelScrollShowCards;
	private Image panelScrollShowCardsImage;
	private Transform panelContentShowCards;
	private Image panelContentShowCardsImage;

	private GameObject border;
	private ScriptBorder scriptBorder;

	void Start(){

		image = gameObject.GetComponent<Image>();
		rt = gameObject.GetComponent<RectTransform>();

		childTextClickToDismiss = gameObject.transform.Find("TextClickToDismiss");
		childTextClickToDismissText = childTextClickToDismiss.gameObject.GetComponent<Text>();
		panelScrollShowCards = gameObject.transform.Find("PanelScrollShowCards");
		panelScrollShowCardsImage = panelScrollShowCards.gameObject.GetComponent<Image>();
		panelContentShowCards = panelScrollShowCards.Find("PanelContentShowCards");
		panelContentShowCardsImage = panelContentShowCards.gameObject.GetComponent<Image>();

		border = GameObject.Find("Border");
		scriptBorder = border.GetComponent<ScriptBorder>();

	}

	void Update(){

		rt.sizeDelta = new Vector2(Screen.width - (scriptBorder.borderHorizontalHeight*2) - 10, Screen.height - (scriptBorder.borderHorizontalHeight*2) -10);

		if(imageSwitch == 1){

			image.enabled = true;
			childTextClickToDismissText.enabled = true;
			panelScrollShowCardsImage.enabled = true;

			GameObject[] cardArray = GameObject.FindGameObjectsWithTag("Card");
			for(int i = 0; i < cardArray.Length; i ++){

				cardArray[i].transform.Find("CardImage").GetComponent<Image>().enabled = true;
				cardArray[i].transform.Find("CardCore").GetComponent<Image>().enabled = true;
				cardArray[i].transform.Find("TextExhibitionName").GetComponent<Text>().enabled = true;
				cardArray[i].transform.Find("TextExhibitionExplanation").GetComponent<Text>().enabled = true;

			}

		}
		else if(imageSwitch == -1){

			image.enabled = false;
			childTextClickToDismissText.enabled = false;
			panelScrollShowCardsImage.enabled = false;

			GameObject[] cardArray = GameObject.FindGameObjectsWithTag("Card");
			for(int i = 0; i < cardArray.Length; i ++){

				cardArray[i].transform.Find("CardImage").GetComponent<Image>().enabled = false;
				cardArray[i].transform.Find("CardCore").GetComponent<Image>().enabled = false;
				cardArray[i].transform.Find("TextExhibitionName").GetComponent<Text>().enabled = false;
				cardArray[i].transform.Find("TextExhibitionExplanation").GetComponent<Text>().enabled = false;

			}

		}

	}

	public void ButtonShowCards(){

		imageSwitch = (imageSwitch*-1);

	}

}