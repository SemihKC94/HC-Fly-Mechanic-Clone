/*//////////////////////////////////////////////////////////////////////////////////////////
//      █─▄▄▄▄█▄─█─▄█─▄▄▄─█                                                               //
//      █▄▄▄▄─██─▄▀██─███▀█             Scripts created by Semih Kubilay Çetin            //
//      ▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▄▄▀                                                               //
//////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static SaveData SaveData;
    public static GameState GameState = GameState.Wait;

    [Header("GUI Controller")]
    [SerializeField] private GUIController guiController;


    // Privates
    private int currentLevel;
    private int uniqueLevelCount;

    #region Project Setup
    void Awake()
    {
        Application.targetFrameRate = 60;

        EventBroker.OnPlay += OnPlay;
        EventBroker.OnFail += OnFail;
        EventBroker.OnLevelSuccess += OnLevelFinished;
        EventBroker.OnLevelReset += OnLevelReset;
        EventBroker.OnNextLevel += OnNextLevel;
        EventBroker.OnSave += OnSave;

        StartCoroutine(Initialize());
    }
    #endregion


    #region EventHandlers

    private void OnFail()
    {
        DataManager.SaveData(StringConstants.SaveData, SaveData);
        GameState = GameState.End;
    }

    private void OnPlay()
    {
        GameState = GameState.Play;
    }

    private void OnLevelFinished()
    {
        GameState = GameState.End;
        SaveData.Level += 1;
    }

    private void OnLevelReset()
    {
        GameState = GameState.Wait;
    }

    private void OnNextLevel()
    {
        GameState = GameState.Wait;
    }


    private void OnSave()
    {
        DataManager.SaveData(StringConstants.SaveData, SaveData);
    }

    #endregion EventHandlers

    private IEnumerator Initialize()
    {
        GameState = GameState.Init;

        yield return new WaitForSeconds(0.05f);

        LoadSaveData();
        currentLevel = SaveData.Level;
        uniqueLevelCount = LevelManager.Instance.LevelsCount();
        guiController.Init();

        yield return new WaitForSeconds(0.05f);

        GameState = GameState.Wait;
    }


    private void LoadSaveData()
    {
        var savedData = DataManager.LoadData<SaveData>(StringConstants.SaveData);

        if (savedData == null)
        {
            //new User
            SaveData = new SaveData()
            {
                Level = 1,
                HardCurrency = 0,
                SoftCurrency = 0,
                GamePlayVersion = 1,
            };
            DataManager.SaveData(StringConstants.SaveData, SaveData);
        }
        else
        {
            SaveData = savedData;
        }
    }
}

/* Tip    #if UNITY_EDITOR
          Debug.Log("Unity Editor");
          #endif                          Tip End */