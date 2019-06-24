using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

public Transform tilePrefab;
public Vector2 mapSize;

[Range(0,2)]
public float outline;


public void GenerateMap()
{
	for(int x=0;x < mapSize.x;x++)
	{
		for(int y = 0; y < mapSize.y;y++)
		{
			Vector2 tilePos = CoordinatesToPosition(x,y);
			Transform newTile = Instantiate(tilePrefab,tilePos,Quaternion.identity);			
			newTile.localScale = Vector3.one * (1-outline);
            newTile.parent = transform;
            newTile.GetComponent<TileLogic>().X = x;
            newTile.GetComponent<TileLogic>().Y = y;
            newTile.GetComponent<TileLogic>().IsChecked = false;
			newTile.name = "tile" + x + "x" +y;
		}
	}
	
}

public  void DeleteAllTiles()
{	
	GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
 
     for (int i = 0; i < tiles.Length; i++)
     {
         DestroyImmediate(tiles[i]);
     }
}


private Vector2 CoordinatesToPosition(int x, int y)
{
 	Vector2 newCoordinates = new Vector2(-mapSize.x/2 + 0.5f + x,-mapSize.y/2 + 0.5f +y);
	return newCoordinates;
}

}
