using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_updater : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;
    public GameObject iceImage;
    public GameObject fireImage;
    public GameObject ballImage;
    private float currentPosition;
    private Stack<GameObject> images = new Stack<GameObject>();
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void ClearCanvas()
    {
        while (images.Count > 0)
        {
            Destroy(images.Pop());
        }
        currentPosition = 0;
    }

    public void AddImageOnButton(KeyCode code)
    {
        switch (code)
        {
            case KeyCode.Q:
                AddImage(fireImage);
                break;
            case KeyCode.E:
                AddImage(ballImage);
                break;
            case KeyCode.Alpha1:
                AddImage(iceImage);
                break;
        }
    }

    private void AddImage(GameObject image)
    {
        GameObject delta = Instantiate(image) as GameObject;
        delta.transform.SetParent(canvas.transform);
        var rect = delta.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(-60*currentPosition-30,30);
        currentPosition++;
        images.Push(delta);
    }
}
