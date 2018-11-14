using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class DialogueTransmitter : MonoBehaviour {

    private XMLReader _xml;
    [Tooltip("Root is \'Assets\\Resources\\\', also no extension needed such as .xml or .txt")]
    public string path;
    public Action OnFinishedDialogue;
    
    private int _part;
    private string _text;
    private float _speed;
    private bool started;


    [Tooltip("Will set to it's attached gameobject if not defined.")]
    public GameObject textObject;
    private void Start ()
    {
        started = true;
        if (textObject == null)textObject = gameObject;
        _xml = new XMLReader(path);

        Debug.Log(_xml.GetNode(_xml.GetXML(), "root/dialogue/text").InnerText);

        if (float.TryParse(_xml.GetNode(_xml.GetXML(), "root/dialogue/textSpeed").InnerText, NumberStyles.AllowDecimalPoint,CultureInfo.InvariantCulture, out speed)) { }
        else
        {
            if (_xml.GetNode(_xml.GetXML(), "root/dialogue/textSpeed").InnerText == null)
            {
                Debug.Log("Path doesn\'t exist.");
            }
            Debug.Log("Can't be changed to float");
            _speed = 0.5f;
        }

        _text = ReturnPiece(_xml.GetNode(_xml.GetXML(), "root/dialogue/text").InnerText,0, _xml.GetNode(_xml.GetXML(), "root/dialogue/text").InnerText.IndexOf("(\\)"));

        textObject.AddComponent<DisplayText>();
    }

    private void CreateNewDialogue()
    {
        if (!textObject.GetComponent<DisplayText>().outPuttingText)
        {
            textObject.GetComponent<DisplayText>().ClearText();

            if (!started)
            {
                _text = _xml.GetNode(_xml.GetXML(), "root/dialogue/text").InnerText.Substring(_xml.GetNode(_xml.GetXML(), "root/dialogue/text").InnerText.IndexOf("(\\)") + 3);
            }

            _text = ReturnPiece(_xml.GetNode(_xml.GetXML(), "root/dialogue/text").InnerText, 0, _xml.GetNode(_xml.GetXML(), "root/dialogue/text").InnerText.IndexOf("(\\)"));

            textObject.GetComponent<DisplayText>().InstantiateVal(_xml.GetNode(_xml.GetXML(), "root/dialogue/narrative").InnerText,
                                         _text,
                                         _speed, textObject.GetComponent<Text>());

            textObject.GetComponent<DisplayText>().OutputText();

            if (_text == ReturnPiece(_xml.GetNode(_xml.GetXML(), "root/dialogue/text").InnerText, 0, _xml.GetNode(_xml.GetXML(), "root/dialogue/text").InnerText.IndexOf("(\\)")))
            {

            }
            else
            {
                textObject.GetComponent<DisplayText>().OnFinishedText += CreateNewDialogue;
            }

            Debug.Log(_text);
        }
    }

    private string ReturnPiece(string text,int start,int length)
    {
        try
        {
            //Returns the string that was before the seperator.
            return text.Substring(0, text.IndexOf("(\\)"));
        }
        catch (System.ArgumentOutOfRangeException)
        {
            //Will return itself if the seperator is not found in the string.
            return text;
        }
    }
    private string ReturnPiece(string text,string seperator, int start, int length)
    {
        try
        {
            //Returns the string that was before the seperator.
            return text.Substring(0, text.IndexOf(seperator));
        }
        catch (System.ArgumentOutOfRangeException)
        {
            //Will return itself if the seperator is not found in the string.
            return text;
        }
    }
}
