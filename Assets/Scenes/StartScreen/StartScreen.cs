using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace RunAndJump
{
    public class StartScreen : BaseScene
    {

        public Text BuildInfoText;

        public void Start()
        {
            //  SetBuildInfo();
            //AudioPlayer.Instance.StopBgm();
          
        }

        public void SetBuildInfo()
        {
            string info = "";
            BuildInfoText.text = info;
        }

        public void ActiveZoneOnClick()
        {
            GoToScene(Scene.LevelHandler);
        }
    }
}