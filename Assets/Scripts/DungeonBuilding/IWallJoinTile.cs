using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DungeonBuilding
{
    public interface IWallJoinTile : ITile
    {
        public void SetJoints(bool north, bool south, bool east, bool west);
    }
}