using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName="Create UI Skin")]
public class SkinnableUIData : ScriptableObject
{
	public Color PrimaryColorLight = Color.white;
    public Color PrimaryColorDark = Color.black;
    public Color SecondaryColorLight = Color.white;
    public Color SecondaryColorDark = Color.black;
    public Color LightTextColor = Color.white;
    public Color DarkTextColor = Color.white;
    public Font PrimaryFont;
    public Font SecondaryFont;
    public AudioClip[] ButtonClickSounds;
    public Selectable.Transition ButtonTransition;
    public Sprite ButtonSprite;
    public Color ButtonColor = Color.white;
    [Header("Button Sprite Swap Transition")]
    public SpriteState SpriteState;
    [Header("Button Color Swap Transition")]
    public ColorBlock ColorBlock = new ColorBlock()
    {
        normalColor = Color.white,
        highlightedColor = new Color(0.9f, 0.9f, 0.9f),
        pressedColor = new Color(0.7f, 0.7f, 0.7f),
        disabledColor = new Color(0.7f, 0.7f, 0.7f, 0.5f)
    };
    [Header("Button Animation Transition")]
    public AnimationTriggers AnimationTriggers;
}
