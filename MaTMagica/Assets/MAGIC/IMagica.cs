using System.Collections;
using System.Collections.Generic;
using ClearSky;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IMagica : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isFire = true;
    public Tilemap map;
    void Start()
    {
        Destroy(gameObject,5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        var t = other.gameObject.GetComponent<SimplePlayerController>();
        t?.IInFire();
    }

}
