using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    HeadController head = null;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool found = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit[] hit = Physics.RaycastAll(ray);


            RaycastHit2D[] hit = Physics2D.GetRayIntersectionAll(ray, 100);

            if (hit != null && hit.Length > 0)
            {
                int iterator = 0;

                while (!found)
                {
                    head = hit[iterator].collider.gameObject.GetComponent<HeadController>();
                    iterator++;
                    if (head != null || iterator >= hit.Length)
                        found = true;
                }
            }
            if (head != null)
            {
                head.box.ErasePathFromAnotherHead(head);
            }
            else
            {
                found = false;
                Tail tail = null;
                if (hit != null && hit.Length > 0)
                {
                    int iterator = 0;

                    while (!found)
                    {
                        tail = hit[iterator].collider.gameObject.GetComponent<Tail>();
                        iterator++;
                        if (tail != null || iterator >= hit.Length)
                            found = true;
                    }
                }
                if (tail != null)
                {
                    tail.head.ErasePath();
                    head = tail.head;
                }

            }
        }

        if (Input.GetMouseButton(0))
        {
            if (head != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // RaycastHit[] hit2 = Physics.RaycastAll(ray);               

                RaycastHit2D[] hit2 = Physics2D.GetRayIntersectionAll(ray, 100);
                TileLogic tile = null;

                if (hit2 != null && hit2.Length > 0)
                {
                    bool found = false;
                    int iterator = 0;


                    while (!found)
                    {
                        tile = hit2[iterator].collider.gameObject.GetComponent<TileLogic>();
                        iterator++;
                        if (tile != null || iterator >= hit2.Length)
                            found = true;
                    }

                    if (tile != null)
                    {
                        if (CheckIfNeighbours(tile.X, head.coordinates.X, tile.Y, head.coordinates.Y))
                        {
                            if (tile.IsChecked)
                            {
                                if (head.box.IsTileWithSecondHea(head, tile))
                                {
                                    head.box.isFinished = true;
                                    head.MergeHeads(tile);
                                    head = null;
                                }
                                else if (head.pathTiles.Count > 1)
                                {
                                    if (head.pathTiles[head.pathTiles.Count - 2] == tile)
                                    {
                                        head.GoBack();
                                    }
                                }
                            }
                            else
                            {
                                head.Move(tile);
                            }
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            head = null;
        }
    }

    private bool CheckIfNeighbours(int _x1, int _x2, int _y1, int _y2)
    {
        if ((Mathf.Abs(_x1 - _x2) + Mathf.Abs(_y1 - _y2)) == 1)
            return true;
        return false;

    }


}
