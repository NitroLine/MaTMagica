using System.Collections;
using System.Collections.Generic;
using ClearSky;
using UnityEngine;

public class DeadTrig : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<SimplePlayerController>();
        if (player is null)
            Destroy(other.gameObject);
        else
        {
            player.gameObject.transform.position = Vector3.zero;
            player.Health -= 10;
        }
    }
}
