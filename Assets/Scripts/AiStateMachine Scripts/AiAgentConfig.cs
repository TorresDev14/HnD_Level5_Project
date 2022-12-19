using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    [Header("floats")]
    public float MaxTime = 0.1f;
    public float MinDistance = 1.0f;
    public float MaxSightDistance = 20.0f;

}
