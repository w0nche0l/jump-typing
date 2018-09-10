using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class TypingInput : MonoBehaviour {

    public TMP_InputField inputField;
    public event Action<string> OnEnter;

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

    public void OnEndEdit()
    {
        Debug.Log("ENTERED: " + inputField.text);
        OnEnter?.Invoke(inputField.text);
        Focus();
    }
}
