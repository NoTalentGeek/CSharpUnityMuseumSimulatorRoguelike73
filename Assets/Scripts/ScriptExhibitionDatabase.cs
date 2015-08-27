using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ScriptExhibitionDatabase:MonoBehaviour{

	/*
	totalBoss variable is how many exhibition boss is in the scene.
	What I meant by exhibition boss is an exhibition that has strong theme
		that lead the visitor/player to another cluster of exhibition.
	*/
	public int totalBoss = 5;
	//This variable below is to detect change in totalBoss variable.
	private int totalBossPrevious;

	//How many exhibitions details you want to provide.
	public int howManyExhibition = 30;
	private int howManyExhibitionPrevious; //Detect change in amount of exhibition.

	[Serializable]
	public class Exhibition{

		/*
		These both variables below is for detecting which exhibition is the boss and
			which one is the minimum variable.
		From what I meant by boss exhibition is that exhibition that a visitor/player will
			visit last from a cluster.
		The minimum exhibition is the least exhibition index wise from an exhibition cluster.
		*/
		public bool isBossExhibition;
		public bool isMinExhibition;

		public int threshold = 10;

		//The boss exhibition from this exhibition's cluster.
		public int bossExhibition;
		//The smallest exhibition index wise from this exhibition's cluster.
		public int minExhibition;

		/*
		List of exhibitions that can be visited after this exhibition.
		These exhibitions will divided into two sets depending on dice roll.
		*/
		public int[] targetExhibitionArray = new int[4];

		/*
		Amount of explanations in this exhibition.
		Visitor/player get this randomly when TAPPING the dice.
		*/
		public string[] explanationArray = new string[4];
		/*
		In this prototype I use 4 as both target and explanation per exhibition.
		It is actually can be changed easily WHEN THE GAME START in the inspector menu.
		I want to make this thing tob eintegrated into UI soon.
		*/

		//Blank constructor.
		public Exhibition(){}

	}
	//An array database that contains all information an exhibition needs.
	public Exhibition[] exhibitionArray;

	void Start(){

		/*
		Preventing totalBoss and howManyExhibition value to go below 0.
		This should be prevented so that division by zero is never happened.
		*/
		if(totalBoss <= 0){ totalBoss = 1; }
		if(howManyExhibition <= 0){ howManyExhibition = 1; }
		/*
		Preventing totalBossAmount to be the same or less than howManyExhibition.
		So that the division result from (howManyExhibition/totalBoss) is always above 1.
		*/
		if(howManyExhibition < totalBoss){
			howManyExhibition = totalBoss + 1;
		}
		/*
		Initiate the basic set.
		Within this basic set, I use dummy explanation and dummy target exhibition.
		*/
		InitiateBasicSets();

	}

	void Update(){

		/*
		Preventing totalBoss and howManyExhibition value to go below 0.
		This should be prevented so that division by zero is never happened.
		*/
		if(totalBoss <= 0){ totalBoss = 1; }
		if(howManyExhibition <= 0){ howManyExhibition = 1; }
		/*
		Preventing totalBossAmount to be the same or less than howManyExhibition.
		So that the division result from (howManyExhibition/totalBoss) is always above 1.
		*/
		if(howManyExhibition < totalBoss){
			howManyExhibition = totalBoss + 1;
		}

		/*
		Detecting change in totalBoss and howManyExhibition.
		If there is a change that re - init the set.
		*/
		if(
			(totalBoss != totalBossPrevious)
			|| (howManyExhibition != howManyExhibitionPrevious)
		){

			InitiateBasicSets();

			//Make the value same so that this section of code only happened once.
			totalBossPrevious = totalBoss;
			howManyExhibitionPrevious = howManyExhibition;

		}

	}

	private void InitiateBasicSets(){

		exhibitionArray = new Exhibition[howManyExhibition];

		/*
		Cluster is how many exhibitions a visitor COULD go before go to 
			boss exhibition.
		*/
		int totalExhibitionsPerCluster = 
			(exhibitionArray.Length/totalBoss);

		//Which exhibitions are the boss exhibition.
		List<int> bossExhibitionList = new List<int>();
		//Which exhibitions are the minimum exhibition for each clusters.
		List<int> minExhibitionList = new List<int>();

		for(int i = 0; i < exhibitionArray.Length; i ++){

			/*
			Initiate all exhibition.
			I can done this outside this loop, however, if I do Unity
				will give you null error at the first tick.
			*/
			exhibitionArray[i] = new Exhibition();

			//Give dummy explanations.
			for(int j = 0; j < exhibitionArray[i].explanationArray.Length; j ++){

				exhibitionArray[i].explanationArray[j] = "Exhibition" + i + "Explanation" + j;

			}

			//Assign boss exhibitions to list.
			if(i%totalExhibitionsPerCluster == 0 && i != 0){
				exhibitionArray[i - 1].isBossExhibition = true;
				bossExhibitionList.Add(i - 1);
			}
			else if(i == exhibitionArray.Length - 1){
				exhibitionArray[i].isBossExhibition = true;
				bossExhibitionList.Add(i);
			}
			else{ exhibitionArray[i].isBossExhibition = false; }

			//Assign min exhibition to list.
			if(i%totalExhibitionsPerCluster == 0){
				exhibitionArray[i].isMinExhibition = true;
				minExhibitionList.Add(i);
			}
			else{ exhibitionArray[i].isMinExhibition = false; }

		}

		/*
		We have assigned the index of which exhibitions are boss and which exhibitions are min.
		Here we assigned those index to the array Exhibition.
		*/
		for(int i = 0; i < exhibitionArray.Length; i ++){

			for(int j = 0; j < totalBoss; j ++){

				
				if(i >= minExhibitionList[j] && i <= bossExhibitionList[j]){

					exhibitionArray[i].bossExhibition = bossExhibitionList[j];
					exhibitionArray[i].minExhibition = minExhibitionList[j];

				}
				

			}

			/*
			Give target explanation to each exhibition.
			The easiest one to set is the minimum and boss exhibition.
			This thing is more complicated to understand than it seems.
			Basically I asked the array to input target exhibition based on the previous exhibition
				minExhibition and boss exhibition.
			Hence, the i - 1.
			If i is equal to target then change the target, because if you are at exhibition i you cannot
				ask your player/visitor to visit the same exhibition again.
			*/
			bool reset = false; //If the number within a loop is reseted than continue from this number.
			int lastReset = 0; //Temporary variable to hold last position.
			int target = 0; //Target exhibition based on the calculation below.
			//These two variables is just for reset the reset boolean to false ahahahaa...
			int iNow = i;
			int iPrevious = 0;

			/*
			Loop through how many target exhibition from current exhibition.
			By target I mean the destined target exhibitions.
			*/
			for(int j = 0; j < exhibitionArray[i].targetExhibitionArray.Length; j ++){

				/*
				If exhibition i is a boss exhibition.
				Which means the last exhibition from its cluster then assign only two target exhibitions.
				The first one is the min exhibition of exhibition i.
				The latter one is the next min exhibition from next cluster.
				*/
				if(exhibitionArray[i].isBossExhibition){
					if(i != exhibitionArray.Length - 1){
						exhibitionArray[i].targetExhibitionArray[j] =
							((j + 1)%2 == 0)? i + 1 : exhibitionArray[i].minExhibition;
					}
					else if(i == exhibitionArray.Length - 1){
						exhibitionArray[i].targetExhibitionArray[j] = 0;
					}
				}

				//If min exhibition, then assign four next exhibition from this exhibition i.
				else if(exhibitionArray[i].isMinExhibition){
					exhibitionArray[i].targetExhibitionArray[j] = i + (j + 1);
				}

				/*
				Things get trickier here.
				But the core things is still the same like I have mentioned above.
				If target is the same with target exhibition, then increase i with 1.
				If target is exceed with exhibition i bossExhibition, then reset i so that it is the same with
					exhibition i minExhibition.
				*/
				else{

					/*
					If exhibition change than reset back.
					Hence, target can take the initial value.
					*/
					iNow = i;
					if(iNow != iPrevious){
						reset = false;
						iPrevious = iNow;
					}
					//Take the initial value of this exhiibiton from the last exhibition last index target.
					if(!reset){
						target =
							exhibitionArray[i - 1].targetExhibitionArray[exhibitionArray[i].targetExhibitionArray.Length - 1] + (j + 1);
					}
					else if(reset){
						target = lastReset + 1;
						lastReset = target;
						reset = true;
					}

					if(target > exhibitionArray[i].bossExhibition){
						target = exhibitionArray[i].minExhibition;
						lastReset = target;
						reset = true;
					}
					if(target == i){
						target = i + 1;
						lastReset = target;
						reset = true;
					}
					exhibitionArray[i].targetExhibitionArray[j] = target;

				}

			}

		}

	}

}