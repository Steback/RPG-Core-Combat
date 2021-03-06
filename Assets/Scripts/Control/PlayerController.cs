using Combat;
using Core;
using Movement;
using UnityEngine;

namespace Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;
        private Camera _mainCamera;
        private Fighter _fighter;
        private Health _health;

        // Start is called before the first frame update
        void Awake()
        {
            _mover = GetComponent<Mover>();
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
            _mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (_health.IsDeath()) return;
            
            if (InteractWithCombat()) return;
            
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMousRay());
            foreach (var hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(!target || !target.isValid) continue;
                
                if (Input.GetMouseButton(0))
                {
                    _fighter.Attack(target.gameObject);
                }

                return true;
            }

            return false;
        }

        private bool InteractWithMovement()
        {
            if (Physics.Raycast(GetMousRay(), out var hit)) 
            { 
                if (Input.GetMouseButton(0))
                { 
                    _mover.StartMove(hit.point);
                }

                return true;
            }

            return false;
        }

        private Ray GetMousRay()
        {
            return _mainCamera.ScreenPointToRay(Input.mousePosition);
        }
    }
}
