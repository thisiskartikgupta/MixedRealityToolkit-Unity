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
    public class MixedRealityAccessibilitySystemProfileInspector : BaseDataProviderServiceInspector
    {
        private const string ProfileTitle = "Accessibility System Settings";
        private const string ProfileDescription = "The Accessibility System profile allows developers to configure the accessibility features available to the application.";

        private static readonly GUIContent AddFeatureContent = new GUIContent("+ Add Feature", "Add Accessibility Feature");
        private static readonly GUIContent RemoveFeatureContent = new GUIContent("-", "Remove Accessibility Feature");

        /// <inheritdoc/>
        public override void OnInspectorGUI()
        {
            if (!RenderProfileHeader(ProfileTitle, ProfileDescription, target))
            {
                return;
            }

            using (new EditorGUI.DisabledGroupScope(IsProfileLock((BaseMixedRealityProfile)target)))
            {
                serializedObject.Update();

                using (new EditorGUI.IndentLevelScope())
                {
                    RenderDataProviderList(AddFeatureContent, RemoveFeatureContent, "", typeof(BaseAccessibilityFeatureProfile));
                }

                serializedObject.ApplyModifiedProperties();
            }
        }

        protected override bool IsProfileInActiveInstance()
        {
            var profile = target as BaseMixedRealityProfile;
            return MixedRealityToolkit.IsInitialized && profile != null &&
                   profile == MixedRealityToolkit.Instance.ActiveProfile.AccessibilitySystemProfile;
        }

        #region DataProvider inspector utilities

        /// <inheritdoc/>
        protected override SerializedProperty GetDataProviderConfigurationList()
        {
            return serializedObject.FindProperty("featureConfigurations");
        }

        /// <inheritdoc/>
        protected override ServiceConfigurationProperties GetDataProviderConfigurationProperties(SerializedProperty providerEntry)
        {
            return new ServiceConfigurationProperties()
            {
                componentName = providerEntry.FindPropertyRelative("componentName"),
                componentType = providerEntry.FindPropertyRelative("componentType"),
                providerProfile = providerEntry.FindPropertyRelative("featureProfile"),
                runtimePlatform = providerEntry.FindPropertyRelative("runtimePlatform"),
            };
        }

        /// <inheritdoc/>
        protected override IMixedRealityServiceConfiguration GetDataProviderConfiguration(int index)
        {
            var configurations = (target as MixedRealityAccessibilitySystemProfile)?.FeatureConfigurations;
            if (configurations != null && index >= 0 && index < configurations.Length)
            {
                return configurations[index];
            }

            return null;
        }

        #endregion // DataProvider inspector utilities
    }
}
