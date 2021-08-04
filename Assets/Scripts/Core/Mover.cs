using UnityEngine;
using UnityEngine.AI;

namespace Core
{
    public class Mover : MonoBehaviour  
    {
        private NavMeshAgent _agent;
        private Camera _mainCamera;
        private Animator _animator;
        private static readonly int ForwardSpeedID = Animator.StringToHash("forwardSpeed");

        void Awake()
        {
            _mainCamera = Camera.main;
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }
    
        // Start is called before the first frame update
        void Start()
        {
        
        }
    
        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }

            UpdateAnimator();
        }

        private void MoveToCursor()
        {
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out var hit))
            {
                _agent.destination = hit.point;
            }
        }

        private void UpdateAnimator()
        {
            _animator.SetFloat(ForwardSpeedID, transform.InverseTransformDirection(_agent.velocity).z);
        }
    }
}
