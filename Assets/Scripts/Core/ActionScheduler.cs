using UnityEngine;

namespace RPG.Core
{
    // will remove the cycle dependencies (common ground for movement and figter, for example)
    public class ActionScheduler : MonoBehaviour 
    {
        IAction _currentAction; 

        // substitution principle (can be passed many things that inherit monobehaviour)
        public void StartAction(IAction action)
        {
            if (_currentAction == action) return;

            if (_currentAction != null)
            {
                _currentAction.Cancel();
            }

            _currentAction = action;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
    
}
