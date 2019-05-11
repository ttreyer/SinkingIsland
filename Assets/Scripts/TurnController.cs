using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    public GameObject resourceController;
    
    public int currentTurn;
    public int turnLimit;
    public Text turnDisplay;

    private bool gameOver;
    private ResourceController resourceControllerScript;
    
    
    // Start is called before the first frame update
    void Start()
    {
        resourceControllerScript = resourceController.GetComponent<ResourceController>();
        Clicked();
        gameOver = false;
    }

    public void Clicked()
    {
        if ((currentTurn != turnLimit) && (gameOver == false))
        {
            currentTurn += 1;
            turnDisplay.text = "Current Turn: " + currentTurn;
            resourceControllerScript.UpdateResourceCount();
        }

        else if (currentTurn == turnLimit)
        {
            gameOver = true;
            turnDisplay.text = "Turn limit exceeded";
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
