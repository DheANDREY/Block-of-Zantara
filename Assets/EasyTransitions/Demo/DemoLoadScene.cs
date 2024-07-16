using UnityEngine;

namespace EasyTransition
{

    public class DemoLoadScene : MonoBehaviour
    {
        public TransitionSettings transition;
        public float startDelay;

        
        public void LoadScene(string _sceneName)
        {
            Time.timeScale = 1f;
            TransitionManager.Instance().Transition(_sceneName, transition, startDelay);
        }   
    }

}


