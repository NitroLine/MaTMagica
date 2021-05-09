using System.Collections;
using System.Collections.Generic;
using ClearSky;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EndLevel()
    {
        Debug.Log("END LEVEL!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<SimplePlayerController>();
        if (player == null) Destroy(other.gameObject);
        EndLevel();
    }
}
