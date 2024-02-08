using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    
    public static PlayerState Instance { get; set; }
    // ---- Player Health ---- //
    public float currentHealth;
    public float maxHealth;


    


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }


    }


    private void Start()
    {
        currentHealth = maxHealth;
        

    }

    // Update is called once per frame
    
    
    
    
    
    
    
    
    
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            print("Player Dead");
        }
        else
        {
            print("Player Hit");
        }

    }
    



   


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieHand"))
        {
            TakeDamage(other.gameObject.GetComponent<ZombieHand1>().damage);
        }
    }

}







