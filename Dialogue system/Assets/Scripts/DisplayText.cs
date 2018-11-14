using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour {

    private string _narrative, _text;
    private float _speed;
    private Text _textOnScreen;
    public Action OnFinishedText, OnReadText;
    public bool outPuttingText;
    
    public void InstantiateVal(string nar, string text, float textSpd)
    {
        outPuttingText = false;
        _narrative = nar;
        this._text = text;
        _speed = textSpd;
        _textOnScreen = gameObject.GetComponent<Text>();
    }
    public void InstantiateVal(string nar, string text, float textSpd, Text attachedTextComponent)
    {
        outPuttingText = false;
        _narrative = nar;
        this._text = text;
        _speed = textSpd;
        _textOnScreen = attachedTextComponent;
    }
    public void OutputText()
    {
        if (!outPuttingText)
        {
            outPuttingText = true;
            StartCoroutine("FloodText");
        }
    }
    public void ClearText()
    {
        _textOnScreen.text = "";
    }

    //Puts out text per character at a certain speed.
    IEnumerator FloodText()
    {
        for(int i = 0; i < _text.Length;i++)
        {
            _textOnScreen.text += _text[i];
            if (OnReadText != null)
            {
                OnReadText();
            }
            yield return new WaitForSeconds(_speed);
        }
        if (OnFinishedText != null)
        {
            OnFinishedText();
            outPuttingText = false;
        }
    }
}
