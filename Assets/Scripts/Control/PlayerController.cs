using Movement;
using UnityEngine;

namespace Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;
        private Camera _mainCamera;

        // Start is called before the first frame update
        void Awake()
        {
            _mover = GetComponent<Mover>();
            _mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }
    
        private void MoveToCursor()
        { 
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out var hit)) 
            { 
                _mover.MoveTo(hit.point);
            }
        }
    }
}
