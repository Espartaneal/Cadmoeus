using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    private Animator animator;

    private NavMeshAgent navAgent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if(HP <= 0)
        {
            int randomValue = Random.Range(0, 2); // 0 or 1

            if(randomValue == 0)
            {
                animator.SetTrigger("DIE1");
            }
            else
            {
                animator.SetTrigger("DIE2");
            }


            animator.SetTrigger("DIE");
            
        }
        else
        {
            animator.SetTrigger("DAMAGE");
        }
       
     
    }

    private void Update()
    {
        // Make the zombie take damage
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }
        
        
    }

   private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f); // Attacking // Stop Attacking


        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 18f); // Detection (Start Chasing)

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 21f); // Stop Chasing
    }




}
