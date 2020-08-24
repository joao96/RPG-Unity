using UnityEngine;

using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{    
    public class Fighter : MonoBehaviour, IAction 
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttacks = 1.25f;
        [SerializeField] float weaponDamage = 5f;

        Health target;
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
            
            if (target.IsDead()) return;

            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            // look at the target
            transform.LookAt(target.transform);
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }
        }

        private void TriggerAttack()
        {
            myAnimator.ResetTrigger("stopAttack");
            // will call the Hit() in the animation
            myAnimator.SetTrigger("attack");
        }

        // Animation Event
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) <= weaponRange;
        }

        // called in playerController
        public bool CanAttack(CombatTarget combatTarget)
        {
            // not a combat target
            if (combatTarget == null) return false;
            
            Health targetToTest = combatTarget.GetComponent<Health>();
            
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(CombatTarget combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            myAnimator.ResetTrigger("attack");
            myAnimator.SetTrigger("stopAttack");
        }
    }
}