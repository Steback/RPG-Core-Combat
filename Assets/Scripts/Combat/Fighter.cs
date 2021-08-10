using System;
using Movement;
using UnityEngine;

namespace Combat
{
    public class Fighter : MonoBehaviour
    {
        private Mover _mover;
        private Transform _target;

        [SerializeField] private float weaponRange = 2.0f;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (_target)
            {
                if (Vector3.Distance(transform.position, _target.position) > weaponRange)
                {
                    _mover.MoveTo(_target.position);
                }
                else
                {
                    _target = null;
                    _mover.Stop();
                }
            }
        }

        public void Attack(CombatTarget target)
        {
            _target = target.transform;
        }
    }
}
