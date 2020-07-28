// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.MixedReality.Toolkit.Experimental.Accessibility
{
    /// <summary>
    /// Interface defining the a camera system settings provider.
    /// </summary>
    public interface IMixedRealityAccessibilityService : IMixedRealityDataProvider
    {
        /// <summary>
        /// Flags describing which accessibility feature categories (ex: vision) that are provided by this service.
        /// </summary>
        AccessibilityFeatureCategory Category { get; }
    }
}
