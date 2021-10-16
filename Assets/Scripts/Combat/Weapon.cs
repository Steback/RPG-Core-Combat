using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "RPG/New Weapon")]
    public class Weapon : ScriptableObject
    {
        enum HandType
        {
            Right = 0,
            Left = 1
        }
        
        [SerializeField] GameObject prefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] private float damage = 2.0f;
        [SerializeField] private float range = 5.0f;
        [SerializeField] private HandType handType;

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (prefab) Instantiate(prefab, handType == HandType.Right ? rightHand : leftHand);
            
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

