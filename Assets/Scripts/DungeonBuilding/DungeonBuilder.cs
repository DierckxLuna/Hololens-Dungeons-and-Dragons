using System.Collections.Generic;
using Assets.Scripts.DataManagement;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

namespace Assets.Scripts.DungeonBuilding
{
    public class DungeonBuilder : MonoBehaviour
    {
        public static DungeonBuilder Instance;

        public ITile[][] Map;

        [SerializeField]
        private GameObject groundPrefab;

        [SerializeField]
        private GameObject doorPrefab;

        [SerializeField]
        private GameObject wallPrefab;

        [SerializeField]
        private GameObject emptyPrefab;

        [SerializeField]
        private GameObject editButtonPrefab;

        [SerializeField]
        private float tileSize = 0.047625f;

        [SerializeField]
        private SharedBool inDecoratingMode;

        [SerializeField]
        private SharedBool inRotatingMode;

        [SerializeField]
        private SharedBool inEditingMode;

        [SerializeField]
        private GameObject editMenu;

        [SerializeField]
        private SharedString editIconName;

        [SerializeField]
        private GameObject handTracker;

        [SerializeField]
        public int MapSize = 50;

        private MapEditMode mapEditMode;

        private int[][] savedMap = new int[][]
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

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Starting!!!");
            if (Instance == null) Instance = this;

            inDecoratingMode.Value = false;

            inEditingMode.Value = false;

            Map = new ITile[50][];

            for (int i = 0; i < MapSize; i++)
            {
                Map[i] = new ITile[50];
            }

            for (int i = 0; i < savedMap.Length; i++)
            {
                for (int j = 0; j < savedMap[i].Length; j++)
                {
                    if (savedMap[i][j] == 1)
                    {
                        placeGround(i, j);
                    }
                    else if (savedMap[i][j] == 8)
                    {
                        placeDoor(i, j);
                    }
                    else if (savedMap[i][j] == 9)
                    {
                        placeWall(i, j);
                    }
                    else
                    {
                        placeEmpty(i, j);
                    }
                }
            }

