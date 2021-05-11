using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Diolog : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public string[] Phrases = {"Чё так долго?", "Ладно, идём"};
    [SerializeField]
    public Texture[] Images = new Texture[0];
    [SerializeField]
    public int[] WhenImageChange = {0};
    [SerializeField] public Text TextPlace;
    [SerializeField] public RawImage ImagePlace;
    [SerializeField] public Canvas NextUI;
    [SerializeField] public bool isStarted;

    private int curPhrase = -1;
    private int curImage = -1;
    void Start()
    {
        gameObject.GetComponent<Canvas>().enabled = isStarted;
        NextUI.enabled = !isStarted;

    }

    // Update is called once per frame
    void Update()
    {
        if(isStarted && Input.GetMouseButtonUp(0))
            NextPhrase();
    }

    public void StartDialog()
    {
        curPhrase = -1;
        curImage = -1;
        TextPlace.text = "";
        isStarted = true;
        gameObject.GetComponent<Canvas>().enabled = true;
        NextUI.enabled = false;
    }

    public void NextPhrase()
    {
        curPhrase += 1;
        if (curPhrase >= Phrases.Length)
        {
            gameObject.GetComponent<Canvas>().enabled = false;
            NextUI.enabled = true;
            isStarted = false;
            return;
        }

        if (WhenImageChange.Contains(curPhrase))
        {
            curImage += 1;
            if (curImage < Images.Length)
                ImagePlace.texture = Images[curImage];
        }
        TextPlace.text = Phrases[curPhrase];
    }

    
}
