using UnityEngine;
using System.Collections;

public class InputWrapper : MonoBehaviour {

	// A custom class to capture keyboard and virtual input

	public bool _virtualLeft = false;
	public bool _virtualRight = false;
	public bool _virtualUp = false; 
	public bool _enabled = true;
	public static InputWrapper _instance;

	public static InputWrapper Instance {
		get {
			if(_instance == null) {
				GameObject go = new GameObject("InputWrapper");
				go.AddComponent<InputWrapper>();
			}
			return _instance;
		}
	}

	public void Awake() {
		_instance = this;
	}

	public void EnableInput(bool enabled) {
		_enabled = enabled;
		Reset ();
	}

	public void Reset() {
		_virtualLeft = false;
		_virtualRight = false;
		_virtualUp = false; 
	}

	public bool VirtualLeft {
		get { return _virtualLeft; }
		set { _virtualLeft = value; }
	}
	
	public bool VirtualRight {
		get { return _virtualRight; }
		set { _virtualRight = value; }
	}
	
	public bool VirtualUp {
		get { return _virtualUp; }
		set { _virtualUp = value; }
	}

	public bool GetRigth() {
		return _enabled && (Input.GetKey (KeyCode.RightArrow) || _virtualRight);
	}

	public bool GetLeft() {
		return _enabled && (Input.GetKey (KeyCode.LeftArrow) || _virtualLeft);
	}

	public bool GetUp() {
		bool result = _enabled && ((Input.GetKeyDown (KeyCode.Space) || _virtualUp));
		_virtualUp = false; // This force to release the button
		return result;
	}


}
