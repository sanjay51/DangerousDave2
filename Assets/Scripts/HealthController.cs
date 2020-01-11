using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Image jetpackFuelImage;
    public Image jetpackGuyImage;
    public Image livesLeftImage;

    private float originalJetpackFuelImageSize;
    private float originalLivesLeftImageSize;

    public static HealthController instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalJetpackFuelImageSize = jetpackFuelImage.rectTransform.rect.width;
        originalLivesLeftImageSize = livesLeftImage.rectTransform.rect.width;

    }

    // Update is called once per frame
    public void setJetpackFuel(float value)
    {
        if (value <= 0.0)
        {
            jetpackFuelImage.enabled = false;
            jetpackGuyImage.enabled = false;
            return;
        }

        if (value >= 1.0f)
        {
            jetpackFuelImage.enabled = true;
            jetpackGuyImage.enabled = true;
            jetpackFuelImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
                200.0f * value);
        }

        jetpackFuelImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
            200.0f * value);

    }

    // Update is called once per frame
    public void setLives(int value)
    {
        livesLeftImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
            30.0f * value);

    }
}
