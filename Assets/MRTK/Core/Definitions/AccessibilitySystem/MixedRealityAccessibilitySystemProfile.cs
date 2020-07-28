// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Experimental.Accessibility
{
    [CreateAssetMenu(menuName = "Mixed Reality Toolkit/Profiles/Mixed Reality Accessibility System Profile", fileName = "MixedRealityToolkitConfigurationProfile", order = (int)CreateProfileMenuItemIndices.Configuration)]
    // todo [HelpURL("https://microsoft.github.io/MixedRealityToolkit-Unity/Documentation/MixedRealityConfigurationGuide.html")]
    public class MixedRealityAccessibilitySystemProfile : BaseMixedRealityProfile
    {
        [SerializeField]
        [Tooltip("Configuration objects describing the registered settings providers.")]
        private MixedRealityAccessibilityServiceConfiguration[] serviceConfigurations = new MixedRealityAccessibilityServiceConfiguration[0];

        /// <summary>
        /// Configuration objects describing the registered accessbility service providers.
        /// </summary>
        public MixedRealityAccessibilityServiceConfiguration[] ServiceConfigurations
        {
            get => serviceConfigurations;
            internal set => serviceConfigurations = value;
        }
    }
}