using System;
using Combat;
using Core;
using Movement;
using UnityEngine;
using UnityEngine.Serialization;

namespace Control
{
    public class AIController : MonoBehaviour
    {
        private GameObject _player;
        private Fighter _fighter;
        private Health _health;
        private Mover _mover;
        private Vector3 _guardPosition;
        private int _currentWaypointIndex = 0;
        private float _timeSinceLastSawPlayer = Mathf.Infinity;
        private float _timeSinceArrivedToWaypoint = Mathf.Infinity;
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionTime = 3f;
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float waypointTolerance = 1.0f;
        [SerializeField] private float waypointDwellTime = 3.0f;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player");
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
            _mover = GetComponent<Mover>();
            _guardPosition = transform.position;
        }

        private void Update()
        {
            if (_health.IsDeath()) return;
            
            UpdateTimes();

            if (InAttackRange() && !_player.GetComponent<Health>().IsDeath())
            {
                AttackBehavior();
            }
            else if (_timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                PatrolBehavior();
            }
        }

        private bool InAttackRange()
        {
            return DistancePlayer() < chaseDistance;
        }

        private float DistancePlayer()
        {
            return Vector3.Distance(transform.position, _player.transform.position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        private void AttackBehavior()
        {
            _timeSinceLastSawPlayer = 0;
            _fighter.Attack(_player);
        }

        private void SuspicionBehavior()
        { 
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void PatrolBehavior()
        {
            Vector3 nextPosition = _guardPosition;

            if (patrolPath)
            {
                
                if (AtWaypoint())
                {
                    _timeSinceArrivedToWaypoint = 0;
                    CycleWaypoint();
                }

                nextPosition = GetCurrentWaypoint();
            }
            
            if (_timeSinceArrivedToWaypoint > waypointDwellTime)
            {
                _mover.MoveTo(nextPosition);
            }
        }

        private bool AtWaypoint()
        {
            return Vector3.Distance(transform.position, GetCurrentWaypoint()) < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            _currentWaypointIndex = patrolPath.GetNextIndex(_currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(_currentWaypointIndex);
        }

        private void UpdateTimes()
        {
            _timeSinceLastSawPlayer += Time.deltaTime;
            _timeSinceArrivedToWaypoint += Time.deltaTime;
        }
    }
}
