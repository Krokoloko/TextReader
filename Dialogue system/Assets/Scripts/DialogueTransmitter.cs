using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class DialogueTransmitter : MonoBehaviour {

    private XMLReader _xml;
    [Tooltip("Root is \'Assets\\Resources\\\', also no extension needed such as .xml or .txt")]
    public string path;
    [Tooltip("Will set to it's attached gameobject if not defined.")]
    public GameObject textObject;
    private void Start ()
    {
        if (textObject == null)textObject = gameObject;
        _xml = new XMLReader(path);
        float speed;
        Debug.Log(_xml.GetNode(_xml.GetXML(), "root/dialogue/text").InnerText);
        //Debug.Log(_xml.GetNode(_xml.GetXML(),"root").SelectSingleNode("dialogue").SelectSingleNode("textSpeed").Value);

        if (float.TryParse(_xml.GetNode(_xml.GetXML(), "root/dialogue/textSpeed").InnerText, NumberStyles.AllowDecimalPoint,CultureInfo.InvariantCulture, out speed)) { }
        else
        {
            if (_xml.GetNode(_xml.GetXML(), "root/dialogue/textSpeed").InnerText == null)
            {
                Debug.Log("Path doesn\'t exist.");
            }
            Debug.Log("Can't be changed to float");
            speed = 1f;
        }

        textObject.AddComponent<DisplayText>();

        textObject.GetComponent<DisplayText>().InstantiateVal(_xml.GetNode(_xml.GetXML(), "root/dialogue/narrative").InnerText,
                                     _xml.GetNode(_xml.GetXML(), "root/dialogue/text").InnerText,
                                     speed,textObject.GetComponent<Text>());
    }
}
