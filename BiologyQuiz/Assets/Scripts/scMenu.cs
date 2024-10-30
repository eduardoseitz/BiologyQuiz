using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scMenu : MonoBehaviour {

	//musica
	public AudioSource sourMusic;
	public AudioClip audMusic;
	public Text txtMute;
	static bool boolMute;
	public static int intPage;

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

	//botao X
	public void Quit ()
	{
			Application.Quit ();
	}
}