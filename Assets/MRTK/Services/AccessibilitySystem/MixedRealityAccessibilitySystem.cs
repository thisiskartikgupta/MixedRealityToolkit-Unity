// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Microsoft.MixedReality.Toolkit.Experimental.Accessibility
{
    /// <summary>
    /// Class providing the default implementation of the <see cref="IMixedRealityAccessibilitySystem"/> interface.
    /// </summary>
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

        #endregion // IMixedRealityAccessibilitySystem

        #region IMixedRealityService

        /// <inheritdoc/>
        public override string Name { get; protected set; } = "Mixed Reality Accessibility System";

        /// <inheritdoc/>
        public override void Initialize()
        {
            base.Initialize();
            RegisterFeatureProviders();
        }

        /// <inheritdoc/>
        public override void Reset()
        {
            base.Reset();
            Disable();
            UnregisterFeatureProviders();
            Initialize();
            Enable();
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

        /// <inheritdoc/>
        public override void Destroy()
        {
            UnregisterFeatureProviders();
            base.Destroy();
        }

        /// <summary>
        /// Registers and initializes the configured accessibility feature providers.
        /// </summary>
        private void RegisterFeatureProviders()
        {
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

        /// <summary>
        /// Unregisters the current accessibility feature providers.
        /// </summary>
        private void UnregisterFeatureProviders()
        {
            foreach (IMixedRealityAccessibilityFeature feature in GetDataProviders<IMixedRealityAccessibilityFeature>())
            {
                UnregisterDataProvider(feature);
            }
        }

        #endregion // IMixedRealityService

    }
}
