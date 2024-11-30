using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    bool _isTesting;

    GameObject _onScreenControllers;

    void Start()
    {
        _onScreenControllers = GameObject.Find("OnScreenControllers");

        if(!_isTesting)
        {
            _onScreenControllers.SetActive(Application.platform != RuntimePlatform.WindowsEditor
                                        && Application.platform != RuntimePlatform.WindowsPlayer);
        }
    }
}
