using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scMenu : MonoBehaviour {

	//Janelas
	public GameObject[] windows;

	//musica
	public AudioSource sourMusic;
	public AudioClip audMusic;
	public Text txtMute;
	static bool boolMute;
	public static int intPage;

	//void de inicializacao
	void Start () 
	{
		for (int i = 1; i < windows.Length; i++) {
			windows [i].SetActive (false);
		}
	}

	//void de update
	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape)) {
			Quit ();
		}
	}

	//botao mudo
	public void Mute()
	{
		if (boolMute == false) {
			sourMusic.Stop ();
			txtMute.color = new Color32(255,0,0,255);
		} else {
			sourMusic.Play ();
			txtMute.color = new Color32(255,255,255,255);
		}
		boolMute = !boolMute;
	}

	public void ChangePage (string c){
		if (c == "+" && intPage < scGame.fTotal) {
			intPage++;
		} else if (intPage > 0){
			intPage--;
		}
		Debug.Log ("Page: " + intPage.ToString ());
	}

	//Ocultar janela
	public void HideWindow(int hide){
		windows[hide].SetActive(false);
	}

	//Mostrar janela
	public void ShowWindow(int show){
		windows[show].SetActive(true);
	}

	//botao X
	public void Quit ()
	{
			Application.Quit ();
	}
}