using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scMenu : MonoBehaviour {

	//musica
	public Text txtMute;
	static bool boolMute;
	public static int intPage;
	public scGame game;

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
		if (boolMute == false)
		{
			game.finishSource.volume = 0;
			game.wrongSource.volume = 0;
			game.rightSource.volume = 0;
			game.clickSource.volume = 0;
			txtMute.color = new Color32(255,0,0,255);
		} else {
			game.finishSource.volume = 0.5f;
			game.wrongSource.volume = 0.5f;
			game.rightSource.volume = 0.5f;
			game.clickSource.volume = 0.5f;
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