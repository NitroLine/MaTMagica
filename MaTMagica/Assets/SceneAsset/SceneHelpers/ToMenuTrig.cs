using System.Collections;
using System.Collections.Generic;
using ClearSky;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMenuTrig : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<SimplePlayerController>();
        if (player == null)
            return;
        SceneManager.LoadScene(0);
    }
}
