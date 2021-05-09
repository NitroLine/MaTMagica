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
    public UI_updater uiUpdater;
    public GameObject fire;
    public GameObject ball;
    public GameObject fireBall;
    public GameObject ice;
    private Stack<KeyCode> pressedCodes = new Stack<KeyCode>();
    public int MaxSpellLength;
    public Dictionary<string, Magika> book = new Dictionary<string, Magika>();
    private SimplePlayerController player;
    private Canvas ui;
    void Start()
    {
        ui = FindObjectOfType<Canvas>();
        player = gameObject.GetComponent<SimplePlayerController>();
        book["Q"] = new Magika(fire);
        book["Alpha1"] = new Magika(ice);
        book["E"] = new Magika(ball);
        book["QQ"] = new Magika(fire,2); 
        book["EE"] = new Magika(ball, 2);
        book["Alpha1Alpha1"] = new Magika(ice, 2);
        book["QE"] = new Magika(fireBall);
        book["EQ"] = new Magika(fireBall);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.anyKey && pressedCodes.Count < MaxSpellLength && player.isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                pressedCodes.Push(KeyCode.Q);
                uiUpdater.AddImageOnButton(KeyCode.Q);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                pressedCodes.Push(KeyCode.E);
                uiUpdater.AddImageOnButton(KeyCode.E);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                pressedCodes.Push(KeyCode.Alpha1);
                uiUpdater.AddImageOnButton(KeyCode.Alpha1);
            }
        }

        if (Input.GetMouseButtonDown(0) && player.isAlive)
        {
            var combinations = new StringBuilder();
            uiUpdater.ClearCanvas();
            while (pressedCodes.Count > 0)
            {
                combinations.Append(pressedCodes.Pop().ToString());
            }
            Debug.Log(combinations.ToString());
            if (book.ContainsKey(combinations.ToString()))
            {
                player.Attack();
                var magika = book[combinations.ToString()];
                Debug.Log(magika.Obj.name);
                switch (magika.Obj.name)
                {
                    case "FireBall":
                    case "Ball" :
                    {
                        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        pos.z = 6f;
                        var rotate = Quaternion.identity;
                        rotate.z += 90f;
                        var spawnedObj = Instantiate(magika.Obj, pos, rotate) as GameObject;
                        var rb = spawnedObj.GetComponent<Rigidbody2D>();
                        var jumpVelocity = new Vector2(player.Direction * 20 * magika.Power, 0);
                        rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
                        break;
                    }
                    case "Ice":
                    case "Fire":
                    {
                        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        pos.z = 6f;
                        var rotate = Quaternion.identity;
                        rotate.z += 90f;
                        Instantiate(magika.Obj, pos, rotate);
                        break;
                    }
                }

            }
            pressedCodes.Clear();
        }
        
    }
}
