// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;

namespace Microsoft.MixedReality.Toolkit.Experimental.Accessibility
{
    /// <summary>
    /// Base class used to derive custom accessibility system feature profiles.
    /// </summary>
    [Serializable]
    public class BaseAccessibilityFeatureProfile : BaseMixedRealityProfile
    {
        // This class exists to allow for type safety and expansion.
        // Currently, there are no parameters that are in common for all accessibility features.
    }
}