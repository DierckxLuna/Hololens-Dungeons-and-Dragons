using System.Collections.Generic;
using Assets.Scripts.DataManagement;
using UnityEngine;

namespace Assets.Scripts.DungeonBuilding
{
    public class DungeonBuilder : MonoBehaviour
    {
        public static DungeonBuilder Instance;

        [SerializeField]
        List<GameObject> walls;

        [SerializeField]
        GameObject tWall;

        [SerializeField]
        GameObject corner;

        [SerializeField]
        GameObject intersection;

        [SerializeField]
        List<GameObject> tiles;

        [SerializeField]
        GameObject door;

        [SerializeField]
        private float tileSize = 0.047625f;

        [SerializeField]
        private SharedBool inDecoratingMode;

        [SerializeField]
        private SharedBool inRotatingMode;

        public int[][] Map; // TODO: replace this with array of the gameobjects

        // Start is called before the first frame update
        void Start()
        {
            if (Instance == null) Instance = this;

            inDecoratingMode.Value = false;

            Map = new int[][]
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

            for (int i = 0; i < Map.Length; i++)
            {
                for (int j = 0; j < Map[i].Length; j++)
                {
                    if (Map[i][j] == 1)
                    {
                        placeTile(i, j);
                    }
                    else if (Map[i][j] == 8)
                    {
                        placeDoor(i, j);
                    }
                    else if (Map[i][j] == 9)
                    {
                        placeWall(i, j);
                    }
                }
            }
        }

        public void ToggleDecoratingMode()
        {
            inDecoratingMode.Value = !inDecoratingMode.Value;
            inRotatingMode.Value = false;
        }

        public void ToggleRotatingMode()
        {
            inRotatingMode.Value = !inRotatingMode.Value;
            inDecoratingMode.Value = false;
        }

        private bool wallNorth(int i, int j) => (i > 0 && Map[i - 1][j] >= 8);
        private bool wallSouth(int i, int j) => (i + 1 < Map.Length && Map[i + 1][j] >= 8);
        private bool wallEast(int i, int j) => (j > 0 && Map[i][j - 1] >= 8);
        private bool wallWest(int i, int j) => (j + 1 < Map[i].Length && Map[i][j + 1] >= 8);

        private void placeTile(int i, int j)
        {
            GameObject newTile = Instantiate(tiles.GetRandom(), transform);
            newTile.transform.Rotate(0, UnityEngine.Random.Range(0, 4) * 90, 0); // randomly rotating it so that it's harder to see the tiling.

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

        private void setPositionAndScale(GameObject newTile, int i, int j)
        {
            if (newTile == null) return;

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
                    newWall = Instantiate(walls.GetRandom(), this.transform);
                }
                else if (east && west)
                {
                    newWall = Instantiate(walls.GetRandom(), this.transform);
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
                    newWall = Instantiate(walls.GetRandom(), this.transform);
                }
                else if (north)
                {
                    newWall = Instantiate(walls.GetRandom(), this.transform);
                    newWall.transform.Rotate(0, 90, 0);
                }
                else if (west)
                {
                    newWall = Instantiate(walls.GetRandom(), this.transform);
                    newWall.transform.Rotate(0, 180, 0);
                }
                else
                {
                    newWall = Instantiate(walls.GetRandom(), this.transform);
                    newWall.transform.Rotate(0, 270, 0);
                }
            }
            else
            {
                // TODO: Column?
                //newWall = Instantiate(tile, this.transform);
            }

            setPositionAndScale(newWall, i, j);
        }
    }
}
