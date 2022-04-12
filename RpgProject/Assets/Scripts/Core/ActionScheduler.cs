using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction currentAction;

        public void StartAction(IAction action)
        {
            if(currentAction == null)
            {
                currentAction = action;
            }
            else if (currentAction != action)
            {
                currentAction.Stop();
                currentAction = action;
            }
        }
    }

}
