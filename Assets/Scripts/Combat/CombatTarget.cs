using UnityEngine;
using UnityEngine.Serialization;

namespace Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {
        public bool isValid = true;
    }
}
