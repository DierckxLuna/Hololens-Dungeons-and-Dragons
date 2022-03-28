using Assets.Scripts.DataManagement;
using UnityEngine;

namespace Assets.Scripts.DungeonBuilding
{
    public class EditButton : MonoBehaviour
    {
        public int I;

        public int J;

        [SerializeField]
        private SharedBool inEditingMode;

        private void Start()
        {
            inEditingMode.OnChange += toggleBoxCollider;
            toggleBoxCollider(inEditingMode.Value);
        }

        private void toggleBoxCollider(bool val) => this.gameObject.SetActive(val);

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.name == "indexTip")
            {
                DungeonBuilder.Instance.PlaceTile(I, J);
            }
        }
    }
}