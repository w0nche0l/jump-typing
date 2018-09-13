using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ObstacleData
{
    public string word;
    public float fightSteps;
}

public class TypingRunner : SerializedMonoBehaviour
{
    [SerializeField]
    private List<ObstacleData> obstacleDataList = new List<ObstacleData>();
    [SerializeField]
    private GameObject obstaclePrefab;
    [SerializeField]
    private Transform obstacleSpawnPoint;
    [SerializeField]
    private Transform obstacleDestinationPoint;
    [SerializeField]
    //private TypingInput typingInput;

    private int obstacleIndex = 0;
    private int currSteps = 0;
    private float startTime = 0.0f;

    public const float SECONDS_PER_STEP = 3.0f;
    public const float PREVIEW_STEPS = 3.0f;

    private List<ObstacleObject> obstacleObjList = new List<ObstacleObject>();

    public void Start()
    {
        InvokeRepeating("Step", 0.0f, SECONDS_PER_STEP);
        startTime = Time.time;
    }

    // Update is for visuals (frames)
    public void Update()
    {
        float currSteps = (Time.time - startTime) / SECONDS_PER_STEP;
        foreach (ObstacleObject o in obstacleObjList)
        {
            o.RunnerUpdate(currSteps);
        }
    }

    // _Tick is for logic (spawning objects)
    private void Step()
    {
        if (obstacleIndex < obstacleDataList.Count && obstacleDataList[obstacleIndex].fightSteps - PREVIEW_STEPS == currSteps)
        {
            SpawnObstacle(obstacleIndex);
            obstacleIndex++;
        }
        currSteps++;
    }

    private void SpawnObstacle(int index)
    {
        ObstacleObject newObject = Instantiate(obstaclePrefab, obstacleSpawnPoint.position, Quaternion.identity).GetComponent<ObstacleObject>();
        newObject.Init(obstacleDataList[index], obstacleSpawnPoint.position, obstacleDestinationPoint.position, PREVIEW_STEPS);
        newObject.PlayerSucceeded += SucceedObstacle;
        newObject.PlayerFailed += RestartGame;
        obstacleObjList.Add(newObject);

        // pass in the Fail() and SucceedObstacle() functions like callbacks (???)
    }

    private void RestartGame()
    {
        Debug.Log("IMPLEMENT ENDGAME / RESTART GAME");
    }

    private void SucceedObstacle(ObstacleObject obstacleObj)
    {
        obstacleObjList.Remove(obstacleObj);
        UnityUtil.DestroyWithChildren(obstacleObj.transform);

        if (obstacleObjList.Count == 0 && obstacleIndex == obstacleDataList.Count) {
            Debug.Log("IMPLEMENT ENDGAME / WIN GAME");
        }
    }
}

