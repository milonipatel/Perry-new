using UnityEngine;
using System.Collections.Generic;

namespace RunAndJump {
    [CreateAssetMenu(fileName = "LevelsPackage", menuName = "Data")]
    public class LevelsPackage : ScriptableObject {

		public const string Suffix = "_level";
		public const string ResourcePath = "LevelsPackage";
		public const string FullPath = "Assets/Resources/LevelsPackage.asset";
		public List<LevelMetadata> metadataList;

       // Debug.log(metadataList.Count);
		public bool hasChanges = true;
        //Debug.log(metadataList.Count);
	}	
}
