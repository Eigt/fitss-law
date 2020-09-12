using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public GameObject m_cursor;
    public bool m_isCursorVisible = false;
    public float m_sensitivityX = 0.5f;
    public float m_sensitivityY = 0.5f;
    public float m_deviation = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = m_isCursorVisible;

        transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * m_sensitivityX;
        float moveY = Input.GetAxis("Mouse Y") * m_sensitivityY;

        transform.Translate(moveX, moveY + moveX * m_deviation, 0);

        // left click
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log($"X = {transform.position.x}, Y = {transform.position.y}");

            // send position to target object
            GameObject.FindGameObjectWithTag("Target").GetComponent<TargetController>().AddRelativeClick(transform.position);

            // reset position of cursor
            transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        }
    }
}
