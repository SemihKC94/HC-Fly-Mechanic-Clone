/*//////////////////////////////////////////////////////////////////////////////////////////
//      █─▄▄▄▄█▄─█─▄█─▄▄▄─█                                                               //
//      █▄▄▄▄─██─▄▀██─███▀█             Scripts created by Semih Kubilay Çetin            //
//      ▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▄▄▀                                                               //
//////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIController : MonoBehaviour
{
    [Header("Joystick")] [SerializeField] private Joystick joyStick = null;
    [Header("Camera Controller")] [SerializeField] private CamController myCam = null;

    [Space,Header("GUI Config")]
    [SerializeField] [Range(0.1f, 5f)] private float splashScreenDuration = 1f;

    [Space, Header("Panels")]
    [SerializeField] private GameObject startPanel = null;
    [SerializeField] private GameObject inGamePanel = null;
    [SerializeField] private GameObject winPanel = null;
    [SerializeField] private GameObject losePanel = null;

    [Space, Header("UI Elements")]
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI inGameCurrency = null;

    [Header("Buttons")]
    [SerializeField] private Button tapToStartButton = null;
    [SerializeField] private Button nextLevelButton = null;
    [SerializeField] private Button restartButton = null;

    // Privates
    // Components
    private SKC_TransitionScript transitionIntro;
    private SKC_TransitionScript transitionOutro;

    // Values
    private double softCurrency;
    #region Singleton Pattern
    public static GUIController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    #region Unity Methods
    private void Start()
    {
        // Transition
        transitionIntro = transform.parent.GetChild(1).GetComponent<SKC_TransitionScript>(); // Assign Intro
        transitionOutro = transform.parent.GetChild(2).GetComponent<SKC_TransitionScript>(); // Assign Outro

        // Buttons Assign
        tapToStartButton.onClick.AddListener(() => StartPlaying());
        nextLevelButton.onClick.AddListener(() => NextLevel());
        restartButton.onClick.AddListener(() => RestartGame());

        // Panels
        startPanel.SetActive(true);
        inGamePanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);

    }

    #endregion

    #region Accessible Methods

    public void Init()
    {
        // DO SplashScreen
        transitionOutro.ManuelTransition(splashScreenDuration, 0f); // SplashScreen

        // Get Level Prefab
        LevelManager.Instance.SpawnLevel(true);

        // Events
        EventBroker.OnPlay += GameOnPlay;
        EventBroker.OnFail += GameOnFail;
        EventBroker.OnLevelSuccess += GameOnFinished;
        EventBroker.OnNextLevel += OnNextLevel;
        EventBroker.OnLevelReset += OnRestartLevel;

        UpdateCurrency(0);
    }
    public void UpdateCurrency(double take)
    {
        GameManager.SaveData.SoftCurrency += take;
        softCurrency = GameManager.SaveData.SoftCurrency;
        inGameCurrency.SetText(softCurrency.ToString());
    }

    public Vector2 GetJoystickDirection()
    {
        return joyStick.Direction;
    }

    #endregion

    #region Unaccessible Methods

    private void GameOnPlay()
    {
        startPanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        inGamePanel.SetActive(true);

    }

    private void GameOnFail()
    {
        startPanel.SetActive(false);
        winPanel.SetActive(false);
        inGamePanel.SetActive(false);
        losePanel.SetActive(true);
    }

    private void GameOnFinished()
    {
        startPanel.SetActive(false);
        inGamePanel.SetActive(false);
        losePanel.SetActive(false);
        winPanel.SetActive(true);
    }

    private void OnNextLevel()
    {
        // TODO : Ad will be integrated.

        StartCoroutine(SplashScreen());

        // Get Level Prefab
        LevelManager.Instance.SpawnLevel(true);

        startPanel.SetActive(true);
        winPanel.SetActive(false);
        inGamePanel.SetActive(false);
        losePanel.SetActive(false);
    }

    private void OnRestartLevel()
    {
        // TODO : Ad will be integrated.

        StartCoroutine(SplashScreen());

        // Get Level Prefab
        LevelManager.Instance.SpawnLevel(false);

        startPanel.SetActive(true);
        winPanel.SetActive(false);
        inGamePanel.SetActive(false);
        losePanel.SetActive(false);
    }

    private void StartPlaying()
    {
        EventBroker.InvokePlay();
    }

    private void NextLevel()
    {
        EventBroker.InvokeNextLevel();
    }

    private void RestartGame()
    {
        EventBroker.InvokeLevelReset();
    }

    private IEnumerator SplashScreen()
    {
#if UNITY_EDITOR
        Debug.Log($"<color=orange>SPLASH SCREEN</color>");
#endif

        transitionIntro.ManuelTransition(.1f, 1f); // SplashScreen

        yield return new WaitForSeconds(1f);

        myCam.ResetCamera();

        transitionIntro.ManuelTransition(splashScreenDuration, 0f); // SplashScreen
    }


    #endregion
}
/* Tip    #if UNITY_EDITOR
          Debug.Log("Unity Editor");
          #endif                          Tip End */