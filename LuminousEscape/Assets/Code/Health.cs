using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
	public SimpleHealthBar healthbar;
	public float maxHealth = 10f;
	float currentHealth;
	public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 10f;
		//Remove later
		healthbar.UpdateBar(currentHealth, maxHealth);
		anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void TakeDamage(float amount)
	{	
		currentHealth -= amount;
		healthbar.UpdateBar(currentHealth, maxHealth);
		
		if(currentHealth <= 0) {
			anim.SetTrigger("Death");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex  + 2);
		}	
	}

	public float getHealth() {
		return currentHealth;
	}

	public void Heal(float amount)
	{
		currentHealth += amount;

		if(currentHealth > maxHealth){
			currentHealth = maxHealth;		
		}

		healthbar.UpdateBar(currentHealth, maxHealth);

		
	}
}
