using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collectable : MonoBehaviour
{
	//private GameObject thisObject; 
		public Item myPrefab;
		private bool destroy;
		public WinController heartCounter;

	//When character are near the object
	void OnTriggerEnter(Collider collider) {

		if(collider.gameObject.layer == LayerMask.NameToLayer("Player")) {

			if(this.tag.Equals("Apple")) {
				destroy = true;
				Inventory.instance.Add(Instantiate(myPrefab));	
			}
			else if(this.tag.Equals("Heart")) {
				destroy = true;
				Inventory.instance.Add(Instantiate(myPrefab));
				heartCounter.heartCounting();

			}
		}		
	}

	//Use function?

    // Start is called before the first frame update
    void Update()
    {
		if(destroy) {
		Destroy(this.gameObject);
		}
	}

	void Start()
    {
		destroy = false;

    }
}
