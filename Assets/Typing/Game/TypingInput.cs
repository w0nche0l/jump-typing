using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class TypingInput : MonoBehaviour {

    public TMP_InputField inputField;
    public event Action<string> OnEnter; 
    
    // for text highlighting
    public event Action<string> OnKeyHit;

    // Use this for initialization
    void Start () {
        Focus();
	}

    void Focus()
    {
        if (!inputField.isFocused)
        {
            inputField.text = "";
            EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
            inputField.OnPointerClick(new PointerEventData(EventSystem.current));
        }
    }

    // for text highlighting
    public void OnType()
    {
        OnKeyHit?.Invoke(inputField.text);
    }

    public void OnEndEdit()
    {
        OnEnter?.Invoke(inputField.text);
        Focus();
    }
}
