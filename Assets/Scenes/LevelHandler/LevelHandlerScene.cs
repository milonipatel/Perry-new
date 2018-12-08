using UnityEngine;
using System.Collections;

namespace RunAndJump {

   /* public class LevelObject{
        private string sceneName;
        private string levelName;

        public void setScene(string s)
        {
            sceneName = s;
        }

        public void setLevel(string level)
        {
            levelName = level;
        }

    }*/

	public class LevelHandlerScene : BaseScene {

		#region EDITOR VARIABLES
	

		[Header ("UI Panel Elements")]
		public TransitionPanel Transition;
		public GameInfoPanel GameInfo;
		public VirtualInputPanel VirtualInput;
		public PauseMenuPanel PauseMenu;
		public HintMessagePanel HintMessage;
		public GoalMenuPanel GoalMenu;
        public LevelsPackage x;
        public static Level _level;
        #endregion

        #region CONSTANTS

        public enum GameState {
			Initializing = 0,
			Playing,
			Paused,
			Win,
			Lose,
		}

        public const int TOTAL_LIVES = 5;
        public const int COIN_SCORE = 100;
        public const int TREASURE_SCORE = 1000;
        // private const int i = 1;
        #endregion

        #region VARIABLES

        public GameState _gameState = GameState.Initializing;
		public int _score ;
        public int _lives;
        public float _time;
        public int _treasuresCollected;
        public string _sceneName;
        public string _levelName;
        public int _levelId;
        public int o = 1;
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
            _score = COIN_SCORE;
			SubscribeLevelElementsEvents();
			yield return StartCoroutine(InitScene());
		}

        public IEnumerator InitScene() {
            if (o == 1)
                _score = 0;
            _treasuresCollected = 0;
			_time = 100;
			_levelId = o;
            LevelHandlerScene lh = new LevelHandlerScene();
            string level_name = "LEVEL: " + o + "";
            string scene_name = "level" + o + "";
            _sceneName = scene_name;
            _levelName = level_name;
            lh.setScene(scene_name);
            Debug.Log("SCENE NAME: " + _sceneName);
            lh.setLevel(level_name);
            InputWrapper.Instance.EnableInput(false);

            HideAllThePanels ();
                Transition.gameObject.SetActive(true);
                Transition.DisplayIntro (true);
                Transition.DisplayGameOver (false);
                Transition.SetIntro (_levelId, _levelName, _lives);

           LevelHandlerUtils.DestroyLevel();
			yield return StartCoroutine( LevelHandlerUtils.LoadLevel(_sceneName));
			yield return new WaitForSeconds(1.5f);

			HideAllThePanels ();
			GameInfo.gameObject.SetActive(true);
			//VirtualInput.gameObject.SetActive(true);
			HintMessage.gameObject.SetActive(true);
			UpdateGameInfoUI();
			InputWrapper.Instance.EnableInput (true);
			_gameState = GameState.Playing;
		}

        public void HideAllThePanels() {
			Transition.gameObject.SetActive(false);
			GameInfo.gameObject.SetActive(false);
		//	VirtualInput.gameObject.SetActive(false);
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
			GameInfo.SetScore(_score);
			GameInfo.SetLives(_lives);
		}

        public void UpdateTimerCountdown() {
			_time = _time - UnityEngine.Time.deltaTime;
		}
        // private static Level _level;
        #region LEVEL ELEMENTS EVENTS

        public void LevelFinish () {
            if(o<3)
            o++;
            else
                StartCoroutine(GameOver());

            _gameState = GameState.Win;
            _level = GameObject.FindObjectOfType<Level>();
            GoalMenu.SetScore(_score);
           // GoalMenu.SetTreasures(_treasuresCollected);
            HideAllThePanels();
           // yield return new WaitForSeconds(1);
            GoalMenu.gameObject.SetActive(true);
            LevelHandlerUtils.DestroyLevel();
            StartCoroutine(InitScene());
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
		//	HintMpublicShow();
		}

        public void HideHint() {
			HintMessage.Hide();
		}

        public void RestartLevel() {
			//Debug.Log("RestartLevel called!");
			_gameState = GameState.Lose;
			_lives--;
            _score = 0;
			if(_lives <= 0) {
                LevelHandlerUtils.DestroyLevel();
                StartCoroutine(GameOver());
			} else {
				HideAllThePanels();
			    Transition.gameObject.SetActive(true);
				Transition.DisplayIntro (true);
				Transition.DisplayGameOver (false);
				Transition.SetIntro (_levelId, _levelName, _lives);
                //_level = GameObject.FindObjectOfType<Level>();
                LevelHandlerUtils.DestroyLevel();
				StartCoroutine(InitScene());
			}
		}

        public void LoadNextLevel() {
			HideAllThePanels();
			//Session.Instance.PlayNext();
			StartCoroutine(InitScene());
		}

        public IEnumerator GameOver() {
            //_gameState = GameState.Win;
            HideAllThePanels();
			Transition.gameObject.SetActive(true);
			Transition.DisplayIntro (false);
			Transition.DisplayGameOver (true);
            Transition.SetIntro(_levelId, _levelName, _lives);
            _level = GameObject.FindObjectOfType<Level>();
            LevelHandlerUtils.DestroyLevel();
            yield return new WaitForSeconds(0);
            GoToScene(Scene.SampleScene);
		}

		#endregion

		#region ONCLICK EVENTS

		// All the events of the buttons, except VirtualInput, are handled here.

		public void PauseButtonOnClick() {
			_gameState = GameState.Paused;
			Time.timeScale = 0;
			PauseMenu.gameObject.SetActive(true);
			GameInfo.gameObject.SetActive(false);
//			VirtualInput.gameObject.SetActive(false);
		}

		public void QuitButtonOnClick() {
		//Debug.Log("QuitButtonOnClick called...");
			Time.timeScale = 1;
            GoToScene(Scene.SampleScene);
		}

		public void PlayButtonOnClick() {
			Time.timeScale = 1;
			PauseMenu.gameObject.SetActive(false);
			GameInfo.gameObject.SetActive(true);
		//	VirtualInput.gameObject.SetActive(true);
			_gameState = GameState.Playing;
		}

		public void NextButtonOnClick() {
			Time.timeScale = 1;
			LoadNextLevel();
		}
	
		#endregion
	}
}