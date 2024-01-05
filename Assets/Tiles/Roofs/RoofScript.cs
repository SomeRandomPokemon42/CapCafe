using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofScript : MonoBehaviour
{
	private bool Shown = true;
	[SerializeField] private float Speed = 2;
	[SerializeField] private float FadeLevel = 0;
	private float progress = 1;
	private float lastprogress = 1;
	public void ShowRoof()
	{
		Shown = true;
	}
	public void HideRoof()
	{
		Shown = false;
	}
	private void LateUpdate()
	{
		if (Shown)
		{
			progress += Time.deltaTime * Speed;
		}
		else
		{
			progress -= Time.deltaTime * Speed;
		}
		progress = Mathf.Clamp(progress, FadeLevel, 1);
		// This if statement looks dumb, but floating points are dumb, so shut up
		if (Mathf.RoundToInt(progress*100) != Mathf.RoundToInt(lastprogress*100))
		{
            SpriteRenderer[] Renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer r in Renderers)
            {
                r.color = new Color(1, 1, 1, progress);
            }
        }
		lastprogress = progress;
	}
}
