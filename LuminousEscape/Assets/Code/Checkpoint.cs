using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Checkpoint: MonoBehaviour
{
	public TextMeshProUGUI tutText;

	private bool triggered;
	private bool skipTutorial;
	public WinController heartController;
	//public GameObject dad;
	//public GameObject mom;
	private int heartCounter;
	
	
	void Awake() {
		triggered = false;
		skipTutorial = false;
		heartCounter = heartController.getHeartCounter();

		tutText.SetText( "A/Left key and D/Right key moves your character.\n\n"
				+ "Press Enter to continue (to skip tutorials, press V)");
	}

	void OnTriggerEnter(Collider collider) {

		if(!triggered) {

			if(collider.gameObject.layer == LayerMask.NameToLayer("Player") && !skipTutorial) {

				Trigger();			
			}
			if(this.name.Equals("Finish")) {

				Trigger();	
			}
		}
	}


	void Trigger() {
	
		switch(this.name) {

			//Gravity control (on player)
			case "Part1": 
			tutText.SetText("Use the movement-keys to move around WHILE pressing the SPACE BAR to use your gravity control ability! \n\n" 
				+ "Press ENTER to continue (to skip the tutorial, press V)");									
			break;
			//First apple
			case "Part2":
				tutText.SetText("You have found an apple, you will find it in your inventory." +
				"You open/close your inventory by clicking TAB. \n" + 
				"Tip: Eating apples increases your health. \n\n"
				+ "Press Enter to continue (to skip the tutorial, press V)");
			GameObject.Find("Part1").SetActive(false);
			break;
			case "Part3":
				tutText.SetText("The health bar displays your current health. Be careful, even aliens are vulnerable!");
			GameObject.Find("Part2").SetActive(false);
				break;
			case "Part4":
				tutText.SetText("Obstacles can be also be moved with your gravity power! Switch between moving yourself \n"+
					"or the object by pressing Z or X. You need to be near the stone to be able to move it.  \n\n"
				+ "Press Enter to continue (to skip the tutorial, press V)");
			GameObject.Find("Part3").SetActive(false);
				break;
			case "Part5":
				tutText.SetText("You have collected your first heart! You will have to collect all 3 pieces before your heart is fully mended...\n"+
					"You find the collected hearts in your inventory \n\n"
				+ "Press Enter to continue (to skip the tutorial, press V)");
			GameObject.Find("Part4").SetActive(false);
				break;
			case "Finish":
				
				heartCounter = heartController.getHeartCounter();

				if (heartCounter.Equals(3))
				{	
					SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex  + 1);			 
				}
				else
				{
					tutText.SetText("You have only collected " + heartCounter + " hearts. You are missing " + (3-heartCounter) + " hearts!");

				}

			break;
		}	
	}

	void Update()
	{
		

		//Player press enter for confirmation
		if (Input.GetKeyDown(KeyCode.Return))  {			
			tutText.SetText("");
		}
		//Skip tutorial
		if(Input.GetKeyDown(KeyCode.V) && !tutText.Equals("")) {
			skipTutorial = true;
			tutText.SetText("");
		}
	}





}
