using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLogic : MonoBehaviour 
{
    [SerializeField]
    public bool IsChecked;
    [SerializeField]
    public int X;
    [SerializeField]
    public int Y;

    public SpriteRenderer srenderer;

	public void ColorTiler(Color32 color)
	{
		if(srenderer == null)
		{
			srenderer = GetComponent<SpriteRenderer>();
		}
		srenderer.color = color;
	}
}
