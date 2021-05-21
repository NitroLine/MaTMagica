using System.Collections;
using System.Collections.Generic;
using ClearSky;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.MAGIC;

public class Fire : MonoBehaviour
{
    //TODO удалить
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var t = other.gameObject.GetComponent<SimplePlayerController>();
        if (!(t is null))
            t.isInFire = true;
    }
}
