using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class ObjectGravityController : MonoBehaviour
{

	//PRIVATE
	public CapsuleCollider m_ObjectCollider;
	public BoxCollider m_ObjectBoxCollider;
	private Vector2 moveDirection = Vector2.zero;
	private Vector2 myPosition = Vector2.zero;
	private bool gravityChoice;
	private bool test;
	private Vector2 moveDirectionBox = Vector2.zero;

	

	//PUBLIC
	public bool triggered;
	public static float stamina_gravity;
	public Rigidbody gravityBoxcontroller;
	public bool collisionTest;
	public static GameObject sparkleObject;
	public TextMeshProUGUI gravityText;
	public TextMeshProUGUI playerText;
	public TextMeshProUGUI buttonTextObject;
	public TextMeshProUGUI buttonTextPlayer;
	

	void Start() {

		
		triggered = false;
		m_ObjectCollider = GetComponent<CapsuleCollider>();
		m_ObjectBoxCollider = GetComponent<BoxCollider>();
		stamina_gravity = PlayerController.stamina;

		gravityBoxcontroller = GetComponent<Rigidbody>();	
		sparkleObject = GameObject.Find("SparkStone");
		gravityText.color = new Color32(255, 255, 255, 0);
		buttonTextObject.color = new Color32(255, 255, 255, 0);
		buttonTextPlayer.color = new Color32(255, 255, 255, 0);
		gravityChoice = false;
		collisionTest = true;
	}

	//When character are near the object
	void OnTriggerEnter(Collider collider) {

		if(collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
			gravityChoice = true;
			gravityText.color = new Color32(255, 255, 255, 112);
			buttonTextObject.color = new Color32(255, 255, 255, 112);
			buttonTextPlayer.color = new Color32(255, 255, 255, 255);
						
		}
		if(collider.gameObject.tag.Equals("Ground") && m_ObjectBoxCollider.bounds.Intersects(collider.bounds)){
			collisionTest = true;
		}
	}

	//When character leaves object
	void OnTriggerExit(Collider collider) {
		if (collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
			triggered = false;
			gravityChoice = false;
			gravityText.color = new Color32(255, 255, 255, 0);
			playerText.color = new Color32(255, 255, 255, 255);
			buttonTextObject.color = new Color32(255, 255, 255, 0);
			buttonTextPlayer.color = new Color32(255, 255, 255, 0);

		}
		if(collider.gameObject.tag.Equals("Ground")) {	
			collisionTest = false;
		}
	}

	void Update() {

		//Controll Player
		if(Input.GetKey(KeyCode.X) && gravityChoice) {
			gravityText.color = new Color32(255, 255, 255, 255);
			playerText.color = new Color32(255, 255, 255, 112);
			buttonTextObject.color = new Color32(255, 255, 255, 255);
			buttonTextPlayer.color = new Color32(255, 255, 255, 112);
			triggered = true;
		}
		//Controll Object
		else if(Input.GetKey(KeyCode.Z) && gravityChoice) {
			gravityText.color = new Color32(255, 255, 255, 112);
			playerText.color = new Color32(255, 255, 255, 255);
			buttonTextObject.color = new Color32(255, 255, 255, 112);
			buttonTextPlayer.color = new Color32(255, 255, 255, 255);
			triggered = false;
		}

	}
}