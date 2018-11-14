using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class KeyInputManager : MonoBehaviour {

    public enum keyInputType {OnClick,OnHold,OnRelease};
    public KeyCode[] keyCodes;
    public string[] keyFunctionNames;
    public Action[] OnClick;
    public Action[] OnHold;
    public Action[] OnRelease;

    void CheckInput()
    {
        for (int i = 0; i < keyCodes.Length;i++)
        {
            if (Input.GetKey(keyCodes[i]))
            {
                try
                {
                    OnHold[i]();
                }catch (NullReferenceException){
                    Debug.Log(keyFunctionNames[i] + " doesn't have a OnHold function");
                }
            }
            if (Input.GetKeyDown(keyCodes[i]))
            {
                try
                {
                    OnClick[i]();
                }catch (NullReferenceException)
                {
                    Debug.Log(keyFunctionNames[i] + " doesn't have a OnClick function");
                }
            }
            if (Input.GetKeyUp(keyCodes[i]))
            {
                try
                {
                    OnRelease[i]();
                }
                catch (NullReferenceException)
                {
                    Debug.Log(keyFunctionNames[i] + " doesn't have a OnRelease function");
                }
            }
        }
    }

    public void AddDelicate(Action action,string assignName,keyInputType type)
    {
        switch (type)
        {
            case keyInputType.OnClick:
                OnClick[Array.IndexOf(keyFunctionNames,assignName)] += action;
                break;
            case keyInputType.OnHold:
                OnHold[Array.IndexOf(keyFunctionNames, assignName)] += action;
                break;
            case keyInputType.OnRelease:
                OnRelease[Array.IndexOf(keyFunctionNames, assignName)] += action;
                break;
        }
    }

    void Start()
    {
        OnClick = new Action[keyCodes.Length];
        OnHold = new Action[keyCodes.Length];
        OnRelease = new Action[keyCodes.Length];
        if (OnClick.Length != keyFunctionNames.Length)
        {
            Debug.Log("Unassigned function names are present.");
        }
    }

    void Update () {
        CheckInput();
	}
}
