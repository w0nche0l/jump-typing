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

        // for text highlighting
        if (PlayerObject.instance != null)
        {
            PlayerObject.instance.typingInput.OnKeyHit += OnTypingInputKeyHit;
        }
    }

    public void OnDestroy()
    {
        if (PlayerObject.instance != null)
        {
            PlayerObject.instance.typingInput.OnKeyHit -= OnTypingInputKeyHit;
        }
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
            playerObj.typingInput.OnEnter += OnTypingInputEnter;
        }
    }

    private void DeregisterPlayer(Collider collider)
    {
        if (collider.tag == "Player")
        {
            PlayerObject playerObj = collider.GetComponent<PlayerObject>();
            playerObj.typingInput.OnEnter -= OnTypingInputEnter;
        }
    }

    // for text highlighting
    int findHighlightIndex(string textInput)
    {
        for (int i = textInput.Length; i > 0; i--)
        {
            if (i <= selfData.word.Length && 
                textInput.Substring(0, i).ToUpper() == selfData.word.Substring(0, i).ToUpper())
            {
                return i;
            }
        }
        return 0;
    }

    // for text highlighting
    void OnTypingInputKeyHit(string textInput)
    {
        string openTag = "<color=\"orange\">";
        string closeTag = "</color>";
        int i = findHighlightIndex(textInput);
        nameTextMesh.text = openTag + selfData.word.Substring(0, i) + closeTag + selfData.word.Substring(i);
    }

    void OnTypingInputEnter(string textInput)
    {
        if (textInput.ToUpper() == selfData.word.ToUpper())
        {
            PlayerSucceeded?.Invoke(this);
        }
    }
}

