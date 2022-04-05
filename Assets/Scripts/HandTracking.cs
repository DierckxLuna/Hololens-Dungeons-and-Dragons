using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

namespace Assets.Scripts
{
    public class HandTracking : MonoBehaviour
    {
        [SerializeField]
        private GameObject fingertipPrefab;

        private GameObject indexTip;


        MixedRealityPose pose;

        // Start is called before the first frame update
        void Start()
        {
            indexTip = Instantiate(fingertipPrefab, this.transform);
            indexTip.name = nameof(indexTip);
        }

        // Update is called once per frame
        void Update()
        {
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out pose))
            {
                indexTip.SetActive(true);
                indexTip.transform.position = pose.Position;
            }
            else
            {
                indexTip.SetActive(false);
            }
        }
    }
}
