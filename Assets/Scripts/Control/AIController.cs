using System;
using Combat;
using Core;
using Movement;
using UnityEngine;

namespace Control
{
    public class AIController : MonoBehaviour
    {
        private GameObject _player;
        private Fighter _fighter;
        private Health _health;
        private Mover _mover;
        private Vector3 _guardPosition;
        private float _timeSinceLastSawPlayer = Mathf.Infinity;
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionTime = 3f; 

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

            if (InAttackRange() && !_player.GetComponent<Health>().IsDeath())
            {
                _timeSinceLastSawPlayer = 0;
                AttackBehavior();
            }
            else if (_timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                GuardBehavior();
            }

            _timeSinceLastSawPlayer += Time.deltaTime;
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
            _fighter.Attack(_player);
        }

        private void SuspicionBehavior()
        { 
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void GuardBehavior()
        {
            _mover.MoveTo(_guardPosition);
        }
    }
}
