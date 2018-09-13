using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextFlasher : MonoBehaviour {

    const float FONT_SCALE = 1.15f;
    const float DURATION = 0.7f;

	// Use this for initialization
	void Start () {
        TextMeshProUGUI flashingText = GetComponent<TextMeshProUGUI>();
        DOTween.To(() => flashingText.fontSize, (float x) => { flashingText.fontSize = x; }, 
            flashingText.fontSize * FONT_SCALE, DURATION).SetLoops(-1, LoopType.Yoyo);
	}
}
