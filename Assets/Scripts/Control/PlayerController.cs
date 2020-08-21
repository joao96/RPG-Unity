﻿using UnityEngine;
using RPG.Movement;
using RPG.Combat;

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
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if (target == null) continue;
                
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                // just hovering over the target OR is actively attacking it
                // able to change the UI cursor, for example
                // if i'm hovering an enemy, i wont display the movement cursor. Instead, i'll show the attack cursor
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hitInfo;

            bool hasHit = Physics.Raycast(GetMouseRay(), out hitInfo);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    mover.MoveTo(hitInfo.point);
                }
                return true;
            }

            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}