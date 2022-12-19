using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdleState : AiStates
{
    public void Enter(AiAgent agent)
    {
       
    }

    public void Exit(AiAgent agent)
    {
       
    }

    public AiStateId GetId()
    {
        return AiStateId.AiIdle;
    }

    public void update(AiAgent agent)
    {
        Vector3 PlayerDirection = agent.PlayerTransform.position - agent.transform.position;

        if (PlayerDirection.magnitude > agent.Config.MaxSightDistance) 
        {
            return;
        }

        Vector3 agentDirection = agent.transform.forward;

        PlayerDirection.Normalize();

        float DotProduct = Vector3.Dot(PlayerDirection, agentDirection);

        if (DotProduct > 0.0f) 
        {
            agent.StateMachine.ChangeState(AiStateId.AiChasePlayerState);
        }

    }
}
