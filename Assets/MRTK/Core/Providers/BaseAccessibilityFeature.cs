// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Experimental.Accessibility
{
    /// <summary>
    /// Base class that implements an IMixedRealityAccessibilitySystem feature.
    /// </summary>
    public class BaseAccessibilityFeature : BaseDataProvider<IMixedRealityAccessibilitySystem>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="system">The system that is managing this feature.</param>
        /// <param name="name">The name of the feature.</param>
        /// <param name="priority">The feature priority.</param>
        /// <param name="profile">Profile containing feature configuration settings.</param>
        public BaseAccessibilityFeature(
            IMixedRealityAccessibilitySystem system,
            string name,
            uint priority,
            BaseAccessibilityFeatureProfile profile) : base(system, name, priority, profile)
        { }

        #region IMixedRealityService

        /// <inheritdoc />
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <inheritdoc />
        public override void Reset()
        {
            base.Reset();
            Disable();
            Initialize();
            Enable();
        }

        /// <inheritdoc />
        public override void Enable()
        {
            base.Enable();
        }

        /// <inheritdoc />
        public override void Disable()
        {
            base.Disable();
        }

        /// <inheritdoc />
        public override void Update()
        {
            base.Update();
        }

        /// <inheritdoc />
        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        /// <inheritdoc />
        public override void Destroy()
        {
            base.Destroy();
        }

        #endregion // IMixedRealityService
    }
}
