/*//////////////////////////////////////////////////////////////////////////////////////////
//      █─▄▄▄▄█▄─█─▄█─▄▄▄─█                                                               //
//      █▄▄▄▄─██─▄▀██─███▀█             Scripts created by Semih Kubilay Çetin            //
//      ▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▄▄▀                                                               //
//////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] private List<SKC_LevelSO> levelDatas = null;
    [SerializeField] private Transform levelContainer = null;
    [SerializeField] private Transform environmentContainer = null;

    // Privates
    private List<Transform> environmentList = new List<Transform>();
    private int levelHolder;

    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        environmentList = environmentContainer.GetChildren();

#if UNITY_EDITOR
        Debug.Log($"<color=green>We have {LevelsCount()} Levels</color>");
#endif
    }

    public int LevelsCount()
    {
        return levelDatas.Count;
    }

    public void SpawnLevel(bool newLevel)  // false == Restart, true == New Level
    {
        switch(newLevel)
        {
            case false:
                StartCoroutine(RestartLevel());

                break;

            case true:
                StartCoroutine(SpawnNewLevel());

                break;

        }
    }

    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(.5f);

        Destroy(levelContainer.GetChild(0).gameObject);

        yield return new WaitForSeconds(.2f);

        foreach (Transform item in environmentList)
        {
            item.gameObject.SetActive(false);
        }

#if UNITY_EDITOR
        Debug.Log($"<color=blue>Unique Level: {levelHolder}   Current Level: {GameManager.SaveData.Level}</color>");
#endif

        environmentList[levelHolder - 1].gameObject.SetActive(true);

        GameObject newLevel = (GameObject)Instantiate(levelDatas[levelHolder - 1].GetLevelPrefab(), levelContainer);
        newLevel.name = levelHolder.ToString() + " Level";

        RenderSettings.skybox = levelDatas[levelHolder - 1].GetSkyBox();

        PlayerMovement playerMovement = newLevel.GetComponentInChildren<PlayerMovement>();
        playerMovement.Init();
    }

    private IEnumerator SpawnNewLevel()
    {
        if(levelContainer.childCount != 0)
        {
            yield return new WaitForSeconds(.5f);
            
            Destroy(levelContainer.GetChild(0).gameObject);

            yield return new WaitForSeconds(.2f);

            int levelIndex;

            if(GameManager.SaveData.Level > LevelsCount())
            {
                levelIndex = Random.Range(1, LevelsCount());
            }
            else
            {
                levelIndex = GameManager.SaveData.Level;
            }

            levelHolder = levelIndex;

            foreach (Transform item in environmentList)
            {
                item.gameObject.SetActive(false);
            }

#if UNITY_EDITOR
            Debug.Log($"<color=blue>Unique Level: {levelIndex}   Current Level: {GameManager.SaveData.Level}</color>");
#endif

            environmentList[levelIndex - 1].gameObject.SetActive(true);

            GameObject newLevel = (GameObject)Instantiate(levelDatas[levelIndex - 1].GetLevelPrefab(), levelContainer);
            newLevel.name = levelIndex.ToString() + " Level";

            RenderSettings.skybox = levelDatas[levelIndex - 1].GetSkyBox();

            PlayerMovement playerMovement = newLevel.GetComponentInChildren<PlayerMovement>();
            playerMovement.Init();
        }
        else
        {
            yield return new WaitForSeconds(.2f);

            int levelIndex;

            if (GameManager.SaveData.Level > LevelsCount())
            {
                levelIndex = Random.Range(1, LevelsCount());
            }
            else
            {
                levelIndex = GameManager.SaveData.Level;
            }

            levelHolder = levelIndex;

            foreach (Transform item in environmentList)
            {
                item.gameObject.SetActive(false);
            }

#if UNITY_EDITOR
            Debug.Log($"<color=blue>Unique Level: {levelIndex}   Current Level: {GameManager.SaveData.Level}</color>");
#endif

            environmentList[levelIndex - 1].gameObject.SetActive(true);

            GameObject newLevel = (GameObject)Instantiate(levelDatas[levelIndex - 1].GetLevelPrefab(), levelContainer);
            newLevel.name = levelIndex.ToString() + " Level";

            RenderSettings.skybox = levelDatas[levelIndex - 1].GetSkyBox();

            PlayerMovement playerMovement = newLevel.GetComponentInChildren<PlayerMovement>();
            playerMovement.Init();
        }
    }
}
/* Tip    #if UNITY_EDITOR
          Debug.Log("Unity Editor");
          #endif                          Tip End */