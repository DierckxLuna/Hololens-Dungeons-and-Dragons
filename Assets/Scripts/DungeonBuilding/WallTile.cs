using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.DungeonBuilding
{
    public class WallTile : Tile
    {
        [SerializeField]
        private GameObject lonelyWall;

        [SerializeField]
        private List<GameObject> normalWallPrefabs;

        private GameObject normalWall;

        [SerializeField]
        private GameObject cornerWall;

        [SerializeField]
        private GameObject splitWall;

        [SerializeField]
        private GameObject intersectingWall;

        private int previousWallNeighbours = 0;

        private void OnEnable()
        {
            lonelyWall = Instantiate(lonelyWall, this.transform);
            normalWall = Instantiate(normalWallPrefabs.GetRandom(), this.transform);
            cornerWall = Instantiate(cornerWall, this.transform);
            splitWall = Instantiate(splitWall, this.transform);
            intersectingWall = Instantiate(intersectingWall, this.transform);

            cornerWall.transform.localPosition = Vector3.zero;

            turnOffEverything();

            setWall();
        }

        public override void UpdateNeighbourInfo(Directions direction, Tile tile)
        {
            base.UpdateNeighbourInfo(direction, tile);

            int currentWallNeighbours = getCurrentWallNeighbours();

            if (previousWallNeighbours != currentWallNeighbours)
            {
                previousWallNeighbours = currentWallNeighbours;
                setWall();
            }
        }

        private int getCurrentWallNeighbours()
        {
            int value = 0;
            if (this.NorthNeighbour is WallTile || this.NorthNeighbour is DoorTile) value++;
            if (this.SouthNeighbour is WallTile || this.SouthNeighbour is DoorTile) value++;
            if (this.WestNeighbour is WallTile || this.WestNeighbour is DoorTile) value++;
            if (this.EastNeighbour is WallTile || this.EastNeighbour is DoorTile) value++;
            return value;
        }

        private void turnOffEverything()
        {
            lonelyWall.SetActive(false);
            normalWall.SetActive(false);
            cornerWall.SetActive(false);
            splitWall.SetActive(false);
            intersectingWall.SetActive(false);
        }

        private void setWall()
        {
            int neighBours = getCurrentWallNeighbours();

            turnOffEverything();

            bool north = NorthNeighbour is WallTile || NorthNeighbour is DoorTile;
            bool south = SouthNeighbour is WallTile || SouthNeighbour is DoorTile;
            bool east = EastNeighbour is WallTile || EastNeighbour is DoorTile;
            bool west = WestNeighbour is WallTile || WestNeighbour is DoorTile;

            if (neighBours == 0)
            {
                this.lonelyWall.SetActive(true);
            }
            else if (neighBours == 1)
            {
                if (north || south)
                {
                    normalWall.SetActive(true);
                    normalWall.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (east || west)
                {
                    normalWall.SetActive(true);
                    normalWall.transform.eulerAngles = new Vector3(0, 90, 0);
                }
            }
            else if (neighBours == 2)
            {
                if (north && south)
                {
                    normalWall.SetActive(true);
                    normalWall.transform.eulerAngles = new Vector3(0, 90, 0);
                }
                else if (east && west)
                {
                    normalWall.SetActive(true);
                    normalWall.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (west && north)
                {
                    cornerWall.SetActive(true);
                    cornerWall.transform.eulerAngles = new Vector3(0, 180, 0);
                }
                else if (north && east)
                {
                    cornerWall.SetActive(true);
                    cornerWall.transform.eulerAngles = new Vector3(0, 270, 0);
                }
                else if (east && south)
                {
                    cornerWall.SetActive(true);
                }
                else
                {
                    cornerWall.SetActive(true);
                    cornerWall.transform.eulerAngles = new Vector3(0, 90, 0);
                }
            }
            else if (neighBours == 3)
            {
                splitWall.SetActive(true);

                if (!west)
                {
                    splitWall.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (!north)
                {
                    splitWall.transform.eulerAngles = new Vector3(0, -90, 0);
                }
                else if (!east)
                {
                    splitWall.transform.eulerAngles = new Vector3(0, -180, 0);
                }
                else if (!south)
                {
                    splitWall.transform.eulerAngles = new Vector3(0, -270, 0);
                }
            }
            else
            {
                intersectingWall.SetActive(true);
            }
        }
    }
}