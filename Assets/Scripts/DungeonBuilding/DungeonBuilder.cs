using System.Collections.Generic;
using Assets.Scripts.DataManagement;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

namespace Assets.Scripts.DungeonBuilding
{
    public class DungeonBuilder : MonoBehaviour
    {
        public static DungeonBuilder Instance;

        public Tile[][] Map;

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

            Map = new Tile[50][];

            for (int i = 0; i < MapSize; i++)
            {
                Map[i] = new Tile[50];
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

                    GameObject button = Instantiate(editButtonPrefab);

                    if (button != null)
                    {
                        button.GetComponentInChildren<Interactable>().OnClick.AddListener(() => PlaceTile(i, j));

                        button.transform.position = this.transform.position + new Vector3(i * tileSize, 0, j * tileSize);
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
                    UpdateNeighbours(i, j, Map[i][j]);
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

            Destroy(Map[i][j].gameObject);

            Tile tile;

            switch (mapEditMode)
            {
                case MapEditMode.wall:
                    tile = placeWall(i, j);
                    break;
                case MapEditMode.ground:
                    tile = placeGround(i, j);
                    break;
                case MapEditMode.door:
                    tile = placeDoor(i, j);
                    break;
                default:
                    tile = placeEmpty(i, j);
                    break;
            }

            UpdateNeighbours(i, j, tile);
        }

        private Tile placeGround(int i, int j)
        {
            Tile newTile = Instantiate(groundPrefab, transform).GetComponent<Tile>();

            Map[i][j] = newTile;

            setPositionAndScale(newTile.gameObject, i, j);

            return newTile;
        }

        private Tile placeDoor(int i, int j)
        {
            Tile newDoor = Instantiate(doorPrefab, transform).GetComponent<Tile>();

            Map[i][j] = newDoor;

            setPositionAndScale(newDoor.gameObject, i, j);

            return newDoor;
        }

        private Tile placeWall(int i, int j, bool notInitial = true)
        {
            Tile newWall = Instantiate(wallPrefab, transform).GetComponent<Tile>();

            Map[i][j] = newWall;

            setPositionAndScale(newWall.gameObject, i, j);

            return newWall;
        }

        private Tile placeEmpty(int i, int j, bool notInitial = true)
        {
            Tile newEmpty = Instantiate(emptyPrefab, transform).GetComponent<Tile>();

            Map[i][j] = newEmpty;

            return newEmpty;
        }

        private void setPositionAndScale(GameObject newTile, int i, int j)
        {
            if (newTile == null) return;

            newTile.transform.position = this.transform.position + new Vector3(i * tileSize, 0, j * tileSize);
            newTile.transform.localScale = new Vector3(tileSize, tileSize, tileSize);
        }

        private void UpdateNeighbours(int i, int j, Tile tile)
        {
            if (i > 0)
            {
                tile.UpdateNeighbourInfo(Directions.West, Map[i - 1][j]);
            }
            else
            {
                tile.UpdateNeighbourInfo(Directions.West, Instantiate(emptyPrefab).GetComponent<Tile>());
            }

            if (i < MapSize - 1)
            {
                tile.UpdateNeighbourInfo(Directions.East, Map[i + 1][j]);
            }
            else
            {
                tile.UpdateNeighbourInfo(Directions.East, Instantiate(emptyPrefab).GetComponent<Tile>());
            }

            if (j > 0)
            {
                tile.UpdateNeighbourInfo(Directions.South, Map[i][j - 1]);
            }
            else
            {
                tile.UpdateNeighbourInfo(Directions.South, Instantiate(emptyPrefab).GetComponent<Tile>());
            }

            if (j < MapSize - 1)
            {
                tile.UpdateNeighbourInfo(Directions.North, Map[i][j + 1]);
            }
            else
            {
                tile.UpdateNeighbourInfo(Directions.North, Instantiate(emptyPrefab).GetComponent<Tile>());
            }
        }
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
