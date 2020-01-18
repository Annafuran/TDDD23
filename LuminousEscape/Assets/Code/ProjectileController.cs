using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ADD THIS SCRIPT TO THE ENEMY WEAPON

public class ProjectileController : MonoBehaviour
{
	public float damage = 1f;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter(Collision collision)
    {
		GameObject other = collision.gameObject;
		Health otherHealth = other.GetComponent<Health>();

		if(otherHealth) {
			otherHealth.TakeDamage(damage);
		}
        Destroy(gameObject);
    }
        
    }

