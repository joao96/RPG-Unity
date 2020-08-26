using UnityEngine;
using UnityEngine.AI;

using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent myNavMeshAgent;
        Animator myAnimator;
        Health health;
        ActionScheduler actionScheduler;

        void Start()
        {
            health = GetComponent<Health>();
            myNavMeshAgent = GetComponent<NavMeshAgent>();
            myAnimator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        void Update()
        {
            myNavMeshAgent.enabled = !health.IsDead();

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            actionScheduler.StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            myNavMeshAgent.destination = destination;
            myNavMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            myNavMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = myNavMeshAgent.velocity; // world space
            Vector3 localVelocity = transform.InverseTransformDirection(velocity); // local space
            // z -> forward (it's what the blender tree uses to animate properly)!
            float speed = localVelocity.z;
            myAnimator.SetFloat("forwardSpeed", speed);
        }
    }
}
