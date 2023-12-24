using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaithController : MonoBehaviour
{
    [SerializeField]
    private int faith;
    [SerializeField]
    public string entityStatus { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        faith = 10;
    }

    // Update is called once per frame
    void Update()
    {
        StatusManager();
    }

    private void StatusManager()
    {
        switch (faith)
        {
            case <= 0:
                entityStatus = "Angry";
                break;
            case int n when (n >= 1 && n <= 9):
                entityStatus = "neutral";
                break;
            case int n when (n >= 10 && n <= 19):
                entityStatus = "Passif";
                break;
            case int n when (n >= 20):
                entityStatus = "Happy";
                break;
        }
    }

    public void AddFaith()
    {
        faith++;
        Debug.Log("AddFaith");
    }

    public void LooseFaith()
    {
        faith--;
        Debug.Log("LooseFaith");
    }
}
