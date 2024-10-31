using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class scGame : MonoBehaviour {

	public int intLevel;
	public static float fTotal;
	int intCurrent;
	int intRight;
	List<int> intRandomized;
	public Level[] levels;
	public Sprite spriDefault;

	[Header("Sound Elements")]
	public AudioSource rightSource;
	public AudioSource wrongSource;
	public AudioSource finishSource;
	public AudioSource clickSource;

	[Header("Graphic Elements")]
	public Text txtQuestion;
	public Button[] btnAnswers;
	public SpriteRenderer renderBg;
	public Text[] txtAnswers;
	public Text txtTotal;
	public Text txtScore;
	public Text txtResult; 
	public Text[] txtLevels;
	public GameObject cvLevel;
	public GameObject cvQuiz;
	public GameObject cvUnvailable;
	public GameObject cvResult;

	[System.Serializable]
	public class Level{
		[Header("Level Information")]
		public string name;
		//public int level;
		public Color32 mainColor;
		public Color32 secondColor;
		public Question[] questions;	
		public Sprite[] backgrounds;
	}

	[System.Serializable]
	public class Question{
		public string title;
		public string[] answers;
		public char right;
	}

	public void Start(){
		fTotal = levels.Length / 6;
		for (int i = 0; i < 6; i++) {
			if ((i + scMenu.intPage * 6) < levels.Length) {
				txtLevels [i].text = levels [i + scMenu.intPage * 6].name;
			}
		}
	}

	public void Reset (){
		//intRandomized = new List<int> (levels [intLevel].questions.Length);
		intCurrent = 0;
		intRight = 0;

		renderBg.sprite = spriDefault;
		txtTotal.text = "0/" + levels [intLevel].questions.Length;
		txtQuestion.color = levels [intLevel].mainColor;
		for (int c = 0; c < 5; c++) {
			txtAnswers [c].color = levels [intLevel].mainColor;
			btnAnswers [c].targetGraphic.color = levels [intLevel].secondColor;
		}
	}		

	public void LevelClick (int level){
		if (level != -1) {
			intLevel = level + scMenu.intPage * 6;
		}
		if (level + scMenu.intPage * 6 < levels.Length) {
			Reset ();
			ChangeQuestion (intLevel);

			Debug.Log ("Current level: " + intLevel.ToString ());
		}
	}

	public void ChangeQuestion (int l){
		if (intCurrent < levels [l].questions.Length) {
			txtQuestion.text = levels [l].questions [intCurrent].title;
			renderBg.sprite = levels[l].backgrounds[Random.Range(0,levels[l].backgrounds.Length)];
			for (int z = 0; z < 5; z++){
				txtAnswers [z].text = levels [l].questions [intCurrent].answers [z];
			}
			intCurrent++;
		}else{
			cvQuiz.SetActive (false);
			cvResult.SetActive (true);
			finishSource.Play();

			if (intRight == 0) {
				txtResult.text = "Péssimo";
				txtResult.color = new Color32 (255, 0, 0, 255);				
			}
			else if (intRight <= (levels [l].questions.Length * 0.20)) {
				txtResult.text = "Ruim";
				txtResult.color = new Color32 (255, 140, 0, 255);
			}
			else if (intRight <= (levels [l].questions.Length * 0.60)) {
				txtResult.text = "Bom";
				txtResult.color = new Color32 (255, 255, 0, 255);			
			}
			else if (intRight <= (levels [l].questions.Length * 0.80)) {
				txtResult.text = "Muito Bom";
				txtResult.color = new Color32 (0, 200, 0, 255);
			}
			else if (intRight == levels [l].questions.Length) {
				txtResult.text = "Excelente";
				txtResult.color = new Color32 (0, 255, 0, 255);
			}
			txtScore.text = intRight.ToString() + "/" + levels [l].questions.Length.ToString();
		}
		txtTotal.text = intCurrent.ToString() + "/" + levels [l].questions.Length.ToString();
	}

	public void ClickAnswer(string alt){
		/*
		intRandomized = new List<int> (levels [intLevel].questions.Length); //Should be moved

		//Gera numero randomico
		
		bool original = true;

		do{
			int random = Random.Range(0, intRandomized.Count);

			for (int i = 0; i < intRandomized.Count; i++)
			{
				if (random == intRandomized[i]){
					original = false;
				}
			}

		}while(original == false);
		*/	

		if (levels [intLevel].questions [intCurrent-1].right == alt[0]) {
			intRight++;
			rightSource.Play();
		}
		else
		{
			wrongSource.Play();
		}

		ChangeQuestion (intLevel);
	}
}