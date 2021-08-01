using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;

public class Mover : MonoBehaviour  
{
    public Transform target;
    private NavMeshAgent _agent; 
    
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>(); 
    }
    
    // Update is called once per frame
    void Update()
    {
        _agent.destination = target.position; 
    }
}
