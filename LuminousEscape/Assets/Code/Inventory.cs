using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

	public GameObject player;
	public GameObject inventoryPanel;
	public static Inventory instance;
	public List<Item> list = new List<Item>();

	
	void updatePanelSlots() {

		int index = 0;

		//Each child of the panel corresponds with one of the slots
		foreach (var slot in inventoryPanel.GetComponentsInChildren<InventorySlotController>())
		{
			if(index < list.Count)
			{
				slot.item = list[index];
				
			}
			else
			{
				slot.item = null;
			}
			
			slot.updateInfo();
			index++;
			
		}
		
	}

	public void Add(Item item) {

		if(list.Count < 6) {
			list.Add(item);			
		}

		updatePanelSlots();
	}

	public void Remove(Item item) {
		
		
		list.Remove(item);
		updatePanelSlots();
	}


    // Start is called before the first frame update
    void Start()
    {
		//Now any script can write Inventory.instance and refer to the one and only inventory
		instance = this;
		//Update slots
		updatePanelSlots();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
