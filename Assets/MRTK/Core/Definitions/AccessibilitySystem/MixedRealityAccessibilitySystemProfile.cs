// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Experimental.Accessibility
{
    [CreateAssetMenu(menuName = "Mixed Reality Toolkit/Profiles/Mixed Reality Accessibility System Profile", fileName = "MixedRealityToolkitAccessibilitySystemProfile", order = (int)CreateProfileMenuItemIndices.Configuration)]
    [MixedRealityServiceProfile(typeof(IMixedRealityAccessibilitySystem))]
    // todo [HelpURL("https://microsoft.github.io/MixedRealityToolkit-Unity/Documentation/MixedRealityConfigurationGuide.html")]
    public class MixedRealityAccessibilitySystemProfile : BaseMixedRealityProfile
    {
        [SerializeField]
        [Tooltip("Configuration objects describing the registered feature providers.")]
        private MixedRealityAccessibilityFeatureConfiguration[] featureConfigurations = new MixedRealityAccessibilityFeatureConfiguration[0];

        /// <summary>
        /// Configuration objects describing the registered accessbility feature providers.
        /// </summary>
        public MixedRealityAccessibilityFeatureConfiguration[] FeatureConfigurations
        {
            get => featureConfigurations;
            internal set => featureConfigurations = value;
        }
    }
}