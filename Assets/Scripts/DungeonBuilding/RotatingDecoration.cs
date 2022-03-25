using Assets.Scripts.DataManagement;
using Microsoft.MixedReality.Toolkit.UI;
using System;
using UnityEngine;

namespace Assets.Scripts.DungeonBuilding
{
    public class RotatingDecoration : Decoration
    {
        [SerializeField]
        private SharedBool inRotatingMode;

        [SerializeField]
        private GameObject rotateButton;

        protected override void Awake()
        {
            base.Awake();

            if (rotateButton == null)
            {
                throw new ArgumentNullException(nameof(rotateButton));
            }

            inRotatingMode.OnChange += activateRotateButton;
        }

        protected override void Start()
        {
            base.Start();

            activateRotateButton(inRotatingMode.Value);

            rotateButton.GetComponent<Interactable>().OnClick.AddListener(RotateSelected);
        }

        public void RotateSelected() => decorations[activeDecoration].transform.Rotate(0, 90, 0);

        private void OnDestroy()
        {
            inRotatingMode.OnChange -= activateRotateButton;
        }

        private void activateRotateButton(bool active) => rotateButton.SetActive(active);
    }
}