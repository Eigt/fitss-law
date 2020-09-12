using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddRelativeClick(Vector3 a_clickPos)
    {
        Debug.Log($"X difference = {transform.position.x - a_clickPos.x}, Y difference = {transform.position.y - a_clickPos.y}");
    }
}
