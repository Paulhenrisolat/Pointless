using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptLocator : MonoBehaviour
{
    [SerializeField]
    private List<MonoBehaviour> scriptList = new List<MonoBehaviour>();

    void Start()
    {
        MonoBehaviour[] scriptsOnGameObject = GetComponents<MonoBehaviour>();
        scriptList.AddRange(scriptsOnGameObject);
    }
}
