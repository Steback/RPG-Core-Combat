using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;

public class Mover : MonoBehaviour  
{
    private NavMeshAgent _agent;
    private Camera _mainCamera;
    
    void Awake()
    {
        _mainCamera = Camera.main;
        _agent = GetComponent<NavMeshAgent>(); 
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }
    }

    private void MoveToCursor()
    {
        RaycastHit hit;

        if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
        {
            _agent.destination = hit.point;
        }
    }
}
