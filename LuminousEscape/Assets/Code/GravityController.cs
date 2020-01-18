using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
	public GameObject sparkObject;
	public GameObject sparkPlayer;
	public CharacterController player;

	private bool gravityTest;
	private ObjectGravityController stone;
	public PlayerController getstone;

    // Start is called before the first frame update
    void Start()
    {

		
    }

    // Update is called once per frame
    void Update()
    {
		stone = getstone.FindClosestStone();
		sparkObject = stone.transform.GetChild(1).gameObject;
		gravityTest = stone.triggered;

		if (gravityTest) {
			sparkObject.SetActive(true);
			sparkPlayer.SetActive(false);
		}
		else if(!gravityTest) {
			sparkObject.SetActive(false);
			sparkPlayer.SetActive(true);
		}
    }
}
