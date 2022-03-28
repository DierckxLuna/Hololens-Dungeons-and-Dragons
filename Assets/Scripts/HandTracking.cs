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
            thumbTip.name = nameof(thumbTip);
            indexTip = Instantiate(fingertipPrefab, this.transform);
            indexTip.name = nameof(indexTip);
            middleTip = Instantiate(fingertipPrefab, this.transform);
            middleTip.name = nameof(middleTip);
            ringTip = Instantiate(fingertipPrefab, this.transform);
            ringTip.name = nameof(ringTip);
            pinkyTip = Instantiate(fingertipPrefab, this.transform);
            pinkyTip.name = nameof(pinkyTip);

            thumbTip.layer = Layers.ThumbTip;

            palm = Instantiate(palmPrefab, this.transform);
            palm.name = nameof(palm);
        }

        // Update is called once per frame
        void Update()
        {
            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Right, out pose))
            {
                thumbTip.SetActive(true);
                thumbTip.transform.position = pose.Position;
            }
            else
            {
                thumbTip.SetActive(false);
            }

            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out pose))
            {
                indexTip.SetActive(true);
                indexTip.transform.position = pose.Position;
            }
            else
            {
                indexTip.SetActive(false);
            }

            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Right, out pose))
            {
                middleTip.SetActive(true);
                middleTip.transform.position = pose.Position;
            }
            else
            {
                middleTip.SetActive(false);
            }

            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Right, out pose))
            {
                ringTip.SetActive(true);
                ringTip.transform.position = pose.Position;
            }
            else
            {
                ringTip.SetActive(false);
            }

            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Right, out pose))
            {
                pinkyTip.SetActive(true);
                pinkyTip.transform.position = pose.Position;
            }
            else
            {
                pinkyTip.SetActive(false);
            }

            if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Right, out pose))
            {
                palm.SetActive(true);
                palm.transform.position = pose.Position;
                palm.transform.rotation = pose.Rotation;
            }
            else
            {
                palm.SetActive(false);
            }
        }
    }
}
