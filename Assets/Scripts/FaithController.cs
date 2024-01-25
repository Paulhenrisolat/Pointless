using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaithController : MonoBehaviour
{
    public string entityStatus { get; private set; }
    public int faith { get; private set; }

    [SerializeField]
    private Material headLight;
    [SerializeField]
    private Light deityLight;

    // Start is called before the first frame update
    void Start()
    {
        faith = 10;
        deityLight.intensity = 217;
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
                deityLight.intensity = 40;
                deityLight.color = Color.red;
                headLight.color = Color.red;
                headLight.SetColor("_EmissionColor", Color.red);
                break;
            case int n when (n >= 1 && n <= 9):
                entityStatus = "Frustrated";
                deityLight.intensity = 50;
                deityLight.color = Color.yellow;
                headLight.color = Color.yellow;
                headLight.SetColor("_EmissionColor", Color.yellow);
                break;
            case int n when (n >= 10 && n <= 19):
                entityStatus = "Neutral";
                deityLight.intensity = 217;
                deityLight.color = Color.cyan;
                headLight.color = Color.cyan;
                headLight.SetColor("_EmissionColor", Color.cyan);
                break;
            case int n when (n >= 20):
                entityStatus = "Happy";
                deityLight.color = Color.green;
                headLight.color = Color.green;
                headLight.SetColor("_EmissionColor", Color.green);
                break;
        }
    }

    public void AddFaith()
    {
        faith++;
        Debug.Log("AddFaith("+faith+")");
    }

    public void LooseFaith()
    {
        if (faith > 0)
        {
            faith-=2;
            Debug.Log("LooseFaith(" + faith + ")");
        }
    }
}
