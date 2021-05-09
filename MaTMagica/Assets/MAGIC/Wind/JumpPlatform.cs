using ClearSky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<SimplePlayerController>();
        if (player == null) return;
        player.jumpPower *= 2;
        player.JumpMe();
        player.jumpPower /= 2;
    }
}
