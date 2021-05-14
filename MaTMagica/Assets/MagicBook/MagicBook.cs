using System;
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
    public GameObject platform;
    public GameObject platformLeaf;
    public GameObject platformFreeze;
    public GameObject platformLeafFreeze;
    public GameObject jumpPlatform;
    public GameObject ball;
    public GameObject heal;

    public int MaxSpellLength;
    public int StartRunesCount = 10;

    private Stack<KeyCode> pressedCodes = new Stack<KeyCode>();
    private Dictionary<KeyCombination, Magika> keyCombinationsToMagik = new Dictionary<KeyCombination, Magika>();
    private readonly Dictionary<Rune, int> RunesCount = new Dictionary<Rune, int>();
    private SimplePlayerController player;
    private readonly Dictionary<KeyCode,Rune> keyCodeToRune = new Dictionary<KeyCode, Rune>()
    {
        [KeyCode.Q] = Rune.Stone,
        [KeyCode.E] = Rune.Leaf,
        [KeyCode.Alpha1] = Rune.Ice,
        [KeyCode.Alpha2] = Rune.Shield,
        [KeyCode.Alpha3] = Rune.Wind,
    };



    void Start()
    {
        player = gameObject.GetComponent<SimplePlayerController>();
        keyCombinationsToMagik[GetComb(KeyCode.Q)] =
            new Magika(ball, 0);
        keyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.Q)] =
            new Magika(ball, 1);
        keyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.Q, KeyCode.Q)] =
            new Magika(ball, 2);

        keyCombinationsToMagik[GetComb(KeyCode.E)] =
            new Magika(heal);

        keyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.E, KeyCode.Alpha2, KeyCode.Alpha3)] =
            new Magika(jumpPlatform);

        keyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.E)] =
            new Magika(platformLeaf);
        keyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.E, KeyCode.Alpha2)] =
            new Magika(platform);
        keyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.E, KeyCode.Alpha2, KeyCode.Alpha1)] =
            new Magika(platformFreeze);
        keyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.E, KeyCode.Alpha1)] =
            new Magika(platformLeafFreeze);

        keyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.E, KeyCode.Q)] =
            new Magika(platformLeaf, 0);
        keyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.E, KeyCode.Alpha2, KeyCode.Q)] =
            new Magika(platform,0);
        keyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.E, KeyCode.Alpha2, KeyCode.Alpha1, KeyCode.Q)] =
            new Magika(platformFreeze, 0);
        keyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.E, KeyCode.Alpha1, KeyCode.Q)] =
            new Magika(platformLeafFreeze,0);


        foreach (Rune rune in Enum.GetValues(typeof(Rune)))
            RunesCount[rune] = StartRunesCount;
        uiUpdater.UpdateRuneCount(RunesCount);
    }

    public void AddRunes(Rune type, int count)
    {
        RunesCount[type] += count;
        uiUpdater.UpdateRuneCount(RunesCount);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.anyKey && pressedCodes.Count < MaxSpellLength && player.isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Q) && RunesCount[keyCodeToRune[KeyCode.Q]] > 0)
            {
                pressedCodes.Push(KeyCode.Q);
                uiUpdater.AddImageOnButton(KeyCode.Q);
            }

            if (Input.GetKeyDown(KeyCode.E) && RunesCount[keyCodeToRune[KeyCode.E]] > 0)
            {
                pressedCodes.Push(KeyCode.E);
                uiUpdater.AddImageOnButton(KeyCode.E);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && RunesCount[keyCodeToRune[KeyCode.Alpha1]] > 0)
            {
                pressedCodes.Push(KeyCode.Alpha1);
                uiUpdater.AddImageOnButton(KeyCode.Alpha1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && RunesCount[keyCodeToRune[KeyCode.Alpha2]] > 0)
            {
                pressedCodes.Push(KeyCode.Alpha2);
                uiUpdater.AddImageOnButton(KeyCode.Alpha2);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && RunesCount[keyCodeToRune[KeyCode.Alpha3]] > 0)
            {
                pressedCodes.Push(KeyCode.Alpha3);
                uiUpdater.AddImageOnButton(KeyCode.Alpha3);
            }
        }

        if (!Input.GetMouseButtonDown(0) || !player.isAlive) return;
        var combinations = new List<KeyCode>();
        uiUpdater.ClearCanvas();
        while (pressedCodes.Count > 0)
        {
            combinations.Add(pressedCodes.Pop());
        }

        combinations.Reverse();
        if (keyCombinationsToMagik.ContainsKey(GetCombFromList(combinations)))
        {
            player.Attack();
            var magika = keyCombinationsToMagik[GetCombFromList(combinations)];
            foreach (var key in combinations)
                RunesCount[keyCodeToRune[key]]--;
            uiUpdater.UpdateRuneCount(RunesCount);
            switch (magika.Obj.name)
            {
                case "Heal":
                {
                    var pos = player.gameObject.transform.position;
                    var rotate = Quaternion.identity;
                    pos.z = 6f;
                    pos.y += 2f; 
                    Instantiate(magika.Obj, pos, rotate); 
                    break;
                    
                }
                case "Ball" :
                {
                    var pos = player.gameObject.transform.position;
                    pos.z = 6f;
                    pos.x += player.Direction * 2f;
                    pos.y += 2f;
                    var rotate = Quaternion.identity;
                    rotate.z += 1f * player.Direction;
                    var spawnedObj = Instantiate(magika.Obj, pos, rotate) as GameObject;
                    var rb = spawnedObj.GetComponent<Rigidbody2D>();
                    var jumpVelocity = new Vector2(player.Direction * 20 * magika.Power, 0);
                    rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
                    break;
                }
                case "Platform":
                case "PlatformFreeze":
                case "PlatformLeafFreeze":
                case "PlatformLeaf":
                case "JumpPlatform":
                {
                    var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    pos.z = 6f;
                    var rotate = Quaternion.identity;
                    if (magika.Power == 0)
                        rotate.z += 1f;
                    Instantiate(magika.Obj, pos, rotate);
                    break;
                }
            }
        }
        pressedCodes.Clear();
    }

    private KeyCombination GetComb(params KeyCode[] comb)
    {
        return new KeyCombination(comb);
    }
    private KeyCombination GetCombFromList(List<KeyCode> comb)
    {
        return new KeyCombination(comb);
    }
}
