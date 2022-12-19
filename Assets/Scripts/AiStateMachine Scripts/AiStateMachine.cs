using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateMachine
{

    [Header("AiState List")]
    public AiStates[] states; //An Array which contains all the states for the Ai.


    [Header("AiAgent")]
    public AiAgent agent; //Allows for the assigning of the Agent.

    [Header("Current State")]
    AiStateId CurrentState; //Contains the variable for the current state.

    public AiStateMachine(AiAgent agent)
    {
        this.agent = agent; //Stores the Agent
        int numStates = System.Enum.GetNames(typeof(AiStateId)).Length; //Allocates A number to each Ai State.
        states = new AiStates[numStates]; //Allocates a new Array of states which are numbered.

    }

    public void RegisterAiState(AiStates state) //Stores pre allocated states into an array. They won't be allocated during run time.
    {
        int index = (int)state.GetId(); //Casts the ID into an Interger.
        states[index] = state; //Inserts said ID into the array.
    }

    public AiStates GetStates(AiStateId stateId) //Returns a state from a state Id.
    {
        int index = (int)stateId; //Casts the state Id to an interger from the Index.
        return states[index];
    }

    public void Update()
    {
        GetStates(CurrentState)?.update(agent); //gets the current state and updates the agent.
    }

    public void ChangeState(AiStateId newState) 
    {
        GetStates(CurrentState)?.Exit(agent); //Exits current state.
        CurrentState = newState; //Sets the current state to the new state.
        GetStates(CurrentState)?.Enter(agent); //Enters the new state.

    }



}
