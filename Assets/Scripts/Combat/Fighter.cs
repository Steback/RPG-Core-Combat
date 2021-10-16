using Core;
using Movement;
using UnityEngine;

namespace Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        private Mover _mover;
        private Health _target;
        private ActionScheduler _actionScheduler;
        private Animator _animator;
        private float _timeSinceLastAttack = Mathf.Infinity;
        private static readonly int AttackAnimationID = Animator.StringToHash("attack");
        private static readonly int StopAttackAnimationID = Animator.StringToHash("stopAttack");

        [SerializeField] public float timeBetweenAttacks = 1f;
        [SerializeField] private Transform handTransform = null;
        [SerializeField] private Weapon weapon = null;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();

            SpawnWeapon();
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            
            if (!_target || _target.IsDeath()) return;

            if (TargetIsInRange())
            {
                _mover.Stop();
                AttackBehaviour();
            }
            else
            {
                _mover.MoveTo(_target.transform.position);
            }
        }

        private void SpawnWeapon()
        {
            if (!weapon) return;
            
            weapon.Spawn(handTransform, GetComponent<Animator>());
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
            if (_target) _target.TakeDamage(weapon.GetDamage());
        }

        public bool TargetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < weapon.GetRange();
        }

        public void ResetTarget()
        {
            _target = null;
        }

        public void Attack(GameObject target)
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
