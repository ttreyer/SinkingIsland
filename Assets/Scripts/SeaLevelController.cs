using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaLevelController : MonoBehaviour
{
    public int currentPollutionLevel;
    public int currentWaterHeight;
    public GameObject island1;
    public GameObject island2;
    private ResourceController island1ResourceControllerScript;
    private ResourceController island2ResourceControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        island1ResourceControllerScript = island1.GetComponent<ResourceController>();
        island2ResourceControllerScript = island2.GetComponent<ResourceController>();
    }

    public void DrawPollutionFromIslands()
    {
        currentPollutionLevel = (island1ResourceControllerScript.current.polution + island2ResourceControllerScript.current.polution);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
