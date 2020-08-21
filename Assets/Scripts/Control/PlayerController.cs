using UnityEngine;
using RPG.Movement;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        //cached
        Mover mover;

        void Start()
        {
            mover = GetComponent<Mover>();
        }
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }
        private void MoveToCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            bool hasHit = Physics.Raycast(ray, out hitInfo);

            if (hasHit)
            {

                mover.MoveTo(hitInfo.point);
            }
        }
    }
}