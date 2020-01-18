using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour
{
	public Item item;

	public void updateInfo()
	{
		Text displayText = transform.Find("Text").GetComponent<Text>();
		Image displayImage = transform.Find("Image").GetComponentInChildren<Image>();
		
		
		if(item)
		{
			displayText.text = item.itemName;
			displayImage.sprite = item.icon;
		}
		else {
	
			displayText.text = "";
			displayImage.sprite = null;
			displayImage.color = Color.clear;
		}

	}

	public void Use() {

		
		if(item.itemName.Equals("Heart")) {
		}
		else if(item && !item.itemName.Equals("Heart"))
		{
			item.Use();
			
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        updateInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}