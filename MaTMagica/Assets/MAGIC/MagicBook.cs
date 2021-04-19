using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class MagicBook : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject agic;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var pos =Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 6f;
            Instantiate(agic, pos, Quaternion.identity);
        }
        
    }
}
