using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DungeonBuilding
{
    public class EmptyTile : MonoBehaviour, ITile
    {
        public bool IsNavigable => false;
    }
}