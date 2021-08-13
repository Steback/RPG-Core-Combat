using System;
using Core;
using Movement;
using UnityEngine;
using UnityEngine.Serialization;

namespace Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        private Mover _mover;
        private Transform _target;
        private ActionScheduler _actionScheduler;
        private Animator _animator;
        private float _timeSinceLastAttack = 0;
        private static readonly int AttackAnimationID = Animator.StringToHash("attack");

        [SerializeField] public float weaponRange = 2.0f;
        [SerializeField] public float timeBetweenAttacks = 1f;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            
            if (!_target) return;

            if (TargetIsInRange())
            {
                _mover.MoveTo(_target.position);
            }
            else
            {
                _mover.Stop();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (_timeSinceLastAttack > timeBetweenAttacks)
            {
                _animator.SetTrigger(AttackAnimationID);
                _timeSinceLastAttack = 0;
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

        // Animation Event
        void Hit()
        {
            
        }
    }
}
