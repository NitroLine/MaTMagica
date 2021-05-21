using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{

    public float maxValue = 100;
    public Color color = Color.red;
    public int width = 4;
    public Slider slider;
    public bool isRight;

    private static float current;

    void Start()
    {
        slider.fillRect.GetComponent<Image>().color = color;

        slider.maxValue = maxValue;
        slider.minValue = 0;
        current = maxValue;

        UpdateUI();
    }
    
    void Update()
    {
        if (current < 0) current = 0;
        if (current > maxValue) current = maxValue;
        slider.value = current;
    }

    void UpdateUI()
    {
        var rect = slider.GetComponent<RectTransform>();

        var rectDeltaX = Screen.width / width;
        float rectPosX = 0;

        if (isRight)
        {
            rectPosX = rect.position.x - (rectDeltaX - rect.sizeDelta.x) / 2;
            slider.direction = Slider.Direction.RightToLeft;
        }
        else
        {
            rectPosX = rect.position.x + (rectDeltaX - rect.sizeDelta.x) / 2;
            slider.direction = Slider.Direction.LeftToRight;
        }

        rect.sizeDelta = new Vector2(rectDeltaX, rect.sizeDelta.y);
        var rectPosY = rect.position.y;
        var rectPosZ = rect.position.z;
        rect.position = new Vector3(rectPosX, rectPosY, rectPosZ);
    }

    public static void AdjustCurrentValue(float adjust)
    {
        current += adjust;
    }
}
