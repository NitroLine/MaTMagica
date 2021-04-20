using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Assets.MAGIC;
using ClearSky;
using UnityEngine;

public class MagicBook : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject magic;
    public GameObject ball;
    public GameObject fireBall;
    private Stack<KeyCode> pressedCodes = new Stack<KeyCode>();
    public int MaxSpellLength;
    public Dictionary<string, GameObject> book = new Dictionary<string, GameObject>();
    private SimplePlayerController player;
    void Start()
    {
        player = gameObject.GetComponent<SimplePlayerController>();
        book["Q"] = magic;
        book["E"] = ball;
        book["QQ"] = magic;
        book["EE"] = ball;
        book["QE"] = fireBall;
        book["EQ"] = fireBall;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && pressedCodes.Count < MaxSpellLength)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                pressedCodes.Push(KeyCode.Q);
            if (Input.GetKeyDown(KeyCode.E))
                pressedCodes.Push(KeyCode.E);
        }

        if (Input.GetMouseButtonDown(0))
        {
            var combinations = new StringBuilder();
            while (pressedCodes.Count > 0)
            {
                combinations.Append(pressedCodes.Pop().ToString());
            }
            if (book.ContainsKey(combinations.ToString()))
            {
                player.Attack();
                var obj = book[combinations.ToString()];
                switch (obj.name)
                {
                    case "FireBall":
                    case "Ball" :
                    {
                        var pos = player.gameObject.transform.position;
                        pos.z = 6f;
                        pos.x += player.Direction * 2f;
                        pos.y += 2f;
                        var rotate = Quaternion.identity;
                        rotate.z += 90f;
                        var spawnedObj = Instantiate(obj, pos, rotate) as GameObject;
                        var rb = spawnedObj.GetComponent<Rigidbody2D>();
                        var jumpVelocity = new Vector2(player.Direction * 20, 0);
                        rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
                        break;
                    }
                    case "Magic":
                    {
                        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        pos.z = 6f;
                        var rotate = Quaternion.identity;
                        rotate.z += 90f;
                        Instantiate(obj, pos, rotate);
                        break;
                    }
                }

            }
            pressedCodes.Clear();
        }
        
    }
}
