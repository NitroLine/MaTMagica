using System.Collections;
using System.Collections.Generic;
using ClearSky;
using UnityEditor.UIElements;
using UnityEngine;

public class Sus_Move : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 target;
    public float acceleration = 50f;
    
    void Start()
    {
        target = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var x = (target - transform.position).normalized;
        transform.position += x * acceleration * Time.deltaTime;
    }

}
