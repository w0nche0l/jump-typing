using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObstacleObject : MonoBehaviour {
    private ObstacleData selfData;
    private float startSteps, endSteps;
    private Vector3 startPos, endPos;

    public event Action<ObstacleObject> PlayerSucceeded;
    public event Action PlayerFailed;
    private TypingInput typingInput;

    public TextMeshPro nameTextMesh;
    public ColliderPropagator bodyCollider;
    public ColliderPropagator frontCollider;

    public void Init(ObstacleData obstacleData, Vector3 startPos, Vector3 endPos, float previewSteps)
    {
        selfData = obstacleData;
        startSteps = selfData.fightSteps - previewSteps;
        endSteps = selfData.fightSteps;
        this.startPos = startPos;
        this.endPos = endPos;
        nameTextMesh.text = obstacleData.word;
        bodyCollider._OnTriggerEnter += (Collider c) => { PlayerFailed?.Invoke(); };
        frontCollider._OnTriggerEnter += RegisterPlayer;
        frontCollider._OnTriggerExit += DeregisterPlayer;
    }

    public void RunnerUpdate (float currSteps) {
        float deltaSteps = (currSteps - startSteps) / (endSteps - startSteps);
        transform.position = (endPos - startPos) * deltaSteps + startPos;
	}

    private void RegisterPlayer(Collider collider)
    {
        if (collider.tag == "Player")
        {
            PlayerObject playerObj = collider.GetComponent<PlayerObject>();
            playerObj.typingInput.OnEnter += OnTypingInput;
        }
    }

    private void DeregisterPlayer(Collider collider)
    {
        if (collider.tag == "Player")
        {
            PlayerObject playerObj = collider.GetComponent<PlayerObject>();
            playerObj.typingInput.OnEnter -= OnTypingInput;
        }
    }


    void OnTypingInput(string textInput)
    {
        if (textInput.ToUpper() == selfData.word.ToUpper())
        {
            PlayerSucceeded?.Invoke(this);
        } 
    }
}

