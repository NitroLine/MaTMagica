using System.Collections;
using System.Collections.Generic;
using ClearSky;
using UnityEngine;

public class Flus_View : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<SimplePlayerController>();
        if (player == null) return;
        gameObject.GetComponentInParent<Flus_Move>().target = player.transform.position;
    }
}
