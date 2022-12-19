using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    [Header("States Machine")]
    public AiStateMachine StateMachine; //Allows for the allocation of the state machine Script.

    [Header("Initial State")]
    public AiStateId InitialState; //This state is what the state machine should be initially.

    [Header("NavMeshAgent")]
    public NavMeshAgent NavMeshagent;

    [Header("Config")]
    public AiAgentConfig Config;

    [Header("States")]
    public AiStateId AiChasePlayerState;
    public AiStateId AiDeath;
    public AiStateId AiIdle;

    [Header("Transformations")]
    public Transform PlayerTransform; //Allows for the allocation of the players transformration.


    // Start is called before the first frame update
    void Start()
    {
        NavMeshagent = GetComponent<NavMeshAgent>();
        StateMachine = new AiStateMachine(this);
        StateMachine.RegisterAiState(new AiChasePlayerState());
        StateMachine.RegisterAiState(new AiDeathState());
        StateMachine.RegisterAiState(new AiIdleState());
        StateMachine.ChangeState(InitialState); //Changes the state machines state to the initial state.

        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
       StateMachine.Update(); //Updates the state machine.
    }
}
