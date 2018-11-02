using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace RunAndJump {

	[RequireComponent(typeof(Button))]
	[RequireComponent(typeof(Image))]
	public class LevelSlot : MonoBehaviour {

		public Sprite OnStateSprite;
		public Sprite OffStateSprite;
		public Text Label;

		public Button _button;
		public Image _image;

		public void Awake() {
			_button = GetComponent<Button> ();
			_image = GetComponent<Image> ();
		}

		public void Init(bool isOn, int levelId, System.Action<int> cb) {
            if (isOn) {
				_button.enabled = true;
				_image.sprite = OnStateSprite;
			} else {
				_button.enabled = false;
				_image.sprite = OffStateSprite;
				Label.color = Color.grey;
			}
			Label.text = levelId.ToString();
            Debug.Log("VALUE of Text ");
            Debug.Log(Label.text);
            if (cb != null) {
				_button.onClick.AddListener (() => {
					cb (levelId);
				});
			}
		}
	}

}
