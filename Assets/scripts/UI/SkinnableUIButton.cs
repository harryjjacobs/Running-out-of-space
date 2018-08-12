using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class SkinnableUIButton : SkinnableUI
{
	private Image image;
	private Button button;
	private Animator animator;

	private bool registeredClick = false;

	void Awake()
	{
		registeredClick = false;
	}

	protected override void OnSkinUI()
	{
		base.OnSkinUI();

		if (Skin == null) return;

		image = GetComponent<Image>();
		button = GetComponent<Button>();
		animator = GetComponent<Animator>();

		switch (Skin.ButtonTransition)
		{
			case Selectable.Transition.Animation:
				if (animator == null)
				{
					Debug.LogError("Button must have an animator component to do transition animation");
					Skin.ButtonTransition = Selectable.Transition.ColorTint;
				}
				break;
			default:
				button.targetGraphic = image;
				image.sprite = Skin.ButtonSprite;
				image.color = Skin.ButtonColor;
				break;
		}

		button.transition = Skin.ButtonTransition;
		button.spriteState = Skin.SpriteState;
		button.colors = Skin.ColorBlock;
		button.animationTriggers = Skin.AnimationTriggers;

		if (!registeredClick)
			button.onClick.AddListener(DoClickAudio);
			registeredClick = true;
	}

	void DoClickAudio()
	{
		UIAudio.PlaySFX(Skin.ButtonClickSounds.RandomItem());
	}
}
