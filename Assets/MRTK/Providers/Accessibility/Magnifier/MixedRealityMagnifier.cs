// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Experimental.Accessibility
{
    /// <summary>
    /// 
    /// </summary>
    [MixedRealityDataProvider(
        typeof(IMixedRealityAccessibilitySystem),
        (SupportedPlatforms)(-1),
        "Mixed Reality Magnifier",
        "Accessibility/Magnifier/Profiles/DefaultMixedRealityMagnifierProfile.asset",
        "MixedRealityToolkit.Providers")]
    public class MixedRealityMagnifier : BaseAccessibilityFeature, IMixedRealityMagnifier
    {
        public MixedRealityMagnifier(
            IMixedRealityAccessibilitySystem system,
            string name,
            uint priority,
            MixedRealityMagnifierProfile profile = null) : base(system, name, priority, profile)
        { }

        /// <summary>
        /// Reads the contents of the MixedRealityMagnifierProfile.
        /// </summary>
        private void ReadProfile()
        {
            if (MagnifierProfile == null)
            {
                Debug.LogError($"{Name} requires a configuration profile to run properly.");
                return;
            }

            MagnificationFactor = MagnifierProfile.MagnificationFactor;
            MinimumDistance = MagnifierProfile.MinimumDistance;
        }

        #region IMixedRealityAccessibilityFeature

        /// <inheritdoc/>
        public MixedRealityMagnifierProfile MagnifierProfile => ConfigurationProfile as MixedRealityMagnifierProfile;

        /// <inheritdoc />
        public AccessibilityFeatureCategory Category => AccessibilityFeatureCategory.Vision | AccessibilityFeatureCategory.Mobility;

        #endregion // IMixedRealityAccessibilityFeature

        #region IMixedRealityMagnifier

        private float magnificationFactor;

        private AutoStartBehavior startupBehavior = AutoStartBehavior.AutoStart;

        /// <inheritdoc />
        public AutoStartBehavior StartupBehavior
        {
            get => startupBehavior;
            set => startupBehavior = value;
        }

        /// <inheritdoc />
        public float MagnificationFactor
        {
            get => magnificationFactor;

            set
            {
                if (magnificationFactor != value)
                {
                    // todo: enforce upper bound
                    if (value < 1.0f)
                    {
                        Debug.LogError($"Invalid MagnificationFactor. Valid values must be greater than or equal to 1.0");
                        return;
                    }

                    magnificationFactor = value;
                }
            }
        }

        private float minDistance = 0.3f;

        /// <inheritdoc />
        public float MinimumDistance
        {
            get => minDistance;
            // todo enforce range
            set => minDistance = value;
        }

        private bool isRunning = false;

        /// <inheritdoc />
        public bool IsRunning
        {
            get => isRunning;
            private set => isRunning = value;
        }

        private GameObject magnifiedObject = null;

        /// <inheritdoc />
        public GameObject MagnifiedObject
        {
            get => magnifiedObject;
            private set => magnifiedObject = value;
        }

        /// <inheritdoc />
        public void Suspend()
        {
            IsRunning = false;
            suspendedByDisable = false;

            // Return the target hologram to the original scale.
            if (MagnifiedObject != null)
            {
                MagnifiedObject.transform.localScale = Vector3.one;
            }
        }

        /// <inheritdoc />
        public void Resume()
        {
            IsRunning = true;
        }

        #endregion // IMixedRealityMagnifier

        #region IMixedRealityService

        /// <inheritdoc />
        public override void Initialize()
        {
            base.Initialize();
            ReadProfile();
            if (StartupBehavior == AutoStartBehavior.AutoStart)
            {
                Resume();
            }
        }

        /// <inheritdoc />
        public override void Enable()
        {
            base.Enable();
            if (!IsRunning &&
                (StartupBehavior == AutoStartBehavior.AutoStart)
                && suspendedByDisable)
            {
                // Resume after we were disabled.
                Resume();
            }
        }

        /// <inheritdoc />
        public override void Update()
        {
            base.Update();

            if (IsRunning)
            {
                GameObject focusedObject = CoreServices.InputSystem?.GazeProvider?.GazeTarget;

                // Check to see if the user's gaze remains on the magnified object.
                // This same condition holds if there is no magnified object and the user's gaze is not on an object.
                if (MagnifiedObject == focusedObject) 
                { 
                    // There is nothing to do.
                    return; 
                }

                // Check to see if the user's gaze has left the magnified object
                if ((focusedObject == null) ||
                    (MagnifiedObject != focusedObject))
                {
                    if (MagnifiedObject != null)
                    {
                        // Restore the magnified object's scale.
                        MagnifiedObject.transform.localScale = Vector3.one;
                    }
                }

                // Check to see if the user is gazing at a new object.
                if (focusedObject != null)
                {
                    focusedObject.transform.localScale = new Vector3(magnificationFactor, magnificationFactor, magnificationFactor);
                }

                MagnifiedObject = focusedObject;
            }
        }

        private bool suspendedByDisable = false;

        /// <inheritdoc />
        public override void Disable()
        {
            if (IsRunning)
            {
                Suspend();
                suspendedByDisable = true;
            }
            base.Disable();
        }

        #endregion // IMixedRealityService
    }
}
