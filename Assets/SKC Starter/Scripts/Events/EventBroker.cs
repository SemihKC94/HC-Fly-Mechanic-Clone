/*//////////////////////////////////////////////////////////////////////////////////////////
//      █─▄▄▄▄█▄─█─▄█─▄▄▄─█                                                               //
//      █▄▄▄▄─██─▄▀██─███▀█             Scripts created by Semih Kubilay Çetin            //
//      ▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▄▄▀                                                               //
//////////////////////////////////////////////////////////////////////////////////////////*/
using System;

public class EventBroker
{
    public static event Action OnPlay;
    public static void InvokePlay()
    {
        OnPlay?.Invoke();
    }

    public static event Action OnFail;
    public static void InvokeFail()
    {
        OnFail?.Invoke();
    }
    
    public static event Action OnLevelReset;
    public static void InvokeLevelReset()
    {
        OnLevelReset?.Invoke();
        OnSave?.Invoke();
    }

    public static event Action OnLevelSuccess;
    public static void InvokeLevelSuccess()
    {
        OnLevelSuccess?.Invoke();
        OnSave?.Invoke();
    }

    public static event Action OnNextLevel;
    public static void InvokeNextLevel()
    {
        OnNextLevel?.Invoke();
    }

    public static event Action OnSave;
    public static void InvokeSave()
    {
        OnSave?.Invoke();
    }
}
/* Tip    #if UNITY_EDITOR
          Debug.Log("Unity Editor");
          #endif                          Tip End */
