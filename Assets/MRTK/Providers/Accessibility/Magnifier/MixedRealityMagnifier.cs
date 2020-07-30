// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Experimental.Accessibility
{
    /// <summary>
    /// Class providing the default implementation of the <see cref="IMixedRealityMagnifier"/> interface.
    /// </summary>
    [MixedRealityDataProvider(
        typeof(IMixedRealityAccessibilitySystem),
        (SupportedPlatforms)(-1),
        "Mixed Reality Magnifier",
        "Accessibility/Magnifier/Profiles/DefaultMixedRealityMagnifierProfile.asset",
        "MixedRealityToolkit.Providers")]
    public class MixedRealityMagnifier : BaseAccessibilityFeature, IMixedRealityMagnifier
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="system">The system managing this feature.</param>
        /// <param name="name">The name of this feature.</param>
        /// <param name="priority">The feature priority.</param>
        /// <param name="profile">The profile that specifies the feature configuration settings.</param>
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

        private float magFactorMin = 1.0f;
        private float magFactorMax = 5.0f;

        /// <inheritdoc />
        public float MagnificationFactor
        {
            get => magnificationFactor;

            set
            {
                if (magnificationFactor != value)
                {
                    if ((value < magFactorMin) || (value > magFactorMax))
                    {
                        Debug.LogError($"Invalid MagnificationFactor. Valid values must be within {magFactorMin} and {magFactorMax}, inclusive");
                        return;
                    }

                    magnificationFactor = value;
                }
            }
        }

        private float distanceMin = 0.0f;
        private float distanceMax = 10.0f;
        private float minDistance = 0.3f;

        /// <inheritdoc />
        public float MinimumDistance
        {
            get => minDistance;

            set
            {
                if (minDistance != value)
                {
                    if ((value < distanceMin) || (value > distanceMax))
                    {
                        Debug.LogError($"Invalid MinimumDistance. Valid values must be within {distanceMin} and {distanceMax}, inclusive");
                        return;
                    }

                    minDistance = value;
                }
            }
        }

        /// <inheritdoc />
        public bool IsRunning { get; set; }

        /// <inheritdoc />
        public GameObject MagnifiedObject { get; private set; }

        /// <inheritdoc />
        public void Suspend()
        {
            IsRunning = false;
            suspendedByDisable = false;

            // Return the target hologram to the original scale.
            if (MagnifiedObject != null)
            {
                MagnifiedObject.transform.localScale /= MagnificationFactor;
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
                        MagnifiedObject.transform.localScale /= MagnificationFactor;
                    }
                }

                // Check to see if the user is gazing at a new object.
                if (focusedObject != null)
                {
                    focusedObject.transform.localScale *= MagnificationFactor;
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
