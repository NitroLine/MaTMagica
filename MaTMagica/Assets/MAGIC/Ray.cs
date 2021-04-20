using System.Collections;
using System.Collections.Generic;
using ClearSky;
using UnityEngine;

public class Ray : MonoBehaviour
{
    // Start is called before the first frame update
    public SimplePlayerController StartPoint;
    private LineRenderer line;
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var start = StartPoint.gameObject.transform.position;
        var end =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        start.z = 0;
        end.z = 0;
        line.SetPositions(new Vector3[]{start,end});
    }
}
