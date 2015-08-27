using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class ScriptPanelContentShowCards:MonoBehaviour{

	//Get the grid layout group component.
	private GridLayoutGroup gridLayoutGroup;

	public GameObject cardReference;
	private GameObject card;
	private bool instantiateCardTrigger = false;

	private HashSet<string> explanationHashSet;
	private string exhibitionName;
	private string explanation;
	private int explanationHashSetCount = 0;
	private int explanationHashSetCountPrevious = 0;

	void Start(){

		gridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();
		explanationHashSet = new HashSet<string>();

	}

	void Update(){

		if(GameObject.FindGameObjectsWithTag("Card").Length >= 1){
			//Change the width and the height of cells in GridLayoutGroup according to screen size.
			gridLayoutGroup.cellSize = new Vector2(
				GameObject.FindGameObjectsWithTag("Card")[0].GetComponent<RectTransform>().sizeDelta.x,
				GameObject.FindGameObjectsWithTag("Card")[0].GetComponent<RectTransform>().sizeDelta.y
			);
			
		}

		explanationHashSetCount = explanationHashSet.Count;
		if(explanationHashSetCount != explanationHashSetCountPrevious){

			instantiateCardTrigger = true;
			explanationHashSetCountPrevious = explanationHashSetCount;

		}
		if(instantiateCardTrigger){

			Image imageCard;
			ScriptCard scriptCard;

			card = Instantiate(
				cardReference,
				new Vector3(0, 0, 0),
				Quaternion.identity
			) as GameObject;
			card.transform.SetParent(gameObject.transform);

			imageCard = card.transform.Find("CardImage").GetComponent<Image>();
			scriptCard = card.GetComponent<ScriptCard>();

			imageCard.sprite = Resources.Load<Sprite>("AssetsImage/MuseumCards/" + exhibitionName);
			scriptCard.gameObject.transform.Find("TextExhibitionExplanation").GetComponent<Text>().text = explanation;
			scriptCard.gameObject.transform.Find("TextExhibitionName").GetComponent<Text>().text = exhibitionName.Replace("Button", "");

			instantiateCardTrigger = false;
		}

	}

	public void AddToExplanationHashSet(string _explanation, string _exhibitionName){

		explanationHashSet.Add(_explanation);
		explanation = _explanation;
		exhibitionName = _exhibitionName;

	}

	public void ButtonReset(){}

}