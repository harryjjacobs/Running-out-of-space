using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public abstract class SkinnableUI : MonoBehaviour
{
	public SkinnableUIData Skin;

	public virtual void Awake()
	{
		OnSkinUI();
	}

	public virtual void Update()
	{
#if UNITY_EDITOR
		OnSkinUI();
#endif
	}

	protected virtual void OnSkinUI()
	{

	}
}
