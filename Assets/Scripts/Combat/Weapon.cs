using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "RPG/New Weapon")]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;

        public void Spawn(Transform hand, Animator animator)
        {
            Instantiate(weaponPrefab, hand);
            animator.runtimeAnimatorController = animatorOverride;
        }
    }
}

