using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiNew : MonoBehaviour
{
    [Header("NavMesh")] //Contains the NavMesh for the Enemies.

    [SerializeField] public NavMeshAgent Agent; //creates a Navmesh for the enemies to determine where they can move.

    [Header("Transformations")] //Contains all the Transformations.

    [SerializeField] public Transform FirstPersonPlayer;

    [Header("LayerMasks")] //Contains all Layermasks.

    [SerializeField] public LayerMask whatisGround; //Determines what the Ai thinks is the ground.
    [SerializeField] public LayerMask WhatisPlayer; //Determines what the Ai thinks is the player.

    [Header("Patroling")] //Contains all of the Patrolling Variables used by the Ai.

    [SerializeField] public Vector3 WalkPoint; //Creates a Vector 3 which can determin walk points for the Ai.
    [SerializeField] bool WalkPointSet; //Creates a Bool to determin if the Walkpoint can be set.
    [SerializeField] public float WalkPointRange; //Creates a float which determins if the Walkpoint is in range.

    [Header("Attacking")] //Contains all of the Attacking Variables used by the Ai.

    [SerializeField] public float TimeBetweenAttacks; //Calculates the time between the enemy attacks.
    [SerializeField] bool AlreadyAttacked; //Determines if the Ai has attacked or not.
    [SerializeField] public GameObject Projectile; //Creates a Projectile that will shoot at players.

    [Header("States")] //Contains all the Variabels for the different states.

    [SerializeField] public float SightRange; //Determines the Range of sight for the Ai.
    [SerializeField] public float AttackRange; //Determines the Attack Range for the Ai.
    [SerializeField] public bool PlayerInSightRange; //Determins if the player is in the Ai's Range or not.
    [SerializeField] public bool PlayerInAttackingRange; //Determines if the player is in attacking Range.

    private void Awake()
    {
        FirstPersonPlayer = GameObject.Find("FirstPersonPlayer").transform; //Finds the Player.
        Agent = GetComponent<NavMeshAgent>(); //Assigns the Navmesh to the Ai.                                                                   
    }


    private void Update()
    {
        PlayerInSightRange = Physics.CheckSphere(transform.position, SightRange, WhatisPlayer); //Checks to see if the player is in Sight Range and Attack Range.
        PlayerInAttackingRange = Physics.CheckSphere(transform.position, AttackRange, WhatisPlayer); //Checks to see if the player is in Attacking Range.

        if (!PlayerInSightRange && !PlayerInAttackingRange) Patrolling(); //The Ai should be in the patrolling state if the player isn't in attacking or sight range.
        if (PlayerInSightRange && !PlayerInAttackingRange) ChasePlayer(); //The Ai should chase the player if they are in Sight range but not attacking range.
        if (PlayerInSightRange && PlayerInAttackingRange) AttackPlayer(); //The Ai should attack the player if they are in both the attacking range and Sight range.

    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-WalkPointRange, WalkPointRange); //Creates a new Walking point on the Z Axis.
        float randomX = Random.Range(-WalkPointRange, WalkPointRange); //Creates a new walking point on the X Axis.

        WalkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); //Searches for a new Walking Point for the Ai to move around the Map.     

        if (Physics.Raycast(WalkPoint, -transform.up, 2f, whatisGround)) //Stops the Ai from walking off the edge of the map.
            WalkPointSet = true;
    }

    private void ResetAttack()
    {
        AlreadyAttacked = false;
    }

    //State Functions

    private void Patrolling()
    {
        if (!WalkPointSet) SearchWalkPoint(); //Searches for a walking point.

        if (WalkPointSet)
            Agent.SetDestination(WalkPoint); //Sets A walk point for the Ai.

        Vector3 DistanceToWalkPoint = transform.position - WalkPoint; //Checks the distnace from the Ai to the Walkpoint.

        if (DistanceToWalkPoint.magnitude < 10f) //If the Ai is at the walk point it won't walk further until it gets a new walk point.
            WalkPointSet = false;
    }

    private void ChasePlayer()
    {
        Agent.SetDestination(FirstPersonPlayer.position); //Chases the player.
    }

    private void AttackPlayer()
    {
        Agent.SetDestination(transform.position); //Stops the Ai from moving.

        transform.LookAt(FirstPersonPlayer); //Makes the Ai look at the player.

       // if (!AlreadyAttacked)
        //{
        //    AlreadyAttacked = true;
       //     Invoke(nameof(ResetAttack), TimeBetweenAttacks); //Resets the Ai's Attacks if they have attacked.
       // }

        //Attacking code can go here, for now I will just get the Ai to shoot out a projectile at the player.
       // Rigidbody rb = Instantiate(Projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>(); //Shoots a Projectile at the player.

       // rb.AddForce(transform.forward * 1f, ForceMode.Impulse); //adds force to the projectile so it can fly towards the player.
       // rb.AddForce(transform.up * 1f, ForceMode.Impulse); //adds force to the projectile so it can fly towards the player.
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SightRange);
    }

}
