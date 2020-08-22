using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{    
    public class Fighter : MonoBehaviour 
    {
        [SerializeField] private float _weaponRange = 2f;
        private Transform _target;

        // cached
        Mover _mover;

        private void Start() 
        {
            _mover = GetComponent<Mover>();
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
                _mover.Stop();
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.position) <= _weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            _target = combatTarget.transform;
        }

        public void Cancel()
        {
            _target = null;
        }    
    }
}