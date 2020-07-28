// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Experimental.Accessibility
{
    /// <summary>
    /// Base class used to derive custom accessibility service profiles.
    /// </summary>
    [Serializable]
    public class BaseAccessibilityServiceProfile : BaseMixedRealityProfile
    {
        [EnumFlags]
        [SerializeField]
        [Tooltip("Flags describing which accessibility feature categories (ex: vision) that are provided by this service.")]
        private AccessibilityFeatureCategory category = AccessibilityFeatureCategory.None;

        /// <summary>
        /// Flags describing which accessibility feature categories (ex: vision) that are provided by this service.
        /// </summary>
        public AccessibilityFeatureCategory Category
        {
            get => category;
            protected set => category = value;
        }
    }
}