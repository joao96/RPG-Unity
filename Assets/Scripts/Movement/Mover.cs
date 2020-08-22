﻿using UnityEngine;
using UnityEngine.AI;

using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent _myNavMeshAgent;
        Animator _myAnimator;
        ActionScheduler _actionScheduler;

        void Start()
        {
            _myNavMeshAgent = GetComponent<NavMeshAgent>();
            _myAnimator = GetComponent<Animator>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            _actionScheduler.StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            _myNavMeshAgent.destination = destination;
            _myNavMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            _myNavMeshAgent.isStopped = true;
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
