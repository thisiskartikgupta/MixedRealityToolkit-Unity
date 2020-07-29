// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Experimental.Accessibility
{
    public class MixedRealityAccessibilitySystem : BaseDataProviderAccessCoreSystem, IMixedRealityAccessibilitySystem
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="profile">The configuration profile for the service.</param>
        public MixedRealityAccessibilitySystem(
            MixedRealityAccessibilitySystemProfile profile) : base(profile)
        { }

        #region IMixedRealityAccessibilitySystem

        /// <inheritdoc/>
        public MixedRealityAccessibilitySystemProfile AccessibilityProfile => ConfigurationProfile as MixedRealityAccessibilitySystemProfile;

        // todo

        #endregion // IMixedRealityAccessibilitySystem

        #region IMixedRealityService

        /// <inheritdoc/>
        public override string Name { get; protected set; } = "Mixed Reality Accessibility System";

        /// <inheritdoc/>
        public override void Initialize()
        {
            base.Initialize();

            if (AccessibilityProfile != null && GetDataProviders<IMixedRealityAccessibilityFeature>().Count == 0)
            {
                // Register the spatial observers.
                for (int i = 0; i < AccessibilityProfile.FeatureConfigurations.Length; i++)
                {
                    MixedRealityAccessibilityFeatureConfiguration configuration = AccessibilityProfile.FeatureConfigurations[i];
                    object[] args = { this, configuration.ComponentName, configuration.Priority, configuration.FeatureProfile };

                    RegisterDataProvider<IMixedRealityAccessibilityFeature>(
                        configuration.ComponentType.Type,
                        configuration.ComponentName,
                        configuration.RuntimePlatform,
                        args);
                }
            }
        }

        /// <inheritdoc/>
        public override void Reset()
        {
            base.Reset();
            // todo - this needs to be smart.
            // create a data provider cleanup method that can be called here and in destroy
        }

        /// <inheritdoc/>
        public override void Enable()
        {
            base.Enable();
            IReadOnlyList<IMixedRealityAccessibilityFeature> features = GetDataProviders<IMixedRealityAccessibilityFeature>();
            for (int i = 0; i < features.Count; i++)
            {
                features[i].Enable();
            }
        }

        /// <inheritdoc/>
        public override void Disable()
        {
            IReadOnlyList<IMixedRealityAccessibilityFeature> features = GetDataProviders<IMixedRealityAccessibilityFeature>();
            for (int i = 0; i < features.Count; i++)
            {
                features[i].Disable();
            }
            base.Disable();
        }

        public override void Destroy()
        {
            foreach (IMixedRealityAccessibilityFeature feature in GetDataProviders<IMixedRealityAccessibilityFeature>())
            {
                UnregisterDataProvider(feature);
            }
            base.Destroy();
        }

        #endregion // IMixedRealityService

    }
}
