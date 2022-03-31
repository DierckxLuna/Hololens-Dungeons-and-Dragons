using UnityEngine;

namespace Assets.Scripts.DungeonBuilding
{
    public interface ITile
    {
        public GameObject gameObject { get; }

        public bool IsNavigable { get; }
    }
}