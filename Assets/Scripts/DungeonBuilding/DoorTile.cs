using UnityEngine;

namespace Assets.Scripts.DungeonBuilding
{
    public class DoorTile : MonoBehaviour, IWallJoinTile
    {
        [SerializeField]
        private GameObject door;

        [SerializeField]
        private GameObject ground;

        public bool IsNavigable => true;

        private void OnEnable()
        {
            door = Instantiate(door, this.transform);
            ground = Instantiate(ground, this.transform);
        }

        public void SetJoints(bool north, bool south, bool east, bool west)
        {
            turnOffEverything();

            if (north && south)
            {
                door.SetActive(true);
                door.transform.eulerAngles = new Vector3(0, 90, 0);
                return;
            }

            if (east && west)
            {
                door.SetActive(true);
                door.transform.eulerAngles = new Vector3(0, 0, 0);
                return;
            }

            ground.SetActive(true);
        }

        private void turnOffEverything()
        {
            door.SetActive(false);
            ground.SetActive(false);
        }
    }
}