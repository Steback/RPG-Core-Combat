using System;
using Combat;
using UnityEngine;

namespace Control
{
    public class AIController : MonoBehaviour
    {
        private GameObject _player;
        private Fighter _fighter;
        [SerializeField] private float chaseDistance = 5f;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player");
            _fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
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
