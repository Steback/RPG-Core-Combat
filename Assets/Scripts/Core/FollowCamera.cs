using UnityEngine;

namespace Core
{
    public class FollowCamera : MonoBehaviour
    {
        private Camera _camera;
    
        public Transform target;

        private void Awake()
        {
            _camera = Camera.current;
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}
