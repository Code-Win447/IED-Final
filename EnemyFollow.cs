using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;

    private void Update()
    {
        //tells the ai to move towards the player
        
        enemy.SetDestination(player.position);
    }
}

