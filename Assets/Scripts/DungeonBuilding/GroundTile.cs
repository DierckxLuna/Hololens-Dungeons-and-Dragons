using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.DungeonBuilding
{
    public class GroundTile : MonoBehaviour, ITile
    {
        private Decoration decoration;

        public GameObject unit; // this should be changed to a unit class at some point. Also this should be private but ¯\_(ツ)_/¯

        [SerializeField]
        protected List<GameObject> prefabs = new List<GameObject>();

        public void Awake()
        {
            if (prefabs.Count == 0) throw new ArgumentException(nameof(prefabs));

            GameObject tile = Instantiate(prefabs.GetRandom(), this.transform);
            tile.transform.Rotate(new Vector3(0, UnityEngine.Random.Range(0, 4) * 90, 0));
            this.decoration = tile.GetComponent<Decoration>();
        }

        public bool IsNavigable
        {
            get {
                return unit == null && this.decoration.Empty; // I don't want to deal with halflings technically being able to run between peoples legs if they're too tall but if I did it'd go here with a unit input.
            }
        }
    }
}
