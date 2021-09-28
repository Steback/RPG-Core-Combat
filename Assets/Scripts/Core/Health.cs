using Combat;
using Saving;
using UnityEngine;

namespace Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        private Animator _animator;
        private ActionScheduler _actionScheduler;
        private bool _isDeath = false;
        private static readonly int DieTriggerID = Animator.StringToHash("die");

        [SerializeField] public float health = 100f;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);

            if (health <= 0 && !_isDeath)
            {
                Die();
            }
        }

        public void Die()
        {
            if (!gameObject.CompareTag("Player")) 
                GetComponent<CombatTarget>().isValid = false;
            
            _animator.SetTrigger(DieTriggerID);
            _actionScheduler.CancelCurrentAction();
            _isDeath = true;
        }

        public bool IsDeath()
        {
            return _isDeath;
        }

        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health = (float) state;
            
            if (health <= 0 && !_isDeath)
            {
                Die();
            }
        }
    }
}