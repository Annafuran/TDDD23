using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject { 

	public string itemName;
	public Sprite icon;

	//virtual this allows each Item type to overwrite this function.
	public virtual void Use()
	{
		
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
