using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
namespace RunAndJump
{

    public class LevelSelectScene : BaseScene
    {

        public List<LevelSlot> LevelSlots;

        public void Start()
        {
            InitLevelSlots();
        }

        public void InitLevelSlots()
        {
            Debug.Log("VALUE of LEVEL SLOTS ");
            Debug.Log(LevelSlots.Count);
            for (int i = 0; i < LevelSlots.Count; i++)
            {
                LevelSlots[i].Init((i < Session.Instance.GetTotalLevels()), i + 1, LevelSlotOnClick);
              //     LevelSlots[i].Init((i < 1), i + 1, LevelSlotOnClick);

            }
        }

        public void LevelSlotOnClick(int levelId)
        {
            Debug.Log(levelId);
            Session.Instance.PlayLevel(levelId);
            GoToScene(Scene.LevelHandler);
        }
    }

}
