using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor.SceneManagement; 
#endif
using System.Collections.Generic;

public class scQuiz : MonoBehaviour {

	//elementos graficos
	public Text Q;
	public Text A;
	public Text B;
	public Text C;
	public Text D;
	public Button bA;
	public Button bB;
	public Button bC;
	public Button bD;
	public SpriteRenderer BG;
	public Canvas mainCanvas;
	public Canvas resCanvas;
	public Text total;
	public Text score;
	public Text feed;

	//efeitos sonoros
	public AudioSource effectSound;
	public AudioClip rightSound;
	public AudioClip wrongSound;
	public AudioClip allRightSound;
	public AudioClip allWrongSound;

	//variaveis contendo as perguntas e as respostas
	public string[] question;
	public string[] optionA;
	public string[] optionB;
	public string[] optionC;
	public string[] optionD;
	public string[] right;
	//public string[] explanation;
	public Sprite[] background;

	//variaveis do resultado
	int curQuest = 0;
	int rightQuest;
	int totalQuest;
	int intRandom;
	List<int> usedValues = new List<int>();

	//gera numeros aleatorios
	void Randomize() //mostra textos e imagens de forma aleatoria
	{
		intRandom = Random.Range(0, question.Length);
		while(usedValues.Contains(intRandom))
		{
			intRandom = Random.Range(0, question.Length);
		}

		BG.sprite = background[Random.Range(0, background.Length)]; 
		Q.text = question[intRandom];
		A.text = optionA[intRandom];
		B.text = optionB[intRandom];
		C.text = optionC[intRandom];
		D.text = optionD[intRandom];
		total.text = "Total: " + (curQuest + 1) + "/" + question.Length;
		curQuest++;
	}

	//void de inicializacao
	public void Start ()
	{
		totalQuest = question.Length;
		mainCanvas.enabled = true;
		resCanvas.enabled = false;
		Randomize ();
	}

	//clique em algum botao
	public void ClickButton (string answer)
	{
		if (answer == right [intRandom]) {
			effectSound.clip = rightSound;
			effectSound.volume = 0.5f; effectSound.Play ();
			StartCoroutine("Wait");
			Debug.Log("certo");
			rightQuest++;
		} else {
			effectSound.clip = wrongSound;
			effectSound.volume = 1f; effectSound.Play ();
			Debug.Log ("errado");
		}
		if (curQuest < question.Length) {
			Randomize ();
		} else {
			Result ();
		}

	}
	//tentar novamente
	public void Again (int l)
	{
		resCanvas.enabled = false;
		#if UNITY_EDITOR
		EditorSceneManager.LoadScene (l);
		#else
		Application.LoadLevel (l);
		#endif
	}

	//voltar ao menu
	public void Menu ()
	{
		resCanvas.enabled = false;
		#if UNITY_EDITOR
		EditorSceneManager.LoadScene (0);
		#else
		Application.LoadLevel (0);
		#endif
	}

	//botao sair
	public void Exit ()
	{
		Application.Quit ();
	}

	//Apresenta o resultado no canvas result
	public void Result()
	{
		mainCanvas.enabled = false;
		resCanvas.enabled = true;
		score.text = rightQuest + "/" + totalQuest;
		if (rightQuest == 0) {
			feed.text = "Horrível"; //0%
			feed.color = new Color32(255,0,0,255);//red
			effectSound.clip = allWrongSound;
			effectSound.volume = 0.5f; effectSound.Play ();
		}
		else if (rightQuest <= (totalQuest * 0.20)) {
			feed.text = "Muito Ruim"; //20%
			feed.color = new Color32(255,160,60,255); //orange
		}
		else if (rightQuest <= (totalQuest * 0.40)) {
			feed.text = "Ruim"; //40%
			feed.color = new Color32(255,255,60,255); //yellow
		}
		else if (rightQuest <= (totalQuest * 0.60)) {
			feed.text = "Mediano"; //60%
			feed.color = new Color32(60,255,255,255); //cian
		}
		else if (rightQuest <= (totalQuest * 0.80)) {
			feed.text = "Bom"; //80%
			feed.color = new Color32(60,160,160,255); //light green
		}
		else if (rightQuest == totalQuest) {
			feed.text = "Excelente"; //100%
			feed.color = new Color32(0,255,0,255); //green
			effectSound.clip = allRightSound;
			effectSound.volume = 0.5f; effectSound.Play ();
		}
	}
}