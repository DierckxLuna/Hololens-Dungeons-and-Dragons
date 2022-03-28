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
        }

        private void toggleBoxCollider(bool val) => this.gameObject.SetActive(val);

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"Something's touching me {collision.gameObject.name}");
            DungeonBuilder.Instance.PlaceTile(I, J);
        }
    }
}