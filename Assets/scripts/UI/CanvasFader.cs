using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFader : MonoBehaviour
{
    public enum FadeType
    {
        IN,
        OUT
    }

    public Canvas canvas;
    public Color FadeColor;
    public float FadeDuration;

    public bool FadeInOnAwake = false;

    public event FadeHandler OnFadeFinished;
    public delegate void FadeHandler(FadeType type);

    private Color transparent = new Color(0, 0, 0, 0);
    private Image fadeImage;
    private bool fadeIn;
    private bool fadeOut;

    private void Awake()
    {
        GameObject imageGo = new GameObject("Canvas Fader", typeof(Image));
        imageGo.transform.SetParent(canvas.transform);
        // move the transform to the end of the hierarchy so it's on top of everything
        imageGo.transform.SetSiblingIndex(canvas.transform.childCount - 1);
        // stretch the image so it's centered fullscreen
        imageGo.transform.localScale = Vector3.one;
        imageGo.GetComponent<RectTransform>().anchorMin = Vector2.zero;
        imageGo.GetComponent<RectTransform>().anchorMax = Vector2.one;
        imageGo.GetComponent<RectTransform>().pivot = Vector2.one*0.5f;
        imageGo.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        imageGo.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        fadeImage = imageGo.GetComponent<Image>();
        fadeImage.raycastTarget = false; // so we don't block clicks
        fadeImage.color = transparent;

        if (FadeInOnAwake)
        {
            Fade(FadeType.IN);
        }
    }

    float t = 0;
    void Update()
    {
        //Debug.Log(fadeIn);
        if (fadeIn)
        {
            fadeImage.color = Color.Lerp(FadeColor, transparent, t);
        }
        else if (fadeOut)
        {
            fadeImage.color = Color.Lerp(transparent, FadeColor, t);
        }

        if (t < 1)
        {
            t += Time.deltaTime / FadeDuration;
        }
        else if (fadeIn || fadeOut)
        {
            if (fadeIn)
            {
                fadeIn = false;
                fadeOut = false;
                if (OnFadeFinished != null)
                    OnFadeFinished(FadeType.IN);
            }
            else if (fadeOut)
            {
                fadeOut = false;
                if (OnFadeFinished != null)
                    OnFadeFinished(FadeType.OUT);
            }
        }
    }

    public void Fade(FadeType type)
    {
        switch (type)
        {
            case FadeType.IN:
                fadeOut = false;
                fadeIn = true;
                break;
            case FadeType.OUT:
                fadeIn = false;
                fadeOut = true;
                break;
            default:
                break;
        }
        t = 0;
    }
}
