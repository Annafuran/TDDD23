using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitPanel : MonoBehaviour
{
	public GameObject quitPanel;
    // Start is called before the first frame update
    void Start()
    {
        quitPanel.SetActive(false);
    }

	public void quitGame() {
		Application.Quit();
	}

	public void contGame() {
		quitPanel.SetActive(false);
	}

}
