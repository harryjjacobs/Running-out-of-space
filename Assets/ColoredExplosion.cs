using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredExplosion : MonoBehaviour
{
	List<GradientColorKey> gck = new List<GradientColorKey>();
	List<GradientAlphaKey> gak = new List<GradientAlphaKey>();

    public void SetColors(params Color[] colors)
    {
		for (int i = 0; i < colors.Length; i++)
		{
			// Make the keys equidistant
			float t = (float) i / (float) colors.Length;
			gck.Add(new GradientColorKey(colors[i], t));
			gak.Add(new GradientAlphaKey(colors[i].a, t));
		}
		
		Gradient grad = new Gradient();
		grad.colorKeys = gck.ToArray();
		grad.alphaKeys = gak.ToArray();
		grad.mode = GradientMode.Blend;

		ParticleSystem.MinMaxGradient minMaxGrad = new ParticleSystem.MinMaxGradient();
		minMaxGrad.mode = ParticleSystemGradientMode.RandomColor; // Pick a random color in the gradient
		minMaxGrad.gradient = grad;

		var main = GetComponent<ParticleSystem>().main;
        main.startColor = minMaxGrad;
    }

	public void Explode()
	{
		GetComponent<ParticleSystem>().Play();
		Destroy(gameObject, GetComponent<ParticleSystem>().main.duration - 0.1f);
	}
}
