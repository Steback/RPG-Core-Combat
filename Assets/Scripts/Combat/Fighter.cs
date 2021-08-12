using System;
using Core;
using Movement;
using UnityEngine;

namespace Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        private Mover _mover;
        private Transform _target;
        private ActionScheduler _actionScheduler;

        [SerializeField] private float weaponRange = 2.0f;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            if (!_target) return;

            if (TargetIsInRange())
            {
                _mover.MoveTo(_target.position);
            }
            else
            {
                ResetTarget();
                _mover.Stop();
            }
        }

        public bool TargetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.position) > weaponRange;
        }

        public void ResetTarget()
        {
            _target = null;
        }

        public void Attack(CombatTarget target)
        {
            _actionScheduler.StartAction(this);
            _target = target.transform;
        }

        public void Cancel()
        {
            ResetTarget();
        }
    }
}
