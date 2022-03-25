using Assets.Scripts.DataManagement;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

namespace Assets.Scripts.DungeonBuilding
{
    public class EditButton : MonoBehaviour
    {
        [SerializeField]
        private GameObject button;

        [SerializeField]
        private SharedBool inEditMode;

        [SerializeField]
        private SharedString iconName;

        private ButtonConfigHelper configHelper;

        public int I;

        public int J;

        private void Start()
        {
            inEditMode.OnChange += enableButton;
            iconName.OnChange += updateIcon;
            configHelper = button.GetComponent<ButtonConfigHelper>();
            enableButton(inEditMode.Value);
        }

        private void OnDestroy()
        {
            inEditMode.OnChange -= enableButton;
            iconName.OnChange -= updateIcon;
        }



        private void enableButton(bool val) => button.SetActive(val);

        private void updateIcon(string val) => configHelper.SetQuadIconByName(val);
    }
}