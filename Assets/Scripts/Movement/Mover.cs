using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        NavMeshAgent _myNavMeshAgent;
        Animator _myAnimator;

        void Start()
        {
            _myNavMeshAgent = GetComponent<NavMeshAgent>();
            _myAnimator = GetComponent<Animator>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination)
        {
            _myNavMeshAgent.destination = destination;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = _myNavMeshAgent.velocity; // world space
            Vector3 localVelocity = transform.InverseTransformDirection(velocity); // local space
            // z -> forward (it's what the blender tree uses to animate properly)!
            float speed = localVelocity.z;
            _myAnimator.SetFloat("forwardSpeed", speed);
        }
    }
}
