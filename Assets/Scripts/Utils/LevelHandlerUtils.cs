using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace RunAndJump {

	public class LevelHandlerUtils : MonoBehaviour {

        public static Level _level;

		public static IEnumerator LoadLevel(string sceneName) {
          // Application.LoadLevelAdditive(sceneName);
            Debug.Log("_level addede:" + sceneName);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
 
            yield return 0;
            _level = GameObject.FindObjectOfType<Level>();

        }

		public static void DestroyLevel() {
           // Resources.UnloadUnusedAssets();
			if(_level != null) {
				Destroy(_level.gameObject);
            }
		}
	}
}
