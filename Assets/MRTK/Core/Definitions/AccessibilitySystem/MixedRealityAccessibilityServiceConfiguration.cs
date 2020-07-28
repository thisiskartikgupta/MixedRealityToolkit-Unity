// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.﻿

using Microsoft.MixedReality.Toolkit.Utilities;
using System;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Experimental.Accessibility
{
    /// <summary>
    /// Defines the configuration for a camera settings provider.
    /// </summary>
    [Serializable]
    public struct MixedRealityAccessibilityServiceConfiguration : IMixedRealityServiceConfiguration
    {
        [SerializeField]
        [Tooltip("The concrete type of the camera settings provider.")]
        [Implements(typeof(IMixedRealityAccessibilityService), TypeGrouping.ByNamespaceFlat)]
        private SystemType componentType;

        /// <inheritdoc />
        public SystemType ComponentType => componentType;

        [SerializeField]
        [Tooltip("The name of the accessibility service.")]
        private string componentName;

        /// <inheritdoc />
        public string ComponentName => componentName;

        [SerializeField]
        [Tooltip("The accessibility service priority.")]
        private uint priority;

        /// <inheritdoc />
        public uint Priority => priority;

        [SerializeField]
        [Tooltip("The platform(s) on which the accessibility service is supported.")]
        [EnumFlags]
        private SupportedPlatforms runtimePlatform;

        /// <inheritdoc />
        public SupportedPlatforms RuntimePlatform => runtimePlatform;

        [SerializeField]
        private BaseAccessibilityServiceProfile serviceProfile;

        /// <inheritdoc />
        public BaseMixedRealityProfile Profile => serviceProfile;

        /// <summary>
        /// Camera settings specific configuration profile.
        /// </summary>
        public BaseAccessibilityServiceProfile ServiceProfile => serviceProfile;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="componentType">The <see cref="Microsoft.MixedReality.Toolkit.Utilities.SystemType"/> of the provider.</param>
        /// <param name="componentName">The friendly name of the provider.</param>
        /// <param name="priority">The load priority of the provider.</param>
        /// <param name="runtimePlatform">The runtime platform(s) supported by the provider.</param>
        /// <param name="settingsProfile">The configuration profile for the provider.</param>
        public MixedRealityAccessibilityServiceConfiguration(
            SystemType componentType,
            string componentName,
            uint priority,
            SupportedPlatforms runtimePlatform,
            BaseAccessibilityServiceProfile configurationProfile)
        {
            this.componentType = componentType;
            this.componentName = componentName;
            this.priority = priority;
            this.runtimePlatform = runtimePlatform;
            this.serviceProfile = configurationProfile;
        }
    }
}
