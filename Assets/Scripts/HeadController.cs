using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour 
{
    enum ROTATION 
    {
        DEEGREES_0,
        DEEGREES_90,
        DEEGREES_180,
        DEEGREES_270
    }

    public BoxWithHeads box;
 
    
    

	public List<TileLogic> pathTiles;

	public TileLogic coordinates;

	private void Start() 
    {
        int tileMask = (1 << LayerMask.NameToLayer("MapTiles"));
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0,0,-1), new Vector3(0,0,1), 10, tileMask);
        pathTiles.Add(hit.collider.gameObject.GetComponent<TileLogic>());
        coordinates = pathTiles[0];
        coordinates.IsChecked = true;      
        coordinates.GetComponent<SpriteRenderer>().sprite = box.sprites[0];    
        coordinates.ColorTiler(box.color);
    }

    public void Move(TileLogic tile) 
    {
        pathTiles.Add(tile);
        tile.IsChecked = true;
        coordinates = tile;
        gameObject.transform.position = tile.transform.position;
        ROTATION rot = CountRotationHead();
        int mat = ChooseMaterial();

        //head
      
        coordinates.GetComponent<SpriteRenderer>().sprite = box.sprites[0];
        coordinates.gameObject.transform.rotation = Quaternion.Euler(RotationFromROTATION(rot));

        //rest
        if (pathTiles.Count >= 2)
        {          
            pathTiles[pathTiles.Count -2].GetComponent<SpriteRenderer>().sprite = box.sprites[mat];
            if (mat == 2)
            {
                rot = CountRotationSecond();
            }
            pathTiles[pathTiles.Count - 2].gameObject.transform.rotation = Quaternion.Euler(RotationFromROTATION(rot));
        }
        

        GameManager.Instance.CheckIfAllClicked();
        pathTiles[0].GetComponent<SpriteRenderer>().sprite = box.sprites[0];
        tile.ColorTiler(box.color);
    }

    public void GoBack() 
    {
        pathTiles[pathTiles.Count - 1].IsChecked = false;
      
        pathTiles[pathTiles.Count-1].GetComponent<SpriteRenderer>().sprite = box.sprites[3];
        pathTiles.RemoveAt(pathTiles.Count-1);
        coordinates = pathTiles[pathTiles.Count - 1];
        gameObject.transform.position = pathTiles[pathTiles.Count - 1].transform.position;
        ROTATION rot = CountRotationHead();
       
        coordinates.GetComponent<SpriteRenderer>().sprite = box.sprites[1];
        coordinates.gameObject.transform.rotation = Quaternion.Euler(RotationFromROTATION(rot));
        pathTiles[0].GetComponent<SpriteRenderer>().sprite = box.sprites[0];
        coordinates.GetComponent<SpriteRenderer>().sprite = box.sprites[0];      
      


    }

    public void MergeHeads(TileLogic tile)
    {
        pathTiles.Add(tile);
        tile.IsChecked = true;
        coordinates = tile;

        ROTATION rot = CountRotationHead();
        int mat = ChooseMaterial();

        //head
        coordinates.gameObject.transform.rotation = Quaternion.Euler(RotationFromROTATION(rot));
        //rest
        if (pathTiles.Count >= 2)
        {          
            pathTiles[pathTiles.Count -2].GetComponent<SpriteRenderer>().sprite = box.sprites[mat];
            if (mat == 2)
            {
                rot = CountRotationSecond();
            }
            pathTiles[pathTiles.Count - 2].gameObject.transform.rotation = Quaternion.Euler(RotationFromROTATION(rot));
        }
        

        GameManager.Instance.CheckIfAllClicked();
        pathTiles[0].GetComponent<SpriteRenderer>().sprite = box.sprites[0];

        transform.position = pathTiles[0].gameObject.transform.position;
        coordinates = pathTiles[0];
        pathTiles.RemoveAt(pathTiles.Count -1);
    }

    private ROTATION CountRotationSecond() 
    {
        int x_2 = pathTiles[pathTiles.Count - 1].X - pathTiles[pathTiles.Count - 2].X;
        int x_1 = pathTiles[pathTiles.Count - 2].X - pathTiles[pathTiles.Count - 3].X;

        int y_2 = pathTiles[pathTiles.Count - 1].Y - pathTiles[pathTiles.Count - 2].Y;
        int y_1 = pathTiles[pathTiles.Count - 2].Y - pathTiles[pathTiles.Count - 3].Y;

        if (x_1 == 1)
        {
            if (y_2 == 1)
            {
                return ROTATION.DEEGREES_270;
            }
            else if (y_2 == -1)
            {
                return ROTATION.DEEGREES_0;
            }
        }
        else if (x_1 == -1)
        {
            if (y_2 == 1)
            {
                return ROTATION.DEEGREES_180;
            }
            else if (y_2 == -1)
            {
                return ROTATION.DEEGREES_90;
            }
        }
        else if (y_1 == 1)
        {
            if (x_2 == 1)
            {
                return ROTATION.DEEGREES_90;
            }
            else if (x_2 == -1)
            {
                return ROTATION.DEEGREES_0;
            }
        }
        else if (y_1 == -1)
        {
            if (x_2 == 1)
            {
                return ROTATION.DEEGREES_180;
            }
            else if (x_2 == -1)
            {
                return ROTATION.DEEGREES_270;
            }
        }
        return ROTATION.DEEGREES_0;
    }

    private ROTATION CountRotationHead() 
    {
        if (pathTiles.Count == 1)
        {
            return ROTATION.DEEGREES_0;
        }
        else
	    {
            Vector3 dir = pathTiles[pathTiles.Count - 2].gameObject.transform.position
                - pathTiles[pathTiles.Count - 1].gameObject.transform.position;
            if (dir.x >0.5)
            {
                return ROTATION.DEEGREES_90;
            }
            else if (dir.x < -0.5)
            {
                return ROTATION.DEEGREES_270;
            }
            else if (dir.z > 0.5)
            {
                return ROTATION.DEEGREES_180;
            }
                return ROTATION.DEEGREES_0;
	    }
    }

    private int ChooseMaterial() 
    {
        if (pathTiles.Count == 1)
        {
            return 0;
        }
        else if (pathTiles.Count == 2)
        {
            return 1;
        }
        else
        {
            if (IsTurn())
                return 2;
            return 1;
        }
    }

    private bool IsTurn() 
    {
        if (pathTiles.Count >= 3)
        {
            if (pathTiles[pathTiles.Count - 1].X == pathTiles[pathTiles.Count - 2].X &&
                pathTiles[pathTiles.Count - 2].X == pathTiles[pathTiles.Count - 3].X ||
                pathTiles[pathTiles.Count - 1].Y == pathTiles[pathTiles.Count - 2].Y &&
                pathTiles[pathTiles.Count - 2].Y == pathTiles[pathTiles.Count - 3].Y)
            {
                return false;
            }
        }
        return true;
    }

    private Vector3 RotationFromROTATION(ROTATION rott) 
    {
        switch (rott)
        {
            case ROTATION.DEEGREES_0:
                return new Vector3(0, 0, 0);
            case ROTATION.DEEGREES_90:
                return new Vector3(0, 0, 90);
            case ROTATION.DEEGREES_180:
                return new Vector3(0, 0, 180);
            case ROTATION.DEEGREES_270:
                return new Vector3(0, 0, 270);
            default:
                return new Vector3(0, 0, 0);
        }
    }

    public void ErasePath()
    {
        while(pathTiles.Count > 1)
        {
            GoBack();
        }
    }
}
