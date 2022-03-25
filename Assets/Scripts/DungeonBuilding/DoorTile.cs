using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DungeonBuilding
{
    public class DoorTile : Tile
    {
        [SerializeField]
        private GameObject door;

        [SerializeField]
        private GameObject ground;

        private void OnEnable()
        {
            door = Instantiate(door, this.transform);
            ground = Instantiate(ground, this.transform);

            setDoor();
        }

        private void setDoor()
        {
            turnOffEverything();

            bool north = NorthNeighbour is WallTile || NorthNeighbour is DoorTile;
            bool south = SouthNeighbour is WallTile || SouthNeighbour is DoorTile;
            bool east = EastNeighbour is WallTile || EastNeighbour is DoorTile;
            bool west = WestNeighbour is WallTile || WestNeighbour is DoorTile;

            if (getCurrentWallNeighbours() == 2)
            {
                if (north && south)
                {
                    door.SetActive(true);
                    door.transform.eulerAngles = new Vector3(0, 0, 0);
                    return;
                }

                if (east && west)
                {
                    door.SetActive(true);
                    door.transform.eulerAngles = new Vector3(0, 90, 0);
                }
            }

            ground.SetActive(true);
        }

        private void turnOffEverything()
        {
            door.SetActive(false);
            ground.SetActive(true);
        }

        public override void UpdateNeighbourInfo(Directions direction, Tile tile)
        {
            base.UpdateNeighbourInfo(direction, tile);

            setDoor();
        }

        private int getCurrentWallNeighbours()
        {
            int value = 0;
            if (this.NorthNeighbour is WallTile) value++;
            if (this.SouthNeighbour is WallTile) value++;
            if (this.WestNeighbour is WallTile) value++;
            if (this.EastNeighbour is WallTile) value++;
            return value;
        }
    }
}