using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ScriptCard:MonoBehaviour{

	private GameObject panelShowCards;
	private RectTransform panelShowCardsRT;

	private Transform cardImage;
	private Transform cardCore;
	private Transform textExhibitionName;
	private Transform textExhibitionExplanation;

	private RectTransform rt;
	private RectTransform cardImageRT;
	private RectTransform cardCoreRT;
	private RectTransform textExhibitionNameRT;
	private RectTransform textExhibitionExplanationRT;

	public Text textExhibitionNameText;
	public Text textExhibitionExplanationText;

	private float cardCoreProportionWidth = 60;
	private float cardCoreProportionHeight = 83;

	void Start(){

		panelShowCards = GameObject.Find("PanelShowCards");
		panelShowCardsRT = panelShowCards.GetComponent<RectTransform>();

		cardImage = gameObject.transform.Find("CardImage");
		cardCore = gameObject.transform.Find("CardCore");
		textExhibitionName = gameObject.transform.Find("TextExhibitionName");
		textExhibitionExplanation = gameObject.transform.Find("TextExhibitionExplanation");

		rt = gameObject.gameObject.GetComponent<RectTransform>();
		cardImageRT = cardImage.gameObject.GetComponent<RectTransform>();
		cardCoreRT = cardCore.gameObject.GetComponent<RectTransform>();
		textExhibitionNameRT = textExhibitionName.gameObject.GetComponent<RectTransform>();
		textExhibitionExplanationRT = textExhibitionExplanation.gameObject.GetComponent<RectTransform>();
		textExhibitionNameText = textExhibitionName.gameObject.GetComponent<Text>();
		textExhibitionExplanationText = textExhibitionExplanation.gameObject.GetComponent<Text>();

	}

	void Update(){

		cardCoreRT.sizeDelta = new Vector2((panelShowCardsRT.sizeDelta.x/2), (panelShowCardsRT.sizeDelta.x/2)*cardCoreProportionHeight/cardCoreProportionWidth);
		rt.sizeDelta = new Vector2(cardCoreRT.sizeDelta.x, cardCoreRT.sizeDelta.y);
		cardImageRT.sizeDelta = new Vector2(cardCoreRT.sizeDelta.x, cardCoreRT.sizeDelta.y);

		textExhibitionNameRT.anchoredPosition = new Vector2(
			(-1*cardCoreRT.sizeDelta.x/2) + (textExhibitionNameRT.sizeDelta.x/2) + (cardCoreRT.sizeDelta.x*13/240),
			(cardCoreRT.sizeDelta.y/2) - (textExhibitionNameRT.sizeDelta.y/2) - (cardCoreRT.sizeDelta.y*13/332)
		);
		textExhibitionNameText.fontSize = (int)(cardCoreRT.sizeDelta.x*7/120);

		textExhibitionExplanationRT.anchoredPosition = new Vector2(
			(-1*cardCoreRT.sizeDelta.x/2) + (textExhibitionExplanationRT.sizeDelta.x/2) + (cardCoreRT.sizeDelta.x*13/240),
			(-1*cardCoreRT.sizeDelta.y/2) + (textExhibitionExplanationRT.sizeDelta.y/2) + (cardCoreRT.sizeDelta.y*82/332)
		);
		textExhibitionExplanationText.fontSize = (int)(cardCoreRT.sizeDelta.x*7/120);

	}

	public void SetExplanation(string _explanation){

		textExhibitionExplanationText.text = _explanation;

	}

}