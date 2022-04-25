/*//////////////////////////////////////////////////////////////////////////////////////////
//      █─▄▄▄▄█▄─█─▄█─▄▄▄─█                                                               //
//      █▄▄▄▄─██─▄▀██─███▀█             Scripts created by Semih Kubilay Çetin            //
//      ▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▄▄▀                                                               //
//////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static int attempts = 0;
    public void Init()
    {
        EventBroker.OnLevelSuccess += OnLevelSuccess;
        EventBroker.OnFail += OnFail;
        EventBroker.OnPlay += OnPlay;
    }
    public void Set()
    {
    }

    #region EventHandlers

    private void OnPlay()
    {
        int level = GameManager.SaveData.Level;

        Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
        keyValuePairs.Add("level_id", level.ToString());
        keyValuePairs.Add("attempt", attempts.ToString());

        string levelString = level.ToString();
        if (level < 10)
        {
            levelString = "0" + level.ToString();
        }

    }

    private void OnFail()
    {
        attempts++;
        int level = GameManager.SaveData.Level;

        Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
        keyValuePairs.Add("level_id", level.ToString());
    }

    private void OnLevelSuccess()
    {
        int level = GameManager.SaveData.Level;
        Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
        keyValuePairs.Add("level_id", level.ToString());
        keyValuePairs.Add("attempt", attempts.ToString());


        string levelString = level.ToString();
        if (level < 10)
        {
            levelString = "0" + level.ToString();
        }

        attempts = 0;
    }

    #endregion
}
/* Tip    #if UNITY_EDITOR
          Debug.Log("Unity Editor");
          #endif                          Tip End */