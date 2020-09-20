using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoController : MonoBehaviour
{
    const string kStage1 = "STAGE 1: NORMAL";
    const string kStage2 = "STAGE 2: DEVIATION";
    const string kStage3 = "STAGE 3: NORMAL";
    const string kGameOver = "FINISHED";

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text = kStage1;

        if (SettingsManager.Settings.TryGetValue("InfoTextX", out string startX) &&
            SettingsManager.Settings.TryGetValue("InfoTextY", out string startY))
        {
            transform.position = new Vector3(float.Parse(startX), float.Parse(startY), transform.position.z);
        }

        GameObject.FindGameObjectWithTag("GameController").GetComponent<StepsController>().StepAdvanced += StepAdvancedHandler;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<StepsController>().GameOver += GameOverHandler;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Event handler for step advance
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void StepAdvancedHandler(object sender, EventArgs e)
    {
        if (this.GetComponent<Text>().text == kStage1)
        {
            this.GetComponent<Text>().text = kStage2;
        }
        else
        {
            this.GetComponent<Text>().text = kStage3;
        }
    }

    /// <summary>
    /// Event handler for game end
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void GameOverHandler(object sender, EventArgs e)
    {
        this.GetComponent<Text>().text = kGameOver;
    }
}
