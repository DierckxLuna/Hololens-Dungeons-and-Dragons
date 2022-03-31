using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.DungeonBuilding
{
    public class WallTile : MonoBehaviour, IWallJoinTile
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

        private GameObject activeGameObject;

        public bool IsNavigable => false;

        private void OnEnable()
        {
            lonelyWall = Instantiate(lonelyWall, this.transform);
            normalWall = Instantiate(normalWallPrefabs.GetRandom(), this.transform);
            cornerWall = Instantiate(cornerWall, this.transform);
            splitWall = Instantiate(splitWall, this.transform);
            intersectingWall = Instantiate(intersectingWall, this.transform);

            cornerWall.transform.localPosition = Vector3.zero;

            turnOffEverything();
        }

        public void SetJoints(bool north, bool south, bool east, bool west)
        {
            int neighBours = CountBools(north, south, east, west);

            if (neighBours == 0)
            {
                Activeate(this.lonelyWall);
            }
            else if (neighBours == 1)
            {
                if (north || south)
                {
                    Activeate(normalWall);
                    normalWall.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (east || west)
                {
                    Activeate(normalWall);
                    normalWall.transform.eulerAngles = new Vector3(0, 90, 0);
                }
            }
            else if (neighBours == 2)
            {
                if (north && south)
                {
                    Activeate(normalWall);
                    normalWall.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (east && west)
                {
                    Activeate(normalWall);
                    normalWall.transform.eulerAngles = new Vector3(0, 90, 0);
                }
                else if (west && north)
                {
                    Activeate(cornerWall);
                    cornerWall.transform.eulerAngles = new Vector3(0, 90, 0);
                }
                else if (north && east)
                {
                    Activeate(cornerWall);
                    cornerWall.transform.eulerAngles = new Vector3(0, 180, 0);
                }
                else if (east && south)
                {
                    Activeate(cornerWall);
                    cornerWall.transform.eulerAngles = new Vector3(0, 270, 0);
                }
                else
                {
                    Activeate(cornerWall);
                    cornerWall.transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }
            else if (neighBours == 3)
            {
                Activeate(splitWall);

                if (!west)
                {
                    splitWall.transform.eulerAngles = new Vector3(0, 180, 0);
                }
                else if (!north)
                {
                    splitWall.transform.eulerAngles = new Vector3(0, 270, 0);
                }
                else if (!east)
                {
                    splitWall.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (!south)
                {
                    splitWall.transform.eulerAngles = new Vector3(0, 90, 0);
                }
            }
            else
            {
                Activeate(intersectingWall);
            }
        }

        private void turnOffEverything()
        {
            lonelyWall.SetActive(false);
            normalWall.SetActive(false);
            cornerWall.SetActive(false);
            splitWall.SetActive(false);
            intersectingWall.SetActive(false);
        }

        private void Activeate(GameObject gameObject)
        {
            activeGameObject?.SetActive(false);

            activeGameObject = gameObject;

            activeGameObject?.SetActive(true);
        }

        private int CountBools(params bool[] args) => args.Count(t => t);
    }
}