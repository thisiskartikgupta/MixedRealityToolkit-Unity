// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.MixedReality.Toolkit.Experimental.Accessibility
{
    public interface IMixedRealityAccessibilitySystem : IMixedRealityEventSystem
    {
        /// <summary>
        /// Typed representation of the ConfigurationProfile property.
        /// </summary>
        MixedRealityAccessibilitySystemProfile AccessibilityProfile { get; }
    }
}
