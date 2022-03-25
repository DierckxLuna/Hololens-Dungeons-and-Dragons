using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DungeonBuilding
{
    public class TileManager : MonoBehaviour
    {
        

        [SerializeField]
        private GameObject ground;

        [SerializeField]
        private GameObject door;

        [SerializeField]
        private GameObject wall;

        private Tile activeTile;

        public Tile ActiveTile => activeTile;

        private void Awake()
        {
            ground = Instantiate(ground, this.transform);
            door = Instantiate(door, this.transform);
            wall = Instantiate(wall, this.transform);

            ground.SetActive(false);
            door.SetActive(false);
            wall.SetActive(false);

            activeTile = null;
        }

        public void ActivateTile(TileType tileType)
        {
            if (tileType == TileType.ground)
            {
                ground.SetActive(true);
                activeTile = ground.GetComponent<Tile>();
            }
            else if (tileType == TileType.door)
            {
                door.SetActive(true);
                activeTile = ground.GetComponent<Tile>();
            }
            else if (tileType == TileType.wall)
            {
                wall.SetActive(true);
                activeTile = ground.GetComponent<Tile>();
            }
            else
            {
                activeTile = null;
            }
        }

        public enum TileType
        {
            none,
            ground,
            door,
            wall
        }
    }
}