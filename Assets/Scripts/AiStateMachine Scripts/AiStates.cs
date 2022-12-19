using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiStateId
{
    AiChasePlayerState, //Chase The player Ai State.
    AiIdle,
    AiDeath,
}


public interface AiStates 
{
    AiStateId GetId(); //Gets the ID from the Ai State.

    void Enter(AiAgent agent); //
    void update (AiAgent agent); // 
    void Exit(AiAgent agent); //



}
