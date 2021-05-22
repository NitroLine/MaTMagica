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
    public MagicHelper uiHelp;
    public GameObject platform;
    public GameObject platformLeaf;
    public GameObject platformFreeze;
    public GameObject platformLeafFreeze;
    public GameObject jumpPlatform;
    public GameObject ball;
    public GameObject heal;

    public int MaxSpellLength;
    public int StartRunesCount = 10;

    public readonly List<KeyCode> pressedCodes = new List<KeyCode>();
    public readonly Dictionary<KeyCombination, Magika> KeyCombinationsToMagik = 
        new Dictionary<KeyCombination, Magika>();
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
        KeyCombinationsToMagik[GetComb(KeyCode.Q)] =
            new Magika(ball, "Камень",0);
        KeyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.Q)] =
            new Magika(ball,  "Ускореный камень",1);
        KeyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.Q, KeyCode.Q)] =
            new Magika(ball, "Сильно ускореный камень", 2);

        KeyCombinationsToMagik[GetComb(KeyCode.E)] =
            new Magika(heal, "Лечение");

        KeyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.E, KeyCode.Alpha2, KeyCode.Alpha3)] =
            new Magika(jumpPlatform, "Прыгательная платформа");

        KeyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.E)] =
            new Magika(platformLeaf, "Экологичная платформа");
        KeyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.E, KeyCode.Alpha2)] =
            new Magika(platform, "Платформа");
        KeyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.E, KeyCode.Alpha2, KeyCode.Alpha1)] =
            new Magika(platformFreeze, "Парящая платформа");
        KeyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.E, KeyCode.Alpha1)] =
            new Magika(platformLeafFreeze, " Экологично-Парящая платформа");

        KeyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.E, KeyCode.Q)] =
            new Magika(platformLeaf, "Экологичная cтена", 0);
        KeyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.E, KeyCode.Alpha2, KeyCode.Q)] =
            new Magika(platform,"Стена",0);
        KeyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.E, KeyCode.Alpha2, KeyCode.Alpha1, KeyCode.Q)] =
            new Magika(platformFreeze,"Замороженная стена", 0);
        KeyCombinationsToMagik[GetComb(KeyCode.Q, KeyCode.E, KeyCode.Alpha1, KeyCode.Q)] =
            new Magika(platformLeafFreeze,"Экологично Замороженная стена",0);
        
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
        if (Time.timeScale == 0) return;
        if (Input.anyKey && pressedCodes.Count < MaxSpellLength && player.isAlive)
        {
            
            if (Input.GetKeyDown(KeyCode.Q) 
                && RunesCount[keyCodeToRune[KeyCode.Q]] > 0)
            {
                pressedCodes.Add(KeyCode.Q);
                uiUpdater.AddImageOnButton(KeyCode.Q);
                uiHelp.UpdateHelp();
            }

            if (Input.GetKeyDown(KeyCode.E) 
                && RunesCount[keyCodeToRune[KeyCode.E]] > 0)
            {
                pressedCodes.Add(KeyCode.E);
                uiUpdater.AddImageOnButton(KeyCode.E);
                uiHelp.UpdateHelp();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) 
                && RunesCount[keyCodeToRune[KeyCode.Alpha1]] > 0)
            {
                pressedCodes.Add(KeyCode.Alpha1);
                uiUpdater.AddImageOnButton(KeyCode.Alpha1);
                uiHelp.UpdateHelp();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) 
                && RunesCount[keyCodeToRune[KeyCode.Alpha2]] > 0)
            {
                pressedCodes.Add(KeyCode.Alpha2);
                uiUpdater.AddImageOnButton(KeyCode.Alpha2);
                uiHelp.UpdateHelp();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) 
                && RunesCount[keyCodeToRune[KeyCode.Alpha3]] > 0)
            {
                pressedCodes.Add(KeyCode.Alpha3);
                uiUpdater.AddImageOnButton(KeyCode.Alpha3);
                uiHelp.UpdateHelp();
            }
        }

        if (!Input.GetMouseButtonDown(0) || !player.isAlive) return;
        
        uiUpdater.ClearCanvas();
        if (KeyCombinationsToMagik.ContainsKey(GetCombFromList(pressedCodes)))
        {
            player.Attack();
            var magika = KeyCombinationsToMagik[GetCombFromList(pressedCodes)];
            foreach (var key in pressedCodes)
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
                    pos.z = -1f;
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
                    pos.z = -1f;
                    var rotate = Quaternion.identity;
                    if (magika.Power == 0)
                        rotate.z += 1f;
                    Instantiate(magika.Obj, pos, rotate);
                    break;
                }
            }
        }
        pressedCodes.Clear();
        uiHelp.UpdateHelp();
    }

    public static KeyCombination GetComb(params KeyCode[] comb)
    {
        return new KeyCombination(comb);
    }
    private static KeyCombination GetCombFromList(List<KeyCode> comb)
    {
        return new KeyCombination(comb);
    }
}
