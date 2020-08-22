using UnityEngine;

using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{    
    public class Fighter : MonoBehaviour, IAction 
    {
        [SerializeField] private float _weaponRange = 2f;
        private Transform _target;

        // cached
        Mover _mover;
        ActionScheduler _actionScheduler;

        private void Start() 
        {
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            // if it's not fighting
            if (_target == null) return;

            if (!GetIsInRange())
            {
                _mover.MoveTo(_target.position);
            }
            else
            {
                _mover.Cancel();
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.position) <= _weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            _actionScheduler.StartAction(this);
            _target = combatTarget.transform;
        }

        public void Cancel()
        {
            _target = null;
        }    
    }
}