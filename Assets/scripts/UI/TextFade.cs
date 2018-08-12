using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
class TextFade : MonoBehaviour
{
	public enum FadeType
	{
		IN,
		OUT
	}

	public Color FadeColor = Color.white;
	public float FadeDuration = 2;
	public bool FadeInOnAwake = false;

	public event FadeHandler OnFadeFinished;
	public delegate void FadeHandler(FadeType type);

	private Color initialColor;
	private Text textComponent;
	private bool fadeIn;
	private bool fadeOut;

	void Awake()
	{
		textComponent = GetComponent<Text>();
		initialColor = textComponent.color;
		if (FadeInOnAwake)
			Fade(FadeType.IN);
	}

	float t = 0;
    void Update()
    {
        if (fadeIn)
        {
            textComponent.color = Color.Lerp(FadeColor, initialColor, t);
        }
        else if (fadeOut)
        {
            textComponent.color = Color.Lerp(initialColor, FadeColor, t);
        }

        if (t < 1)
        {
            t += Time.deltaTime / FadeDuration;
        }
        else if (fadeIn)
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