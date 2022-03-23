using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonBuilder : MonoBehaviour
{
    [SerializeField]
    GameObject wall;

    [SerializeField]
    GameObject tWall;

    [SerializeField]
    GameObject halfWall;

    [SerializeField]
    GameObject corner;

    [SerializeField]
    GameObject intersection;

    [SerializeField]
    GameObject tile;

    [SerializeField]
    GameObject door;

    [SerializeField]
    private float tileSize = 0.0047625f;

    private int[][] map = new int[][]
    {
        new int[] { 9,9,9,9,9,9,9,9,9,9 },
        new int[] { 9,1,1,1,1,1,1,1,1,9 },
        new int[] { 9,1,1,1,1,1,1,1,1,9 },
        new int[] { 9,1,1,1,1,1,1,1,1,9 },
        new int[] { 9,9,9,9,8,9,9,9,9,9 },
        new int[] { 0,9,1,1,1,1,9,0,0,0 },
        new int[] { 0,9,1,1,1,1,9,0,0,0 },
        new int[] { 0,9,1,1,1,1,9,0,0,0 },
        new int[] { 0,9,1,1,1,1,9,0,0,0 },
        new int[] { 0,9,9,9,9,9,9,0,0,0 }
    };

    // Start is called before the first frame update
    void Start()
    {
        GameObject newTile;
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                newTile = null;

                if (map[i][j] == 1)
                {
                    placeTile(i, j);
                }
                else if (map[i][j] == 8)
                {
                    placeDoor(i, j);
                }
                else if (map[i][j] == 9)
                {
                    placeWall(i, j);
                }
            }
        }
    }

    private void placeTile(int i, int j)
    {
        GameObject newTile = null;

        newTile = Instantiate(tile, this.transform);

        setPositionAndScale(newTile, i, j);
    }

    private void placeDoor(int i, int j)
    {
        GameObject newDoor = null;

        if (wallNorth(i, j) && wallSouth(i, j))
        {
            newDoor = Instantiate(door, this.transform);
            newDoor.transform.Rotate(0, 90, 0);

        }
        else if (wallEast(i, j) && wallWest(i, j))
        {
            newDoor = Instantiate(door, this.transform);
        }

        setPositionAndScale(newDoor, i, j);
    }

    private bool wallNorth(int i, int j) => (i > 0 && map[i - 1][j] >= 8);
    private bool wallSouth(int i, int j) => (i + 1 < map.Length && map[i + 1][j] >= 8);
    private bool wallEast(int i, int j) => (j > 0 && map[i][j - 1] >= 8);
    private bool wallWest(int i, int j) => (j + 1 < map[i].Length && map[i][j + 1] >= 8);

    private void setPositionAndScale(GameObject newTile, int i, int j)
    {
        newTile.transform.position = this.transform.position + new Vector3(i * tileSize, 0, j * tileSize);
        newTile.transform.localScale = new Vector3(tileSize, tileSize, tileSize);
    }

    private void placeWall(int i, int j)
    {
        bool north = wallNorth(i, j);
        bool south = wallSouth(i, j);
        bool east = wallEast(i, j);
        bool west = wallWest(i, j);

        int neighbours = 0;
        if (north) neighbours++;
        if (south) neighbours++;
        if (east) neighbours++;
        if (west) neighbours++;

        GameObject newWall = null;

        if (neighbours == 4)
        {
            newWall = Instantiate(intersection, this.transform);
        }
        else if (neighbours == 3)
        {
            newWall = Instantiate(tWall, this.transform);

            if (!north)
            {
                newWall.transform.Rotate(0, -90, 0);
            }
            else if (!east)
            {
                newWall.transform.Rotate(0, -180, 0);
            }
            else if (!south)
            {
                newWall.transform.Rotate(0, -270, 0);
            }
        }
        else if (neighbours == 2)
        {
            if (north && south)
            {
                newWall = Instantiate(wall, this.transform);
            }
            else if (east && west)
            {
                newWall = Instantiate(wall, this.transform);
                newWall.transform.Rotate(0, 90, 0);
            }
            else if (west && north)
            {
                newWall = Instantiate(corner, this.transform);
                newWall.transform.Rotate(0, 180, 0);
            }
            else if (north && east)
            {
                newWall = Instantiate(corner, this.transform);
                newWall.transform.Rotate(0, 90, 0);
            }
            else if (east && south)
            {
                newWall = Instantiate(corner, this.transform);
            }
            else
            {
                newWall = Instantiate(corner, this.transform);
                newWall.transform.Rotate(0, 270, 0);
            }
        }
        else if (neighbours == 1)
        {
            if (east)
            {
                newWall = Instantiate(halfWall, this.transform);
            }
            else if (north)
            {
                newWall = Instantiate(halfWall, this.transform);
                newWall.transform.Rotate(0, 90, 0);
            }
            else if (west)
            {
                newWall = Instantiate(halfWall, this.transform);
                newWall.transform.Rotate(0, 180, 0);
            }
            else
            {
                newWall = Instantiate(halfWall, this.transform);
                newWall.transform.Rotate(0, 270, 0);
            }
        }
        else
        {
            // TODO: Column?
            newWall = Instantiate(tile, this.transform);
        }

        setPositionAndScale(newWall, i, j);
    }
}
