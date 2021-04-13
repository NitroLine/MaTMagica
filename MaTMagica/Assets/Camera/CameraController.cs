using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D Player;
    public float SmoothSpeed = 1.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPos = Player.transform.position;
        var target = new Vector3((playerPos.x + mousePos.x)/2, (playerPos.y + mousePos.y) / 2, -10);
        var curPosition = Vector3.Lerp(transform.position, target, SmoothSpeed * Time.deltaTime);
        transform.position = curPosition;
    }
}
