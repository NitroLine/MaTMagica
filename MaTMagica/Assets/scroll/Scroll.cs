using System.Collections;
using System.Collections.Generic;
using Assets.MAGIC;
using ClearSky;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    // Start is called before the first frame update
    private bool floating = true;
    private Vector3 startPos;
    [SerializeField]
    public float FloatSpeed = 0.005f;
    [SerializeField]
    public Rune RuneType;
    [SerializeField]
    public int RuneCount = 0;
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (floating)
            transform.position += Vector3.up * FloatSpeed;
        else
            transform.position += Vector3.down * FloatSpeed;
        if ((transform.position - startPos).y > 0.5)
            floating = false;
        else if ((transform.position - startPos).y < -0.5)
            floating = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var spellBook = other.gameObject.GetComponent<MagicBook>();
        spellBook?.AddRunes(RuneType,RuneCount);
        if (spellBook != null)
            Destroy(gameObject);
    }
}
