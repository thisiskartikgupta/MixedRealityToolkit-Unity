// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.MixedReality.Toolkit.Physics;
using UnityEngine;

/// <summary>
/// Brings a hologram closer to the user.
/// </summary>
namespace Microsoft.MixedReality.Toolkit.Experimental.Accessibility {
    [RequireComponent(typeof(Interpolator))]
    public class BringCloser : MonoBehaviour
    {
        [Tooltip("The z-axis distance at which object will be moved to in front of Main camera.")]
        [Range(0.3f, 5.0f)]
        public float offset = 0.5f;

        private Vector3 positionOrigin;
        private Interpolator interpolator;

        private void Awake()
        {
            interpolator = gameObject.GetComponent<Interpolator>();
            
            interpolator.SmoothLerpToTarget = true;
        }

        /// <summary>
        /// Moves the hologram in front of the user's initial location.
        /// </summary>
        public void MoveCloser()
        {
            positionOrigin = gameObject.transform.position;
            Vector3 mainCamera = Camera.main.transform.position;
            mainCamera.z += offset;
            interpolator.SetTargetPosition(mainCamera);
            transform.LookAt(Camera.main.transform.position);
        }

        /// <summary>
        /// Moves the hologram back to the location before it was moved closer.
        /// </summary>
        public void MoveBack()
        {
            interpolator.SetTargetPosition(positionOrigin);
        }
    }
}