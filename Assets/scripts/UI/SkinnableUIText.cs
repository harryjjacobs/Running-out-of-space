using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SkinnableUIText : SkinnableUI
{
	public enum TextColor
	{
		LIGHT,
		DARK
	}

	public enum TextFont
	{
		PRIMARY,
		SECONDARY
	}

	public TextColor Color;
	public TextFont Font;

	// Just set the color initially
	// so it can be changed in-game
	public bool SetOnce = false;
	private Text text;
	private bool hasBeenSet = false;

	protected override void OnSkinUI()
	{
		base.OnSkinUI();

		if (SetOnce && hasBeenSet) return;
		if (Skin == null) return;

		text = GetComponent<Text>();

		switch (Font)
		{
			case TextFont.PRIMARY:
				text.font = Skin.PrimaryFont;
				break;
			case TextFont.SECONDARY:
				text.font = Skin.SecondaryFont;
				break;
			default:
				break;
		}
		
		switch (Color)
		{
			case TextColor.LIGHT:
				text.color = Skin.LightTextColor;
				break;
			case TextColor.DARK:
				text.color = Skin.DarkTextColor;
				break;
			default:
				break;
		}

		hasBeenSet = SetOnce;
	}
}
