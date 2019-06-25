using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxWithHeads : MonoBehaviour
{    
public bool isFinished;


    public HeadController head1;
    public HeadController head2;

    public Sprite[] sprites;

    public Color32 color;
    
public void ErasePathFromAnotherHead(HeadController head)
{
    if(isFinished)
    {
        head1.ErasePath();
        head2.ErasePath();
    }
    else
    {
        if(head == head1)
        {
            head2.ErasePath();
        } else
        {
            head1.ErasePath();
        }    
    }
    isFinished = false;
}

public bool IsTileWithSecondHea(HeadController head, TileLogic tile)
{
    if(head == head1)
    {
        if(head2.coordinates == tile)
        return true;
    } 
    else
    {
        if(head1.coordinates == tile)
            return true;
    }
   return false;
   }
}
