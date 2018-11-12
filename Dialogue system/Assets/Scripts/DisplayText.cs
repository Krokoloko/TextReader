using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour {

    private string _narrative, _text;
    private float _speed;
    private Text _textOnScreen;
    public Action OnFinishedText,OnReadingText;
    
    public void InstantiateVal(string nar, string text, float textSpd)
    {
        _narrative = nar;
        this._text = text;
        _speed = textSpd;
        _textOnScreen = gameObject.GetComponent<Text>();
    }
    public void InstantiateVal(string nar, string text, float textSpd, Text attachedTextComponent)
    {
        _narrative = nar;
        this._text = text;
        _speed = textSpd;
        _textOnScreen = attachedTextComponent;
    }
    private void Start()
    {
        StartCoroutine("FloodText");
    }
    //Puts out text per character at a certain speed.
    IEnumerator FloodText()
    {
        for(int i = 0; i < _text.Length;i++)
        {
            _textOnScreen.text += _text[i];
            yield return new WaitForSeconds(_speed);
        }
    }
}
