using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiChasePlayerState : AiStates
{

    [Header("floats")]
    public float timer = 0.0f;

    [Header("NavMeshAgent")]
    public NavMeshAgent NavMeshagent;
    public AiStateId GetId()
    {
        return AiStateId.AiChasePlayerState;
    }

    public void Enter(AiAgent agent)
    {

    }

    public void Exit(AiAgent agent)
    {

    }


    public void update(AiAgent agent)
    {
        if (!agent.enabled)
        {
            return;
        }

        timer -= Time.deltaTime;
        if (!agent.NavMeshagent.hasPath)
        {
            agent.NavMeshagent.destination = agent.PlayerTransform.position;
            agent.NavMeshagent.destination = agent.PlayerTransform.position;
            agent.NavMeshagent.destination = agent.PlayerTransform.position;

        }

        if (timer < 0.0f)
        {
            Vector3 direction = (agent.PlayerTransform.position - agent.NavMeshagent.destination);
            direction.y = 0;
            if (direction.sqrMagnitude > agent.Config.MinDistance * agent.Config.MinDistance)
            {
                if (agent.NavMeshagent.pathStatus == NavMeshPathStatus.PathPartial)
                {
                    agent.NavMeshagent.destination = agent.PlayerTransform.position;
                }
            }

            timer = agent.Config.MaxTime;
        }
    }
}
