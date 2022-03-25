using System;
using System.Collections.Generic;
using Assets.Scripts.DataManagement;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

namespace Assets.Scripts.DungeonBuilding
{
    public class Decoration : MonoBehaviour
    {
        [SerializeField]
        private SharedBool inDecoratingMode;

        [SerializeField]
        private GameObject editButton;

        [SerializeField]
        private List<GameObject> decorationPrefabs;

        protected List<GameObject> decorations = new List<GameObject>();

        protected int activeDecoration = 0;

        protected virtual void Awake()
        {
            if (editButton == null)
            {
                throw new ArgumentNullException(nameof(editButton));
            }

            inDecoratingMode.OnChange += activateEditButton;
        }

        protected virtual void Start()
        {
            activateEditButton(inDecoratingMode.Value);

            for (int i = 0; i < decorationPrefabs.Count; i++)
            {
                decorations.Add(Instantiate(decorationPrefabs[i], this.transform));
                decorations[i].SetActive(false);
            }

            editButton.GetComponent<Interactable>().OnClick.AddListener(ChangeDecoration);
        }

        public bool Empty => this.activeDecoration == 0;

        public void ChangeDecoration()
        {
            decorations[activeDecoration].SetActive(false);
            activeDecoration = activeDecoration + 1 < decorations.Count ? activeDecoration + 1 : 0;
            decorations[activeDecoration].SetActive(true);
        }

        private void OnDestroy()
        {
            inDecoratingMode.OnChange -= activateEditButton;
        }

        private void activateEditButton(bool active) => editButton.SetActive(active);
    }
}