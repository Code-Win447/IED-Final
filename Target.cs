using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    public void takeDamage(float amount)
    {
        //allows the target to die
        
        health -= amount;
        if(health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);

    }
}
