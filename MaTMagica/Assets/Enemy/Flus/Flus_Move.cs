using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flus_Move : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 target;
    public float acceleration = 5f;

    void Start()
    {
        target = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, target, acceleration*Time.deltaTime);
    }
}
