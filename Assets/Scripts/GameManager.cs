using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance;

	private GameObject[] tiles;
	private void Start() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
		tiles = GameObject.FindGameObjectsWithTag("Tile");		
	}
	
	public void CheckIfAllClicked()
	{
		bool areAllClicked = tiles.All(x => x.GetComponent<TileLogic>().IsChecked);
        if (areAllClicked)
        {
            Debug.Log("Clicked all objects");
        } 
	}
}
