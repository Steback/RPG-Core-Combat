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
        private ActionScheduler _actionScheduler;
        private Animator _animator;
        private float _timeSinceLastAttack = 0;
        private static readonly int AttackAnimationID = Animator.StringToHash("attack");
        private static readonly int StopAttackAnimationID = Animator.StringToHash("stopAttack");

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
            
            if (!_target || _target.IsDeath()) return;

            if (TargetIsInRange())
            {
                _mover.MoveTo(_target.transform.position);
            }
            else
            {
                _mover.Stop();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            _mover.transform.LookAt(_target.transform);
            if (_timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                _timeSinceLastAttack = 0;
            }
        }

        // Animation Event
        void Hit()
        {
            if (_target) _target.TakeDamage(weaponDamage);
        }

        public bool TargetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) > weaponRange;
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
        }

        public void Cancel()
        {
            StopAttack();
            ResetTarget();
        }

        public void TriggerAttack()
        {
            _animator.ResetTrigger(StopAttackAnimationID);
            // This will trigger the Hit() event
            _animator.SetTrigger(AttackAnimationID);
        }

        public void StopAttack()
        {
            _animator.ResetTrigger(AttackAnimationID);
            _animator.SetTrigger(StopAttackAnimationID);
        }
    }
}
