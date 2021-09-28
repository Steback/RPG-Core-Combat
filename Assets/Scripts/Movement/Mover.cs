using Core;
using Saving;
using UnityEngine;
using UnityEngine.AI;

namespace Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        private NavMeshAgent _agent;
        private Animator _animator;
        private ActionScheduler _actionScheduler;
        private Health _health;
        private static readonly int ForwardSpeedID = Animator.StringToHash("forwardSpeed");

        void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _health = GetComponent<Health>();
        }
    
        // Update is called once per frame
        void Update()
        {
            _agent.enabled = !_health.IsDeath();
            
            UpdateAnimator();
        }

        public void Stop()
        {
            _agent.isStopped = true;
        }

        public void StartMove(Vector3 destination)
        {
            _actionScheduler.StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            _agent.destination = destination;
            _agent.isStopped = false;
        }

        private void UpdateAnimator()
        {
            _animator.SetFloat(ForwardSpeedID, transform.InverseTransformDirection(_agent.velocity).z);
        }

        public void Cancel()
        {
            Stop();
        }


        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
