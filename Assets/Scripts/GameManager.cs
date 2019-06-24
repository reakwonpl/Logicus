using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public BoxWithHeads[] boxes;

	private GameObject[] tiles;
	private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
		tiles = GameObject.FindGameObjectsWithTag("Tile");

        boxes = FindObjectsOfType<BoxWithHeads>();

	}

    public bool AreBoxesFinished()
    {
        for(int i = 0; i < boxes.Length;i++)
        {
            if(!boxes[i].isFinished)
            {
                return false;
            }

        }
     Debug.Log("All finished");
     return true;

    }
	public void CheckIfAllClicked()
	{
		//bool areAllClicked = tiles.All(x => x.GetComponent<TileLogic>().IsChecked);
        bool areAllClicked = AreBoxesFinished();
        if (areAllClicked)
        {
            Debug.Log("All Head Merged");
        }
	}
}
