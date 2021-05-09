using System.Collections;
using System.Collections.Generic;
using ClearSky;
using UnityEngine;

public class Heal : MonoBehaviour
{
    // Start is called before the first frame update
    private SimplePlayerController player;
    void Start()
    {
        player = GameObject.Find("Wizard").GetComponent<SimplePlayerController>();
        player.health += 10;
        Destroy(gameObject, 1);
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0,2);

    }
}
