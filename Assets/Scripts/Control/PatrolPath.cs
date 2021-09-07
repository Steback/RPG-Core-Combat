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
                Gizmos.DrawSphere(GetWaypoint(i), waypointRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(GetNextIndex(i)));
            }
        }

        public Vector3 GetWaypoint(int index)
        {
            return transform.GetChild(index).position;
        }

        public int GetNextIndex(int index)
        {
            int nextIndex = index + 1;
            return nextIndex >= transform.childCount ? 0 : nextIndex;
        }
    }
}
