using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class StepsController : MonoBehaviour
{
    public float m_step1Clicks = 10;
    public float m_step2Clicks = 20;
    public float m_step3Clicks = 10;

    private int m_clickCount;
    private int m_currentStep = 1;

    public EventHandler StepAdvanced;
    public EventHandler GameOver;

    // Start is called before the first frame update
    void Start()
    {
        string value;
        if (SettingsManager.Settings.TryGetValue("Step1Clicks", out value))
        {
            m_step1Clicks = int.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
        }
        if (SettingsManager.Settings.TryGetValue("Step2Clicks", out value))
        {
            m_step2Clicks = int.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
        }
        if (SettingsManager.Settings.TryGetValue("Step3Clicks", out value))
        {
            m_step3Clicks = int.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
        }

        GameObject.FindGameObjectWithTag("Cursor").GetComponent<CursorController>().CursorClicked += CursorClickedHandler;
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
    public void CursorClickedHandler(object sender, EventArgs e)
    {
        // advance the number of clicks and possibly the step
        m_clickCount++;
        bool m_stepAdvanced = false;
        if (m_currentStep == 1 && m_clickCount == m_step1Clicks)
        {
            m_currentStep++;
            m_clickCount = 0;
            m_stepAdvanced = true;
        }
        if (m_currentStep == 2 && m_clickCount == m_step2Clicks)
        {
            m_currentStep++;
            m_clickCount = 0;
            m_stepAdvanced = true;
        }
        if (m_currentStep == 3 && m_clickCount == m_step3Clicks)
        {
            // send end event
            GameOver?.Invoke(null, null);
            Destroy(this);
        }

        if (m_stepAdvanced)
        {
            StepAdvanced?.Invoke(this, null);
        }
    }
}
