using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

namespace Assets.Scripts
{
    public class HandTracking : MonoBehaviour
    {
        [SerializeField]
        private GameObject fingertipPrefab;

        [SerializeField]
        private GameObject palmPrefab;

        private GameObject thumbTip;
        private GameObject indexTip;
        private GameObject middleTip;
        private GameObject ringTip;
        private GameObject pinkyTip;
        private GameObject palm;

        MixedRealityPose pose;

        // Start is called before the first frame update
        void Start()
        {
            thumbTip = Instantiate(fingertipPrefab, this.transform);
            indexTip = Instantiate(fingertipPrefab, this.transform);
            middleTip = Instantiate(fingertipPrefab, this.transform);
            ringTip = Instantiate(fingertipPrefab, this.transform);
            pinkyTip = Instantiate(fingertipPrefab, this.transform);

            thumbTip.layer = Layers.ThumbTip;

            palm = Instantiate(palmPrefab, this.transform);
        }

        // Update is called once per frame
        void Update()
        {
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Right, out pose))
            {
                thumbTip.transform.position = pose.Position;
            }

            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out pose))
            {
                indexTip.transform.position = pose.Position;
            }

            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Right, out pose))
            {
                middleTip.transform.position = pose.Position;
            }

            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Right, out pose))
            {
                ringTip.transform.position = pose.Position;
            }

            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Right, out pose))
            {
                pinkyTip.transform.position = pose.Position;
            }

            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Right, out pose))
            {
                palm.transform.position = pose.Position;
                palm.transform.rotation = pose.Rotation;
            }
        }
    }
}
