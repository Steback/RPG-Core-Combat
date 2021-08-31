using System;
using Combat;
using Core;
using UnityEngine;

namespace Control
{
    public class AIController : MonoBehaviour
    {
        private GameObject _player;
        private Fighter _fighter;
        private Health _health;
        [SerializeField] private float chaseDistance = 5f;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player");
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
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
    }
}
