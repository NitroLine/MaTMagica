using System.Collections;
using System.Collections.Generic;
using ClearSky;
using UnityEngine;

public class Freeze : MonoBehaviour
{
    //TODO убрать
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,5f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Inside freeze");
        var t = other.gameObject.GetComponent<SimplePlayerController>();
    }
}

