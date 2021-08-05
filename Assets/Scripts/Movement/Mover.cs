using UnityEngine;
using UnityEngine.AI;

namespace Movement
{
    public class Mover : MonoBehaviour  
    {
        private NavMeshAgent _agent;
        private Animator _animator;
        private static readonly int ForwardSpeedID = Animator.StringToHash("forwardSpeed");

        void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }
    
        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination)
        {
            _agent.destination = destination;
        }

        private void UpdateAnimator()
        {
            _animator.SetFloat(ForwardSpeedID, transform.InverseTransformDirection(_agent.velocity).z);
        }
    }
}
