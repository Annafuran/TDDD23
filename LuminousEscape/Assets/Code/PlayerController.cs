using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
	//VARIABLES   
	private Vector2 moveDirection = Vector2.zero;
	private Vector2 currentMovement = Vector2.zero;
	private Vector2 lookRotation = Vector2.zero;
	public Vector2 boxPosition;
	public Animator anim;
	public CharacterController controller;
	public Rigidbody gravityBoxController;
	public Rigidbody charController;
	public SimpleHealthBar gravityBar;  
	public Vector2 moveDirectionBox = Vector2.zero;
	public Health health;
	public AudioSource audioData;
	public GameObject quitPanel;	

	//MOVEMENT CONSTANTS 	
    private float gravity = 3.0f;
	private float jumpStaminaCost = 0.004f;
	private float staminaRecovery = 0.04f;
	private float gravitySpeed = 0.3f;  
	private float currSpeed;
	private float groundSpeed = 1.0f;
	private bool gravityTest;
	private bool moveBox;
	private GameObject ip;
	private bool collisiontest;
	private ObjectGravityController stone;
	private int heartCounter;

	//Static
	public static float stamina = 1.0f;
	


    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Character Controller component so that we can access it.
        controller = GetComponent<CharacterController> ();
		health = GetComponent<Health>();
		charController = GetComponent<Rigidbody>();

		

		//Using groundspeed as current. 
		currSpeed = groundSpeed;

		//Update gravity color and set max value for gravity controller. 		
		gravityBar.UpdateBar(stamina, stamina);
		//Start music
		audioData = GetComponent<AudioSource>();
        audioData.Play(0);

		ip = Inventory.instance.inventoryPanel;
		ip.SetActive(false);
	
		
    }
	
    void FixedUpdate()
    {
		 //Escape
		if(Input.GetKey(KeyCode.Escape)) {			
			quitPanel.SetActive(true);
		}
		//Inventory
		Menu();
		//Stamina update
		stamina = Mathf.Clamp01(stamina + staminaRecovery * Time.deltaTime);
		//Access and change position of boxes (The stones).
		stone = FindClosestStone(); 
		gravityBoxController = stone.gravityBoxcontroller;
		gravityTest = stone.triggered;
		collisiontest = stone.collisionTest;
		boxPosition = new Vector2(gravityBoxController.position.x, gravityBoxController.position.y);

		//To check if we are moving. 
		if (moveDirection.x != 0.00000f)
		{		
			lookRotation = new Vector3(moveDirection.x, 0.0f, 0.0f);
			transform.rotation = Quaternion.LookRotation(lookRotation);
		}

		//Use gravity controller	
		if(Input.GetKey(KeyCode.Space)) {

			GravityControll();					
		}

		//If not using gravity controller
		else {		
			
			GroundMovement();
		}

		if(!moveBox) {		

			//Apply Smooth movement then move. 
			PID(ref currentMovement, ref moveDirection);
			controller.Move(currentMovement * currSpeed * Time.deltaTime);
		
			
		}		
		
			//Animate when moving. 	
			anim.SetFloat("Speed", Mathf.Abs(moveDirection.x));	
    }


	void GroundMovement() {
			
			currSpeed = groundSpeed;
			//We should no longer move the box. 
			moveBox = false;
					
			//Falling down but still be able to move Horizontal. 			
			moveDirection.x = Input.GetAxis("Horizontal");
			if(moveDirection.y > -3.0f)
			moveDirection.y -= gravity * Time.deltaTime;	
					
			if(!collisiontest)
			{
				moveDirectionBox.y += 0.01f;
				Vector3 tempVect = new Vector3(0, -moveDirectionBox.y, 0);
				tempVect = tempVect.normalized * groundSpeed * Time.deltaTime;
				gravityBoxController.MovePosition(gravityBoxController.position + tempVect);		
			}	
	}
	
	public ObjectGravityController FindClosestStone()
    {
        ObjectGravityController[] gos;
		gos = FindObjectsOfType<ObjectGravityController>();
        ObjectGravityController closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (ObjectGravityController go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
       return closest;
    }

	//Gravity. Both for character and boxes. 
	void GravityControll()
	{
		//Check if we are near a movable box. 
		gravityTest = stone.triggered;

		stamina -= jumpStaminaCost;	
		//Move box
		if (gravityTest && stamina > 0.1f)
		{
			moveBox = true;		
			moveDirectionBox.y = Input.GetAxisRaw("Vertical");
			moveDirectionBox.x = Input.GetAxisRaw("Horizontal");
				
			Vector3 tempVect = new Vector3(moveDirectionBox.x, moveDirectionBox.y, 0);
			tempVect = tempVect.normalized * gravitySpeed * Time.deltaTime;
			gravityBoxController.MovePosition(gravityBoxController.position + tempVect);		
		}
		
		//Move character. 
		else if(!gravityTest && stamina > 0.1f) {

			moveDirection.y = Input.GetAxisRaw("Vertical");
			moveDirection.x = Input.GetAxisRaw("Horizontal");

			Vector3 tempVect = new Vector3(moveDirection.x, moveDirection.y, 0);
			tempVect = tempVect.normalized * gravitySpeed * Time.deltaTime;
			charController.MovePosition(charController.position + tempVect);
		}
			else{ 
				GroundMovement();
				}	
							
		
	}


	void Menu() {
			
			if(Input.GetKeyDown(KeyCode.Tab))
			{
			if(!ip.activeSelf) {
				ip.SetActive(true);
			}
			else {
				ip.SetActive(false);
			}
		}

	}
	
	void LateUpdate() {
		UpdateUI();
	}

	void UpdateUI() {
		gravityBar.UpdateBar(stamina, 1.0f);		
	}

	//For smooth movement
	void PID(ref Vector2 realMovement, ref Vector2 desireredMovement)
    {
		realMovement += (desireredMovement - realMovement) * Mathf.Sqrt(Time.deltaTime); // speed*desireredMovement;

	}
		
   }

