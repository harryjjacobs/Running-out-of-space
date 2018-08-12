using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SkinnableUIImageColor : SkinnableUI
{
	public enum ImageColor
	{
		CUSTOM,
		PRIMARY_LIGHT,
		PRIMARY_DARK,
		SECONDARY_LIGHT,
		SECONDARY_DARK,
	}

	public ImageColor Color;

	private Image image;

	protected override void OnSkinUI()
	{
		base.OnSkinUI();
		
		if (Skin == null) return;

		image = GetComponent<Image>();

		switch (Color)
		{
			case ImageColor.CUSTOM:
				// Just use the one on the image component
				break;
			case ImageColor.PRIMARY_LIGHT:
				image.color = Skin.PrimaryColorLight;
				break;
			case ImageColor.SECONDARY_LIGHT:
				image.color = Skin.SecondaryColorLight;
				break;
			case ImageColor.PRIMARY_DARK:
				image.color = Skin.PrimaryColorDark;
				break;
			case ImageColor.SECONDARY_DARK:
				image.color = Skin.SecondaryColorDark;
				break;
			default:
				break;
		}

	}
}
