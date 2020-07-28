// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.MixedReality.Toolkit.Experimental.Accessibility
{
    /// <summary>
    /// Interface defining the an accessibility service feature.
    /// </summary>
    public interface IMixedRealityAccessibilityFeature : IMixedRealityDataProvider
    {
        /// <summary>
        /// Flags describing which accessibility feature categories (ex: vision) that are provided by this service.
        /// </summary>
        AccessibilityFeatureCategory Category { get; }
    }
}
