using System.Collections;
using System.Collections.Generic;
using ClearSky;
using UnityEngine;

public class DiologTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public bool IsRepeatable = false;
    [SerializeField]
    public Canvas Dialog;

    private bool isUsed = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<SimplePlayerController>() == null) 
            return;
        if (!IsRepeatable && isUsed) 
            return;
        var dialog = Dialog.gameObject.GetComponent<Diolog>();
        dialog?.StartDialog();
        isUsed = true;
    }
}
