using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHelpInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MovementHelp;
    public GameObject StackHelp;
    public GameObject RuneHelp;
    public Diolog ChangeDialog;
    public Diolog EndDialog;

    void Start()
    {
        
    }   

    // Update is called once per frame
    void Update()
    {
        if (EndDialog != null && (EndDialog.isStarted || EndDialog.IsEnded))
        {
            gameObject.GetComponent<Canvas>().enabled = false;
            return;
        }
        if (ChangeDialog.isStarted)
        {
            gameObject.GetComponent<Canvas>().enabled = false;
            return;
        }
        if (!ChangeDialog.IsEnded) return;
        gameObject.GetComponent<Canvas>().enabled = true;
        MovementHelp.transform.localScale = Vector3.zero;
        RuneHelp.transform.localScale = Vector3.one;
        StackHelp.transform.localScale = Vector3.one;


    }
}
