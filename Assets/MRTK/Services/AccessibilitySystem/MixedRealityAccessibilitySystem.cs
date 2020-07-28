// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Experimental.Accessibility
{
    public class MixedRealityAccessibilitySystem : BaseDataProviderAccessCoreSystem, IMixedRealityAccessibilitySystem
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="profile">The configuration profile for the service.</param>
        public MixedRealityAccessibilitySystem(
            MixedRealityAccessibilitySystemProfile profile) : base(profile)
        { }

        #region IMixedRealityAccessibilitySystem

        /// <inheritdoc/>
        public MixedRealityAccessibilitySystemProfile AccessibilityProfile => ConfigurationProfile as MixedRealityAccessibilitySystemProfile;

        // todo

        #endregion // IMixedRealityAccessibilitySystem

        #region IMixedRealityService

        /// <inheritdoc/>
        public override string Name { get; protected set; } = "Mixed Reality Accessibility System";

        /// <inheritdoc/>
        public override void Initialize()
        {
            base.Initialize();
            // todo
        }

        /// <inheritdoc/>
        public override void Reset()
        {
            base.Reset();
            // todo
        }

        /// <inheritdoc/>
        public override void Update()
        {
            base.Update();
            // todo
        }

        /// <inheritdoc/>
        public override void Enable()
        {
            base.Enable();
            // todo
        }

        /// <inheritdoc/>
        public override void Disable()
        {
            // todo
            base.Disable();
        }

        public override void Destroy()
        {
            // todo
            base.Destroy();
        }

        #endregion // IMixedRealityService

    }
}
