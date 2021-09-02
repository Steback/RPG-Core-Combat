using System;
using UnityEngine;

namespace Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] private float waypointRadius = 0.5f;
        
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(transform.GetChild(i).position, waypointRadius);
            }
        }
    }
}
