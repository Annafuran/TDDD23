using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "new Consumable", menuName = "Items/Consumable")]
//The class inherits from Item
public class Consumable : Item
{
	public int heal = 0;

 
	public override void Use() {

		GameObject player = Inventory.instance.player;
		Health playerHealth = player.GetComponent<Health>();

		playerHealth.Heal(heal);
		Inventory.instance.Remove(this);
				
	
	}  
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
