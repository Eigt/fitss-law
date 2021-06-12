using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DrawLine(Vector3 start, Vector3 end, float duration = 0.1f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer line = myLine.GetComponent<LineRenderer>();
        line.sortingLayerName = "Foreground";
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.grey;
        line.endColor = Color.grey;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }
}
