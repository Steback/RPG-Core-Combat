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
        private Health _target;
        private Transform _targetTransform;
        private ActionScheduler _actionScheduler;
        private Animator _animator;
        private float _timeSinceLastAttack = 0;
        private static readonly int AttackAnimationID = Animator.StringToHash("attack");
        private static readonly int StopAttack = Animator.StringToHash("stopAttack");

        [SerializeField] public float weaponRange = 2.0f;
        [SerializeField] public float timeBetweenAttacks = 1f;
        [SerializeField] public float weaponDamage = 5.0f;

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
            
            if (_target.IsDeath()) return;

            if (TargetIsInRange())
            {
                _mover.MoveTo(_targetTransform.position);
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
                // This will trigger the Hit() event
                _animator.SetTrigger(AttackAnimationID);
                _timeSinceLastAttack = 0;
            }
        }

        // Animation Event
        void Hit()
        {
            if (_target)
            {
                _target.TakeDamage(weaponDamage);
            }
        }

        public bool TargetIsInRange()
        {
            return Vector3.Distance(transform.position, _targetTransform.position) > weaponRange;
        }

        public void ResetTarget()
        {
            _target = null;
        }

        public void Attack(CombatTarget target)
        {
            _target = target.GetComponent<Health>();
            
            if (_target.IsDeath()) return;
           
            _actionScheduler.StartAction(this);
            _targetTransform = target.transform;
        }

        public void Cancel()
        {
            _animator.SetTrigger(StopAttack);
            ResetTarget();
        }
    }
}
