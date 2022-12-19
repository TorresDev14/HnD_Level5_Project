using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //Allows for the Use of Ai tools.

public class DebugNavMeshAgent : MonoBehaviour
{
    [Header("NavMeshAgent")]
     public NavMeshAgent agent; //Allows for the allocation of the NavMeshAgent.

    [Header("Bools")]
     public bool Velocity;
     public bool DesiredVelocity;
     public bool Path;

    // Start is called before the first frame update
    void Start()
    {
        agent.GetComponent<NavMeshAgent>(); //Allows for the Agent to use the NavMesh Agent.
        
    }

    // Update is called once per frame
    void OnDrawGizmos()
    {
        if (Velocity)
        {
            Gizmos.color = Color.green; //Shows the veloicty in Green.
            Gizmos.DrawLine(transform.position, transform.position + agent.velocity); 
        }

        if (DesiredVelocity)
        {
            Gizmos.color = Color.yellow; //Shows the Desiredveloicty in Yellow.
            Gizmos.DrawLine(transform.position, transform.position + agent.desiredVelocity);
        }


        if (Path)
        {
            Gizmos.color = Color.red; //Sets the path colour red.
            var agentPath = agent.path; //gets the path from the agent.

            Vector3 prevCorner = transform.position; //Gets the previous corner from the agents path.

            foreach (var corner in agentPath.corners) 
            {
                Gizmos.DrawLine(prevCorner, corner); //Draws a line fro the previous corner.
                Gizmos.DrawSphere(corner, 0.1f); //Draws the sphere with a radius of 0.1.
                prevCorner = corner; //Sets the prvious corner to the last corner that the Ai passed.

            }
        }
    }
}
