using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Combat
{
    public class Health : MonoBehaviour
    {
        private Animator _animator;
        private bool _isDeath = false;
        private static readonly int DieTriggerID = Animator.StringToHash("die");

        [SerializeField] public float health = 100f;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
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
            _isDeath = true;
        }

        public bool IsDeath()
        {
            return _isDeath;
        }
    }
}