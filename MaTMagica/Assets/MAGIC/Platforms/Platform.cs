using System.Collections;
using System.Collections.Generic;
using ClearSky;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isFreeze;
    public bool isLeaf;
    public int maxTouch = 1;
    private int curTouch;
    private bool floating = true;
    private Vector3 startPos;
    [SerializeField]
    public float FloatSpeed = 0.001f;

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFreeze) return;
        if (floating)
            transform.position += Vector3.up * FloatSpeed;
        else
            transform.position += Vector3.down * FloatSpeed;
        if ((transform.position - startPos).y > 0.1)
            floating = false;
        else if ((transform.position - startPos).y < -0.1)
            floating = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<SimplePlayerController>();
        if (!isLeaf || player == null) return;
        if (curTouch < maxTouch)
            curTouch++;
        else
            Destroy(gameObject);
    }
}
