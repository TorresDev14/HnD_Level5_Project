using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //Allows for the use of Ai tools.

public class AiLocomotion : MonoBehaviour
{
    [Header("NavMeshAgent")]
    public NavMeshAgent agent; //allows for the NavMeshAgent to be allocated.


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
