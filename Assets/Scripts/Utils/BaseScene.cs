using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace RunAndJump
{

    public class BaseScene : MonoBehaviour
    {

        public enum Scene
        {
            SampleScene,
            level1,
            level2,
            LevelScreen,
            LevelHandler,
            LevelHandler1
        }

        protected virtual void Awake()
        {
           // AudioPlayer.Instantiate();
         // Session.Instantiate();
        }

        protected void GoToScene(Scene scene)
        {
            //Application.LoadLevel(scene.ToString());
            SceneManager.LoadScene(scene.ToString());
        }

        protected void GoToScene(string sceneName)
        {
            // Application.LoadLevel(sceneName);
            SceneManager.LoadScene(sceneName);
        }
    }

}