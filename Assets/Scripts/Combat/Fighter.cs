using UnityEngine;

using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{    
    public class Fighter : MonoBehaviour, IAction 
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttacks = 1.25f;

        private Transform target;
        float timeSinceLastAttack = 0f;

        // cached
        Mover mover;
        Animator myAnimator;
        ActionScheduler actionScheduler;

        private void Start() 
        {
            mover = GetComponent<Mover>();
            myAnimator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            // if it's not fighting
            if (target == null) return;

            if (!GetIsInRange())
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                myAnimator.SetTrigger("attack");
                timeSinceLastAttack = 0f;
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) <= weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }

        // Animation Event
        void Hit()
        {
        }    
    }
}