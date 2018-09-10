using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObstacleObject : MonoBehaviour {
    private ObstacleData selfData;
    private float startSteps, endSteps;
    private Vector3 startPos, endPos;

    public event Action PlayerSucceeded;
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
        //if collider .touches (player) {
        //    do some checks
        //} else
        //{
        //    ignore completely
        //}
    }
}

//// SOME OTHER ObstacleObj.py file


//private void run()
//{

//    if (self.position.x < playerObj.position.x)
//    {
//        Fail("Obstacle passed you");
//    }

//    if (thingWeMadeEarlier.enter) {// this could be a callback function? maybe? i'm shit at this sryyy
//        if (thingWeMadeEarlier.text != desiredWord)
//        {
//            Fail("Fought bostacle the wrong way");
//        } else if (currTime != ) // ???? ok this is where things get really squirrely? idk how to deal with time at all
//        {
//            Fail("Too early :O")
//        } else
//        {
//            SucceedObstacle();
//        }
//    }
//}