using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHelpInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject movementHelp;
    public GameObject stackHelp;
    public GameObject runeHelp;
    public Diolog changeDialog;
    public Diolog endDialog;

    void Start()
    {
        
    }   

    // Update is called once per frame
    void Update()
    {
        if (endDialog != null && (endDialog.isStarted || endDialog.IsEnded))
        {
            gameObject.GetComponent<Canvas>().enabled = false;
            return;
        }
        if (changeDialog.isStarted)
        {
            gameObject.GetComponent<Canvas>().enabled = false;
            return;
        }
        if (!changeDialog.IsEnded) return;
        gameObject.GetComponent<Canvas>().enabled = true;
        movementHelp.transform.localScale = Vector3.zero;
        runeHelp.transform.localScale = Vector3.one;
        stackHelp.transform.localScale = Vector3.one;


    }
}
