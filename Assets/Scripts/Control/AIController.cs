using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;


namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;

        //cached
        Mover mover;
        Health health;
        Fighter fighter;
        GameObject player;
        ActionScheduler actionScheduler;


        //state -> the AI memory
        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;

        private void Start() 
        {
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
            actionScheduler = GetComponent<ActionScheduler>();

            player = GameObject.FindWithTag("Player");
            guardPosition = transform.position;
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (PlayerIsInRange() && fighter.CanAttack(player))
            {
                AttackBehaviour();
                timeSinceLastSawPlayer = 0f;
            }
            else if (timeSinceLastSawPlayer <= suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                GuardBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            mover.StartMoveAction(guardPosition);
        }

        private void SuspicionBehaviour()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private bool PlayerIsInRange()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer <= chaseDistance;
        }

        // called by unity
        private void OnDrawGizmosSelected() 
        {
            // Draw a blue sphere at the transform's position
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
    
}
