using System.Collections;
using System.Collections.Generic;
using ClearSky;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    //TODO убрать
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
        var t = other.gameObject.GetComponent<SimplePlayerController>();
        if (!(t is null))
            t.isDamaged = true;
        Destroy(gameObject);
    }
}
