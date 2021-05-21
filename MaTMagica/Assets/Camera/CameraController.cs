using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D player;
    public float smoothSpeed = 1.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var playerPos = player.transform.position;
        var target = new Vector3(
            (playerPos.x + mousePos.x) / 2, 
            (playerPos.y + mousePos.y) / 2, 
            -10);
        var curPosition = Vector3.Lerp(
            transform.position, 
            target, 
            smoothSpeed * Time.deltaTime);
        transform.position = curPosition;
    }
}
