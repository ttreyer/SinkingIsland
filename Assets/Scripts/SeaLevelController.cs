using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeaLevelController : MonoBehaviour
{
    public int currentPollutionLevel;
    public int currentWaterHeight;
    public GameObject island1;
    public GameObject island2;
    public int[] pollutionLimitPerLevels;
    public Text pollutionDisplay;

    private ResourceController island1ResourceControllerScript;
    private ResourceController island2ResourceControllerScript;
    private WaterLevelController p1WaterLevel, p2WaterLevel;
    private MusicController musicController;

    // Start is called before the first frame update
    void Start()
    {
        island1ResourceControllerScript = island1.GetComponent<ResourceController>();
        island2ResourceControllerScript = island2.GetComponent<ResourceController>();
        p1WaterLevel = island1.GetComponent<WaterLevelController>();
        p2WaterLevel = island2.GetComponent<WaterLevelController>();
        musicController = GetComponent<MusicController>();
    }

    public void UpdateSeaLevel() {
        DrawPollutionFromIslands();
        UpdatePollutionGUI();

        if (currentPollutionLevel >= pollutionLimitPerLevels[currentWaterHeight]) {
            currentWaterHeight++;

            p1WaterLevel.RaiseWaterLevel();
            p2WaterLevel.RaiseWaterLevel();

            musicController.PlayLevel(currentWaterHeight);

            if (currentWaterHeight >= pollutionLimitPerLevels.Length)
                GetComponent<TurnController>().GameOver("You drown!");
        }
    }

    private void DrawPollutionFromIslands()
    {
        currentPollutionLevel += island1ResourceControllerScript.current.polution + island2ResourceControllerScript.current.polution;
        if(currentPollutionLevel < 0)
        {
            currentPollutionLevel = 0;
        }
    }

    public void UpdatePollutionGUI()
    {
        pollutionDisplay.text = "Pollution Level " + currentPollutionLevel + "/" + pollutionLimitPerLevels[pollutionLimitPerLevels.Length - 1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
