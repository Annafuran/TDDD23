using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	public AudioSource audioData;

   public void PlayGame() {

		if(this.name.Equals("WinningMenu")){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		}
		else if(this.name.Equals("LosingMenu")) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
		}
		else {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
		
	}

	public void QuitGame() {
		Application.Quit();
	}

	public void Start() {

		audioData = GetComponent<AudioSource>();
        audioData.Play(0);
	}
}

