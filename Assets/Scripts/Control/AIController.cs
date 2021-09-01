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
        [SerializeField] private float chaseDistance = 5f;

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
                _fighter.Attack(_player);
            }
            else
            {
                _fighter.Cancel();
                _mover.MoveTo(_guardPosition);
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
    }
}
