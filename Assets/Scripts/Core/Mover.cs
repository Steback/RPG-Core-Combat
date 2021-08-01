using UnityEngine;
using UnityEngine.AI;

namespace Core
{
    public class Mover : MonoBehaviour  
    {
        private NavMeshAgent _agent;
        private Camera _mainCamera;
    
        void Awake()
        {
            _mainCamera = Camera.main;
            _agent = GetComponent<NavMeshAgent>(); 
        }
    
        // Start is called before the first frame update
        void Start()
        {
        
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
}
