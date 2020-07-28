// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;

namespace Microsoft.MixedReality.Toolkit.Experimental.Accessibility
{
    /// <summary>
    /// Flags that can be applied to a service to indicate that it supports one or more
    /// accessibility feature categories.
    /// </summary>
    [Flags]
    public enum AccessibilityFeatureCategory
    {
        /// <summary>
        /// This feature does not apply to any accessibility category.
        /// </summary>
        None = 0,               // 0x00000000

        /// <summary>
        /// A feature addressing the user's ability to see.
        /// </summary>
        Vision = 1 << 0,        // 0x00000001

        /// <summary>
        /// A feature addressing the user's ability to hear.
        /// </summary>
        Hearing = 1 << 1,       // 0x00000002

        /// <summary>
        /// A feature addressing the user's mobility.
        /// </summary>
        Mobility = 1 << 2,      // 0x00000004

        /// <summary>
        /// A cognitive accessibility feature (ex: emotion recognition).
        /// </summary>
        Cognitive = 1 << 3,     // 0x00000008

        /// <summary>
        /// Feature applies to an accessibility category that has not been defined.
        /// </summary>
        Other = 1 << 31         // 0x8000000
    }
}
