using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public List<List<Vector2>> Distances;

    // Start is called before the first frame update
    void Start()
    {
        if (SettingsManager.Settings.TryGetValue("TargetX", out string startX) &&
            SettingsManager.Settings.TryGetValue("TargetY", out string startY))
        {
            transform.position = new Vector3(float.Parse(startX), float.Parse(startY), transform.position.z);
        }

        GameObject.FindGameObjectWithTag("Cursor").GetComponent<CursorController>().CursorClicked += CursorClickedHandler;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<StepsController>().StepAdvanced += StepAdvancedHandler;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<StepsController>().GameOver += GameOverHandler;

        Distances = new List<List<Vector2>>() { new List<Vector2>() };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Event handler for cursor click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void CursorClickedHandler(object sender,  EventArgs e)
    {
        Vector3 clickPos = (sender as GameObject).transform.position;
        Vector2 clickDifference = new Vector2(transform.position.x - clickPos.x, transform.position.y - clickPos.y);
        Distances.LastOrDefault().Add(clickDifference);

        Debug.Log($"X difference = {clickDifference.x}, Y difference = {clickDifference.y}");
    }

    /// <summary>
    /// Event handler for step advance
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void StepAdvancedHandler(object sender, EventArgs e)
    {
        Distances.Add(new List<Vector2>());
    }

    /// <summary>
    /// Event handler for game end
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void GameOverHandler(object sender, EventArgs e)
    {
        GameObject.FindGameObjectWithTag("Cursor").GetComponent<CursorController>().CursorClicked -= CursorClickedHandler;

        string outputFile = System.IO.Path.Combine(Application.dataPath, "output.txt");
        using (StreamWriter sw = new StreamWriter(outputFile, false))
        {
            for (int i = 0; i < Distances.Count; i++)
            {
                sw.WriteLine($"Step {i + 1}");
                sw.WriteLine("Diff X\tDiff Y");
                foreach (Vector2 v in Distances[i])
                {
                    sw.WriteLine($"{v.x.ToString("N2")}\t{v.y.ToString("N2")}");
                }
                sw.WriteLine();
            }
        }
        System.Diagnostics.Process.Start(outputFile);
        Application.Quit();
    }
}
