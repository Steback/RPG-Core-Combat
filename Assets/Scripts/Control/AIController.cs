using System;
using UnityEngine;

namespace Control
{
    public class AIController : MonoBehaviour
    {
        private GameObject _player;
        [SerializeField] private float chaseDistance = 5f;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (DistancePlayer() < chaseDistance)
            {
                print(gameObject.name);
            }
        }

        private float DistancePlayer()
        {
            return Vector3.Distance(transform.position, _player.transform.position);
        }
    }
}
