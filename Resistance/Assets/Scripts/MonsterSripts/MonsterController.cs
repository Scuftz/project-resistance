﻿using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public Card stats;
    public Transform target; //target will always be nexus
    public LayerMask whatIsPlayer, whatIsStructure;
    NavMeshAgent agent;

    public Animator anim;

    //attack
    public float timeBetweenAttack;
    bool hasAttacked;

    //states
    public float sightRange = 10f;
    public float attackRange = 10f;

    private bool isInSightRange, isInAttackRange; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //true if any of these are met
        isInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsStructure) |
                         Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        isInAttackRange = Physics.CheckSphere(transform.position, sightRange, whatIsStructure) |
                          Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        
        if (!isInSightRange)
        {
            WalkForward();
        }
    }

    private void WalkForward()
    {
        Vector3 movement = transform.right * Time.deltaTime * agent.speed;
        anim.Play("walk");
        agent.Move(movement);
        agent.SetDestination(target.position);
        agent.updateRotation = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
