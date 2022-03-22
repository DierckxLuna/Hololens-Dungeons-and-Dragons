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
        new int[] { 1,1,1,1,1,1,1,1,1,1 },
        new int[] { 1,0,0,0,0,0,0,0,0,1 },
        new int[] { 1,0,0,0,0,0,0,0,0,1 },
        new int[] { 1,0,0,0,0,0,0,0,0,1 },
        new int[] { 1,1,1,1,0,1,1,1,1,1 },
        new int[] { -1,1,0,0,0,0,1,-1,-1,-1 },
        new int[] { -1,1,0,0,0,0,1,-1,-1,-1 },
        new int[] { -1,1,0,0,0,0,1,-1,-1,-1 },
        new int[] { -1,1,0,0,0,0,1,-1,-1,-1 },
        new int[] { -1,1,1,1,1,1,1,-1,-1,-1 }
    };

    // Start is called before the first frame update
    void Start()
    {
        GameObject newTile;
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == 0)
                {
                    if (wallNorth(i, j) && wallSouth(i, j))
                    {
                        newTile = Instantiate(door, this.transform);
                        newTile.transform.Rotate(0, 90, 0);
                    }
                    else if (wallEast(i, j) && wallWest(i, j))
                    {
                        newTile = Instantiate(door, this.transform);
                    }
                    else
                    {
                        newTile = Instantiate(tile, this.transform);
                    }

                    newTile.transform.position = this.transform.position + new Vector3(i * tileSize, 0, j * tileSize);
                    newTile.transform.localScale = new Vector3(tileSize, tileSize, tileSize);
                }
                else if (map[i][j] == 1)
                {
                    bool north = wallNorth(i, j);
                    bool south = wallSouth(i, j);
                    bool east = wallEast(i, j);
                    bool west = wallWest(i, j);

                    if (north && south && east && west)
                    {
                        newTile = Instantiate(intersection, this.transform);
                    }
                    else if (!north && south && east && west)
                    {
                        newTile = Instantiate(tWall, this.transform);
                    }
                    else if (north && !south && east && west)
                    {
                        newTile = Instantiate(tWall, this.transform);
                        newTile.transform.Rotate(0, 90, 0);
                    }
                    else if (north && south && !east && west)
                    {
                        newTile = Instantiate(tWall, this.transform);
                        newTile.transform.Rotate(0, 180, 0);
                    }
                    else if (north && south && east && !west)
                    {
                        newTile = Instantiate(tWall, this.transform);
                        newTile.transform.Rotate(0, 270, 0);
                    }
                    else if (north && south)
                    {
                        newTile = Instantiate(wall, this.transform);
                    }
                    else if (east && west)
                    {
                        newTile = Instantiate(wall, this.transform);
                        newTile.transform.Rotate(0, 90, 0);
                    }
                    else if (east)
                    {
                        newTile = Instantiate(halfWall, this.transform);
                    }
                    else if (west)
                    {
                        newTile = Instantiate(halfWall, this.transform);
                        newTile.transform.Rotate(0, 90, 0);
                    }
                    else if (south)
                    {
                        newTile = Instantiate(halfWall, this.transform);
                        newTile.transform.Rotate(0, 180, 0);
                    }
                    else if (north)
                    {
                        newTile = Instantiate(halfWall, this.transform);
                        newTile.transform.Rotate(0, 270, 0);
                    }
                    else
                    {
                        newTile = Instantiate(tile, this.transform);
                    }

                    newTile.transform.position = this.transform.position + new Vector3(i * tileSize, 0, j * tileSize);
                    newTile.transform.localScale = new Vector3(tileSize, tileSize, tileSize);
                }
            }
        }
    }

    private bool wallNorth(int i, int j) => (i > 0 && map[i - 1][j] == 1);
    private bool wallSouth(int i, int j) => (i + 1 < map.Length && map[i + 1][j] == 1);
    private bool wallEast(int i, int j) => (j > 0 && map[i][j - 1] == 1);
    private bool wallWest(int i, int j) => (j + 1 < map[i].Length && map[i][j + 1] == 1);
}
