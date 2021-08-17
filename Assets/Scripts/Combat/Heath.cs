using System;
using UnityEngine;

namespace Combat
{
    public class Heath : MonoBehaviour
    {
        [SerializeField] public float health = 100f;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
        }
    }
}