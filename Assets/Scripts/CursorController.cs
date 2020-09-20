using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    public GameObject m_cursor { get; private set; }
    public bool m_isCursorVisible = false;
    public bool m_isDeviationActive = false;
    public float m_sensitivityX = 0.5f;
    public float m_sensitivityY = 0.5f;
    public float m_deviation = 0.3f;
    public string m_deviationDir = "Up";

    private bool m_isReturnToStart = false;
    private GameObject m_respawnObj;

    public EventHandler CursorClicked;
    public EventHandler Completed;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = m_isCursorVisible;

        string value;
        if (SettingsManager.Settings.TryGetValue("SensitivityX", out value))
        {
            m_sensitivityX = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
        }
        if (SettingsManager.Settings.TryGetValue("SensitivityY", out value))
        {
            m_sensitivityY = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
        }
        if (SettingsManager.Settings.TryGetValue("Deviation", out value))
        {
            m_deviation = float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
        }
        if (SettingsManager.Settings.TryGetValue("DeviationDir", out value))
        {
            m_deviationDir = value;
        }

        // place the start area and position the cursor within
        m_respawnObj = GameObject.FindGameObjectWithTag("Respawn");
        if (SettingsManager.Settings.TryGetValue("StartX", out string startX) &&
            SettingsManager.Settings.TryGetValue("StartY", out string startY))
        {
            m_respawnObj.transform.position = new Vector3(float.Parse(startX), float.Parse(startY), m_respawnObj.transform.position.z);
        }

        transform.position = m_respawnObj.transform.position;

        GameObject.FindGameObjectWithTag("GameController").GetComponent<StepsController>().StepAdvanced += StepAdvancedHandler;
        //GameObject.FindGameObjectWithTag("GameController").GetComponent<StepsController>().GameOver += GameOverHandler;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * m_sensitivityX;
        float moveY = Input.GetAxis("Mouse Y") * m_sensitivityY;

        if (m_isDeviationActive)
        {
            switch (m_deviationDir)
            {
                case "Left":
                    transform.Translate(moveX - moveY * m_deviation, moveY, 0);
                    break;
                case "Right":
                    transform.Translate(moveX + moveY * m_deviation, moveY, 0);
                    break;
                case "Down":
                    transform.Translate(moveX, moveY - moveX * m_deviation, 0);
                    break;
                default: // Up
                    transform.Translate(moveX, moveY + moveX * m_deviation, 0);
                    break;
            }
        }
        else
        {
            transform.Translate(moveX, moveY, 0);
        }

        // left click
        if (m_isReturnToStart == false &&
            Input.GetMouseButtonDown(0))
        {
            Debug.Log($"X = {transform.position.x}, Y = {transform.position.y}");

            // send position in click event
            CursorClicked?.Invoke(this.gameObject, null);

            // must return to start now
            m_isReturnToStart = true;
            GameObject.FindGameObjectWithTag("StartText").GetComponent<Text>().text = "RETURN";

            //removed: reset position of cursor
            //transform.position = m_respawnObj.transform.position;
        }
        else if (m_isReturnToStart == true)
        {
            float bound = m_respawnObj.GetComponent<SpriteRenderer>().bounds.size.x;

            Vector2 respawnPos = new Vector2(m_respawnObj.transform.position.x, m_respawnObj.transform.position.y);
            Vector2 cursorPos = new Vector2(this.transform.position.x, this.transform.position.y);
            // if close enough
            float distance = Vector2.Distance(respawnPos, cursorPos);
            if (distance <= bound)
            {
                m_isReturnToStart = false;
                GameObject.FindGameObjectWithTag("StartText").GetComponent<Text>().text = "GO";
            }
        }
    }

    /// <summary>
    /// Event handler for step advance
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void StepAdvancedHandler(object sender, EventArgs e)
    {
        m_isDeviationActive = !m_isDeviationActive;
    }
}
