using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using ClearSky;
using UnityEngine;

public class Ray : MonoBehaviour
{
    //TODO: удалить
    // Start is called before the first frame update
    public SimplePlayerController startPoint;
    private LineRenderer line;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        Destroy(gameObject,5f);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(StartPoint.transform.Find("Skeletal/15 Staff"));
        var start = startPoint.transform.Find("Skeletal/15 Staff").transform.position;
        var end =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        start.z = 0;
        end.z = 0;
        line.SetPositions(new Vector3[]{start,end});
    }
}
