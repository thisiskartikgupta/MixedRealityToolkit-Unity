// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.﻿

using Microsoft.MixedReality.Toolkit.Editor;
using System.Linq;
using UnityEditor;

namespace Microsoft.MixedReality.Toolkit.Experimental.Accessibility.Editor
{
    [CustomEditor(typeof(MixedRealityMagnifierProfile))]
    public class MagnifierProfileInspector : BaseMixedRealityToolkitConfigurationProfileInspector
    {
        private const string ProfileTitle = "Magnifier Settings";
        private const string ProfileDescription = "";

        private SerializedProperty magnificationFactor;
        private SerializedProperty minDistance;

        protected override void OnEnable()
        {
            base.OnEnable();

            magnificationFactor = serializedObject.FindProperty("magnificationFactor");
            minDistance = serializedObject.FindProperty("minDistance");
        }

        public override void OnInspectorGUI()
        {
            if (!RenderProfileHeader(ProfileTitle, ProfileDescription, target))
            {
                return;
            }

            using (new EditorGUI.DisabledGroupScope(IsProfileLock((BaseMixedRealityProfile)target)))
            {
                serializedObject.Update();

                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(magnificationFactor);
                EditorGUILayout.PropertyField(minDistance);

                serializedObject.ApplyModifiedProperties();
            }
        }

        protected override bool IsProfileInActiveInstance()
        {
            var profile = target as BaseMixedRealityProfile;

            return MixedRealityToolkit.IsInitialized && profile != null &&
                   MixedRealityToolkit.Instance.HasActiveProfile &&
                   MixedRealityToolkit.Instance.ActiveProfile.AccessibilitySystemProfile != null &&
                   MixedRealityToolkit.Instance.ActiveProfile.AccessibilitySystemProfile.FeatureConfigurations != null &&
                   MixedRealityToolkit.Instance.ActiveProfile.AccessibilitySystemProfile.FeatureConfigurations.Any(s => s.FeatureProfile == profile);
        }
    }
}
