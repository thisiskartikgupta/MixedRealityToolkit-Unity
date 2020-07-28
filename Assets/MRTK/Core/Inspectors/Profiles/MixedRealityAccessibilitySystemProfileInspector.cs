// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.﻿

using Microsoft.MixedReality.Toolkit.Editor;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Experimental.Accessibility.Editor
{
    [CustomEditor(typeof(MixedRealityAccessibilitySystemProfile))]
    public class MixedRealityAccessibilitySystemProfileInspector : BaseMixedRealityToolkitConfigurationProfileInspector
    {
        // todo

        protected override bool IsProfileInActiveInstance()
        {
            var profile = target as BaseMixedRealityProfile;
            return MixedRealityToolkit.IsInitialized && profile != null &&
                   profile == MixedRealityToolkit.Instance.ActiveProfile.AccessibilitySystemProfile;
        }
    }
}
