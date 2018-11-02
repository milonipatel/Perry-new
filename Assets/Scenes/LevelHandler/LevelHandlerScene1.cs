using UnityEngine;
using System.Collections;

namespace RunAndJump {
   
   /* public class LevelObject{
        public string sceneName;
        public string levelName;

        public void setScene(string s)
        {
            sceneName = s;
        }

        public void setLevel(string level)
        {
            levelName = level;
        }

    }*/

	public class LevelHandlerScene1 : BaseScene {

		#region EDITOR VARIABLES
	

		[Header ("UI Panel Elements")]
		public TransitionPanel Transition;
		public GameInfoPanel GameInfo;
		public VirtualInputPanel VirtualInput;
		public PauseMenuPanel PauseMenu;
		public HintMessagePanel HintMessage;
		public GoalMenuPanel GoalMenu;
        public LevelsPackage x;
        #endregion

        #region CONSTANTS

        public enum GameState {
			Initializing = 0,
			Playing,
			Paused,
			Win,
			Lose,
		}

		public const int TOTAL_LIVES = 3;
		public const int COIN_SCORE = 100;
		public const int TREASURE_SCORE = 1000;
       // public const int i = 1;
		#endregion

		#region VARIABLES

		public GameState _gameState = GameState.Initializing;
		public int _score;
		public int _lives;
		public float _time;
		public int _treasuresCollected;
		public string _sceneName;
		public string _levelName;
		public int _levelId;
        public int o = 2;
        #endregion

        #region MONOBEHAVIOUR METHODS



        public void setScene(string s)
        {
            _sceneName = s;
        }

        public void setLevel(string level)
        {
            _levelName = level;
        }


        protected override void Awake () {
			base.Awake ();
		}

		public void Start () {
			StartCoroutine(InitSequence());
		}

		public void Update () {
			if(_gameState == GameState.Playing) {
				//UpdateTimerCountdown();
				UpdateGameInfoUI();
			}
		}

		public void OnDestroy() {
			UnSubscribeLevelElementsEvents();
		}

		#endregion

		public IEnumerator InitSequence() {
			_lives = TOTAL_LIVES;
			SubscribeLevelElementsEvents();
			yield return StartCoroutine(InitScene());
		}

		public IEnumerator InitScene() {
			_score = 0;
           // setI m = new setI();
          //  int cc = o;
           // int cc = m.i;
           //m.i = m.i + 1;
            Debug.Log("SCORE" + _score);
            _treasuresCollected = 0;
			_time = 100;// Session.Instance.GetLevelMetadata().TotalTime;
			//_levelId = Session.Instance.GetLevelId();
           // LevelHandlerScene lh = new LevelHandlerScene();
            string level_name = "LEVEL: " + o + "";
            string scene_name = "level" + o + "";
            _sceneName = scene_name;
            _levelName = level_name;
        //    lh.setScene(scene_name);
            // _sceneName = "level1";//Session.Instance.GetLevelMetadata().SceneName;
            Debug.Log("SCENE NAME: " + _sceneName);
          //  lh.setLevel(level_name);
           
            //  Debug.Log("SCENE NAME" + _sceneName);
            // _levelName = "LEVEL 1";//Session.Instance.GetLevelMetadata().LevelName;
            //InputWrapper.Instance.EnableInput(false);

            HideAllThePanels ();
                Transition.gameObject.SetActive(true);
                Transition.DisplayIntro (true);
                Transition.DisplayGameOver (false);
                Transition.SetIntro (_levelId, _levelName, _lives);

            LevelHandlerUtils.DestroyLevel();
            //Application.LoadLevelAdditiveAsync("LevelHandler");
			yield return StartCoroutine( LevelHandlerUtils.LoadLevel(_sceneName));
			yield return new WaitForSeconds(0.5f);

			HideAllThePanels ();
			GameInfo.gameObject.SetActive(true);
			VirtualInput.gameObject.SetActive(true);
			HintMessage.gameObject.SetActive(true);
			UpdateGameInfoUI();
		//	InputWrapper.Instance.EnableInput (true);
			_gameState = GameState.Playing;
		}

		public void HideAllThePanels() {
			Transition.gameObject.SetActive(false);
			GameInfo.gameObject.SetActive(false);
			VirtualInput.gameObject.SetActive(false);
			HintMessage.gameObject.SetActive(false);
			GoalMenu.gameObject.SetActive(false);
			PauseMenu.gameObject.SetActive (false);
		}

		public void SubscribeLevelElementsEvents () {
         

            PlayerController.PlayerDeathEvent += new PlayerController.PlayerDeathDelegate(RestartLevel);
            BronzeCoinCollect.StartInteractionEvent += new BronzeCoinCollect.StartInteractionDelegate(CollectCoin);
            //	InteractiveSignController.StartInteractionEvent += new InteractiveSignController.StartInteractionDelegate(DisplayHint);
            //	InteractiveSignController.StopInteractionEvent += new InteractiveSignController.StopInteractionDelegate(HideHint);

            //InteractiveTreasureController.StartInteractionEvent += new InteractiveTreasureController.StartInteractionDelegate(CollectTreasure);
            InteractiveGoalFlagController.StartInteractionEvent += new InteractiveGoalFlagController.StartInteractionDelegate(LevelFinish);
        }

