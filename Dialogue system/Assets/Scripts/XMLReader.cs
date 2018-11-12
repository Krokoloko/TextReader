using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;

public class XMLReader : MonoBehaviour {

    public string path;
    private XmlNode _currentNode;
    private XmlDocument _currentXml;

    public XMLReader(string path)
    {
        this.path = path;
        SetXML(this.path);
    }
    
    public void SetXML(string path)
    {
        //Make sure you make a folder called 'Resources' in the Assets folder and put the xml file in there.
        TextAsset xmlFile = Resources.Load<TextAsset>(path);
        if (xmlFile.text == null)
        {
            Debug.Log("xmlFile doesn\'t contain value, check if path is correct or if the file exists.");
        }
        XmlDocument xmlDocInXML = new XmlDocument();
        xmlDocInXML.LoadXml(xmlFile.text);
        _currentXml = xmlDocInXML;
    }

    public XmlDocument GetXML()
    {
        return _currentXml;
    }

    public XmlNode GetNode(XmlDocument doc, string nodeName)
    {
        return doc.SelectSingleNode(nodeName);
    }
    public XmlNode GetNode(string nodeName)
    {
        return _currentNode.SelectSingleNode(nodeName);
    }
    public XmlNode GetNode(XmlNode node, string nodeName)
    {
        return node.SelectSingleNode(nodeName);
    }
}

