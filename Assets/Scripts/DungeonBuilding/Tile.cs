using UnityEngine;

namespace Assets.Scripts.DungeonBuilding
{
    public abstract class Tile : MonoBehaviour
    {
        [SerializeField]
        public virtual bool IsNavigable => false;

        protected Tile NorthNeighbour = null;

        protected Tile SouthNeighbour = null;

        protected Tile EastNeighbour = null;

        protected Tile WestNeighbour = null;

        public virtual void UpdateNeighbourInfo(Directions direction, Tile tile)
        {
            if (direction == Directions.North) NorthNeighbour = tile;
            else if (direction == Directions.South) SouthNeighbour = tile;
            else if (direction == Directions.West) WestNeighbour = tile;
            else EastNeighbour = tile;
        }
    }
}