            // adding edit buttons
            for(int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    if (Map[i][j] == null)
                    {
                        placeEmpty(i, j);
                    }

                    EditButton button = Instantiate(editButtonPrefab).GetComponent<EditButton>();

                    if (button != null)
                    {
                        EditButton editButton = button.GetComponent<EditButton>();
                        editButton.I = i;
                        editButton.J = j;

                        editButton.transform.position = this.transform.position + new Vector3(i * tileSize, 0, j * tileSize);
                        editButton.transform.localScale = new Vector3(tileSize * 0.75f, 0.025f, tileSize * 0.75f);
                    }
                    else
                    {
                        Debug.LogWarning($"This shouldn't be happening {i} {j}");
                    }
                    
                }
            }

            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    UpdateNeighbours(i, j);
                }
            }

            editMenu.SetActive(false);
            inEditingMode.OnChange += (bool val) => editMenu.SetActive(val);
        }

        public void ToggleDecoratingMode()
        {
            inDecoratingMode.Value = !inDecoratingMode.Value;
            inRotatingMode.Value = false;
            ExitEditMode();
        }

        public void ToggleRotatingMode()
        {
            inRotatingMode.Value = !inRotatingMode.Value;
            inDecoratingMode.Value = false;
            ExitEditMode();
        }

        public void ToggleEditMode()
        {
            inEditingMode.Value = !inEditingMode.Value;

            if (mapEditMode == MapEditMode.none && inEditingMode.Value)
            {
                GroundEditMode();
            }

            handTracker.SetActive(inEditingMode.Value);
        }

        public void ExitEditMode()
        {
            mapEditMode = MapEditMode.none;
            inEditingMode.Value = false;
        }

        public void WallEditMode()
        {
            mapEditMode = MapEditMode.wall;
            editIconName.Value = "IconFollowMe";
        }
        public void GroundEditMode() {
            mapEditMode = MapEditMode.ground;
            editIconName.Value = "IconSpatialMapping";
        }

        public void DoorEditMode() {
            mapEditMode = MapEditMode.door;
            editIconName.Value = "IconHome";
        }

        public void DeleteEditMode()
        {
            mapEditMode = MapEditMode.delete;
            inEditingMode.Value = true;
            editIconName.Value = "IconClose";
        }

        public void PlaceTile(int i, int j)
        {
            Debug.Log($"Setting tile {i} {j}");
            if (mapEditMode == MapEditMode.none) return;

            ITile tile;

            if (mapEditMode == MapEditMode.wall && Map[i][j].GetType() != typeof(WallTile))
            {
                Destroy(Map[i][j].gameObject);
                tile = placeWall(i, j);
            }
            else if (mapEditMode == MapEditMode.ground && Map[i][j].GetType() != typeof(GroundTile))
            {
                Destroy(Map[i][j].gameObject);
                tile = placeGround(i, j);
            }
            else if (mapEditMode == MapEditMode.door && Map[i][j].GetType() != typeof(DoorTile))
            {
                Destroy(Map[i][j].gameObject);
                tile = placeDoor(i, j);
            }
            else if (mapEditMode == MapEditMode.delete && Map[i][j].GetType() != typeof(EmptyTile))
            {
                Destroy(Map[i][j].gameObject);
                tile = placeEmpty(i, j);
            }
            else
            {
                return;
            }

            UpdateNeighbours(i, j);
        }

        private ITile placeGround(int i, int j)
        {
            ITile newTile = Instantiate(groundPrefab, transform).GetComponent<ITile>();

            Map[i][j] = newTile;

            setPositionAndScale(newTile.gameObject, i, j);

            return newTile;
        }

        private ITile placeDoor(int i, int j)
        {
            ITile newDoor = Instantiate(doorPrefab, transform).GetComponent<ITile>();

            Map[i][j] = newDoor;

            setPositionAndScale(newDoor.gameObject, i, j);

            return newDoor;
        }

        private ITile placeWall(int i, int j, bool notInitial = true)
        {
            ITile newWall = Instantiate(wallPrefab, transform).GetComponent<ITile>();

            Map[i][j] = newWall;

            setPositionAndScale(newWall.gameObject, i, j);

            return newWall;
        }

        private ITile placeEmpty(int i, int j, bool notInitial = true)
        {
            ITile newEmpty = Instantiate(emptyPrefab, transform).GetComponent<ITile>();

            Map[i][j] = newEmpty;

            return newEmpty;
        }

        private void setPositionAndScale(GameObject newTile, int i, int j)
        {
            if (newTile == null) return;

            newTile.transform.position = this.transform.position + new Vector3(i * tileSize, 0, j * tileSize);
            newTile.transform.localScale = new Vector3(tileSize, tileSize, tileSize);
        }

        private void UpdateNeighbours(int i, int j)
        {
            if (i > 0)
            {
                UpdateTile(i - 1, j);
            }

            if (i < MapSize - 1)
            {
                UpdateTile(i + 1, j);
            }

            if (j > 0)
            {
                UpdateTile(i, j - 1);
            }

            if (j < MapSize - 1)
            {
                UpdateTile(i, j + 1);
            }

            UpdateTile(i, j);
        }

        private void UpdateTile(int i, int j)
        {
            if (Map[i][j] is IWallJoinTile tile)
            {
                tile.SetJoints(
                    north: isWallJoinTall(i - 1, j),
                    south: isWallJoinTall(i + 1, j),
                    east: isWallJoinTall(i, j + 1),
                    west: isWallJoinTall(i, j - 1)
                );
            }
        }

        private bool isWallJoinTall(int i, int j) => i >= 0 && i < MapSize && j >= 0 && j < MapSize && Map[i][j] is IWallJoinTile;
    }

    public enum MapEditMode
    {
        none,
        delete,
        ground,
        wall,
        door
    }
}
