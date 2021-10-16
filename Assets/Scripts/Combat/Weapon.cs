using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "RPG/New Weapon")]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject prefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] public float damage = 2.0f;
        [SerializeField] public float range = 5.0f;

        public void Spawn(Transform hand, Animator animator)
        {
            if (prefab) Instantiate(prefab, hand);
            
            if (animatorOverride) animator.runtimeAnimatorController = animatorOverride;
        }

        public float GetDamage()
        {
            return damage;
        }

        public float GetRange()
        {
            return range;
        }
    }
}

