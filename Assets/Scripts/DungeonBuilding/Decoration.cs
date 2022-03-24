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

        public List<GameObject> decorations = new List<GameObject>();

        private Interactable interactible;

        private int activeDecoration = 0;

        private void Awake()
        {
            if (editButton == null)
            {
                throw new ArgumentNullException(nameof(editButton));
            }

            inDecoratingMode.OnChange += activateEditButton;
        }

        private void Start()
        {
            activateEditButton(inDecoratingMode.Value);

            for (int i = 0; i < decorationPrefabs.Count; i++)
            {
                decorations.Add(Instantiate(decorationPrefabs[i], this.transform));
                decorations[i].SetActive(false);
            }

            decorations[activeDecoration].SetActive(true);

            interactible = editButton.GetComponent<Interactable>();

            interactible.OnClick.AddListener(this.ChangeDecoration);
        }

        private void ChangeDecoration()
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