		public void UnSubscribeLevelElementsEvents () {
			PlayerController.PlayerDeathEvent -= new PlayerController.PlayerDeathDelegate(RestartLevel);
		//	InteractiveSignController.StartInteractionEvent -= new InteractiveSignController.StartInteractionDelegate(DisplayHint);
		//	InteractiveSignController.StopInteractionEvent -= new InteractiveSignController.StopInteractionDelegate(HideHint);
            BronzeCoinCollect.StartInteractionEvent -= new BronzeCoinCollect.StartInteractionDelegate(CollectCoin);
		//	InteractiveTreasureController.StartInteractionEvent -= new InteractiveTreasureController.StartInteractionDelegate(CollectTreasure);
			InteractiveGoalFlagController.StartInteractionEvent -= new InteractiveGoalFlagController.StartInteractionDelegate(LevelFinish);
		}

		public void UpdateGameInfoUI() {
          //  Debug.Log("Upadtgameinfoui CALLEEDDDDDDDD");
			GameInfo.SetScore(_score);
			GameInfo.SetLives(_lives);
		}

		public void UpdateTimerCountdown() {
			_time = _time - UnityEngine.Time.deltaTime;
		}
        public static Level _level;
        #region LEVEL ELEMENTS EVENTS

        public void LevelFinish () {
       //     setI m = new setI();
         //   m.i = m.i + 1;
         //   o++;
            Debug.Log("Value of o " + o);
           // m.setII(m.i);
            //   i = i + 1;
            _gameState = GameState.Win;
			Time.timeScale = 0;
			GoalMenu.SetScore(_score);
            //GoalMenu.SetTreasures(_treasuresCollected);
            //	HideAllThePanels ();
            /*GoalMenu.EnableNext(Session.Instance.HasNext());
			GoalMenu.gameObject.SetActive(true);
			*/
            //Session.Instance.addLevel();


            // LevelHandlerUtils.DestroyLevel();

            //  if (_level == null)
            //{
            //  Debug.Log("_level::mmmmmmmmmm");
            //   Debug.Log(_level.gameObject);
            //Destroy(_level.gameObject);
            //Destroy(_level.gameObject);
            //   Destroy(GameObject.Find("level1"));
            // StartCoroutine(InitScene());
            //Destroy(_level.gameObject);
            // }
            // StartCoroutine(InitScene());
            _level = GameObject.FindObjectOfType<Level>();


            //   HideAllThePanels();
            // Transition.gameObject.SetActive(true);
            //Transition.DisplayIntro(true);
            //Transition.DisplayGameOver(false);
            //   Transition.SetIntro(_levelId, _levelName, _lives);
            LevelHandlerUtils.DestroyLevel();
            GoToScene(Scene.SampleScene);
           // Debug.Log("size of metadatalist "+x.metadataList.Count);
        }

		public void CollectTreasure () {
			_score += TREASURE_SCORE;
			_treasuresCollected++;
		}

		public void CollectCoin () {

			_score += COIN_SCORE;
            UpdateGameInfoUI();
          // Debug.Log("SCORE"+ _score);
		}

		public void DisplayHint(string message) {
			HintMessage.SetMessage(message);
			HintMessage.Show();
		}

		public void HideHint() {
			HintMessage.Hide();
		}

		public void RestartLevel() {
			//Debug.Log("RestartLevel called!");
			_gameState = GameState.Lose;
			_lives--;
			if(_lives <= 0) {
				StartCoroutine(GameOver());
			} else {
				HideAllThePanels();
			    Transition.gameObject.SetActive(true);
				Transition.DisplayIntro (true);
				Transition.DisplayGameOver (false);
				Transition.SetIntro (_levelId, _levelName, _lives);
				LevelHandlerUtils.DestroyLevel();
				StartCoroutine(InitScene());
			}
		}

		public void LoadNextLevel() {
			HideAllThePanels();
			Session.Instance.PlayNext();
			StartCoroutine(InitScene());
		}

		public IEnumerator GameOver() {
			HideAllThePanels ();
			Transition.gameObject.SetActive(true);
			Transition.DisplayIntro (false);
			Transition.DisplayGameOver (true);
            LevelHandlerUtils.DestroyLevel();
			yield return new WaitForSeconds (2);
            GoToScene(Scene.SampleScene);
		}

		#endregion

		#region ONCLICK EVENTS

		// All the events of the buttons, except VirtualInput, are handled here.

		public void PauseButtonOnClick() {
			//Debug.Log("PauseButtonOnClick called...");
			_gameState = GameState.Paused;
			Time.timeScale = 0;
			PauseMenu.gameObject.SetActive(true);
			GameInfo.gameObject.SetActive(false);
			VirtualInput.gameObject.SetActive(false);
		}

		public void QuitButtonOnClick() {
		//Debug.Log("QuitButtonOnClick called...");
			Time.timeScale = 1;
            GoToScene(Scene.SampleScene);
		}

		public void PlayButtonOnClick() {
			//Debug.Log("PlayButtonOnClick called...");
			Time.timeScale = 1;
			PauseMenu.gameObject.SetActive(false);
			GameInfo.gameObject.SetActive(true);
			VirtualInput.gameObject.SetActive(true);
			_gameState = GameState.Playing;
		}

		public void NextButtonOnClick() {
		//	Debug.Log("NextButtonOnClick called...");
			Time.timeScale = 1;
			LoadNextLevel();
		}
	
		#endregion
	}
}