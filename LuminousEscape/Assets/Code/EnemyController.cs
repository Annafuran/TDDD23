using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public PlayerController player;
	public Animator anim;

	private float groundSpeed = 1.0f;
	private BoxCollider enemyBox;
	public CharacterController enemyController;
	private Vector2 moveDirection = Vector2.zero;
	private Vector2 currentMovement = Vector2.zero;
	private Vector3 lookRotation = Vector3.zero;
	private bool moveEnemy;
	private Vector3 initialPosition;
	private Vector3 standingStill;
	private float maxDist;
	private float minDist;
	private int direction;
	private float speed;
	private float timeleft;
	private float damageTimer;
	 private float gravity = 1.0f;

	//When character are near the object
	void OnTriggerEnter(Collider collider) {

		if(collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
			moveEnemy = true;
		}
	}


	void OnTriggerExit(Collider collider) {

		if(collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
			moveEnemy = false;
			direction = -1*direction;
		}
	}

	void MoveEnemy()
	{ 
		int xDir = 0;

       //If the difference in positions is approximately zero (Epsilon) do the following:
        if(Mathf.Abs (player.transform.position.x - transform.position.x) < 0.45f && Mathf.Abs (player.transform.position.y - transform.position.y) < 0.2f)
		{
			anim.Play("Attack 1");
			Health playerHealth = player.GetComponent<Health>();
			playerHealth.TakeDamage(0.01f);
		
		}
		else {

            //Check if target x position is greater than enemy's x position, if so set x direction to 1 (move right), if not set to -1 (move left).
            xDir = player.transform.position.x > transform.position.x ? 1 : -1;		
			moveDirection.y -= gravity * Time.deltaTime;
			moveDirection.x = xDir;
			
			PID(ref currentMovement, ref moveDirection);

			enemyController.Move(currentMovement * groundSpeed * Time.deltaTime);
			//Animate when moving. 	
			anim.SetFloat("Speed", Mathf.Abs(moveDirection.x));	
		}
			
	}


    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponent<CharacterController>();
		enemyBox = GetComponent<BoxCollider>();
		initialPosition = transform.position;
        direction = -1;

		//Sometimes the enemies has a negative x-pos. 
		if (initialPosition.x > 0f) {
			maxDist += transform.position.x*2;
			minDist -= transform.position.x*2;
		}else {
			maxDist -= transform.position.x*2;
			minDist += transform.position.x*2;
		}
       
		moveEnemy = false;
    }

    // Update is called once per frame
    void Update()
    {
		
		
		if (moveEnemy && player.GetComponent<Health>().getHealth() > 0.0f) {
			MoveEnemy();
		}		
		else if(!moveEnemy && player.GetComponent<Health>().getHealth() > 0.0f) {
		
			switch (direction)			
        {
             case -1: 
                // Moving Left
                if( transform.position.x > minDist)
                    {
						moveDirection.x = -0.5f;
						moveDirection.y -= gravity * Time.deltaTime;
						PID(ref currentMovement, ref moveDirection);

							enemyController.Move(currentMovement * 0.5f * Time.deltaTime);	
							
							//Animate when moving. 	
							anim.SetFloat("Speed", Mathf.Abs(moveDirection.x));
							
                    }
                else
                    {
                       direction = 1;
                    }
                break;
             case 1:
                  //Moving Right
                if(transform.position.x < maxDist)
                    {

						moveDirection.x = 0.5f;
						moveDirection.y -= gravity * Time.deltaTime;
						PID(ref currentMovement, ref moveDirection);

							enemyController.Move(currentMovement * 0.5f * Time.deltaTime);
							//Animate when moving. 	
							anim.SetFloat("Speed", Mathf.Abs(moveDirection.x));
                    }
                else
                    {
                        direction = -1;
                    }
            break;			
			 }
				timeleft += Time.deltaTime;
				if(timeleft > 5.0) {
				timeleft = 0;
				direction = -1*direction;			
			}

		}

		if (moveDirection.x != 0.00000f)
		{		
			lookRotation = new Vector3(moveDirection.x, 0.0f, 0.0f);
			transform.rotation = Quaternion.LookRotation(lookRotation);
		}
	
	
    }

	void PID(ref Vector2 realMovement, ref Vector2 desireredMovement)
    {
		realMovement += (desireredMovement - realMovement) * Mathf.Sqrt(Time.deltaTime); // speed*desireredMovement;

	}

	
}
