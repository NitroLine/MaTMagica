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
        transform.position = transform.position + (target - transform.position ).normalized * acceleration * Time.deltaTime;
    }

}
