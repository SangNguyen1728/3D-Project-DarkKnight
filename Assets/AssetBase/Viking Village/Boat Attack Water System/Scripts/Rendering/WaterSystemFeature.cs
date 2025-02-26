using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UnityEngine.Rendering.Universal
{
    /// <summary>
    /// Class for render target handles in URP.
    /// Updated to use RTHandle instead of deprecated RenderTargetHandle.
    /// </summary>
    public struct RenderTargetWrapper
    {
        /// <summary>
        /// The RTHandle instance.
        /// </summary>
        private RTHandle rtHandle;

        /// <summary>
        /// The render target handle for the Camera target.
        /// </summary>
        public static readonly RTHandle CameraTarget = null;

        /// <summary>
        /// Constructor for a render target handle.
        /// </summary>
        /// <param name="rtHandle">The RTHandle for the new handle.</param>
        public RenderTargetWrapper(RTHandle rtHandle)
        {
            this.rtHandle = rtHandle;
        }

        internal static RTHandle GetCameraTarget(ref CameraData cameraData)
        {
#if ENABLE_VR && ENABLE_XR_MODULE
            if (cameraData.xr.enabled)
                return cameraData.xr.renderTarget;
#endif
            return cameraData.renderer.cameraColorTargetHandle;
        }

        /// <summary>
        /// Initializes the RTHandle.
        /// </summary>
        /// <param name="shaderProperty">The shader property to initialize with.</param>
        public void Init(string shaderProperty)
        {
            rtHandle = RTHandles.Alloc(shaderProperty);
        }

        /// <summary>
        /// Initializes the RTHandle with a RenderTargetIdentifier.
        /// </summary>
        /// <param name="renderTargetIdentifier">The render target ID to initialize with.</param>
        public void Init(RenderTargetIdentifier renderTargetIdentifier)
        {
            rtHandle = RTHandles.Alloc(renderTargetIdentifier);
        }

        /// <summary>
        /// The render target ID for this handle.
        /// </summary>
        /// <returns>The render target ID.</returns>
        public RenderTargetIdentifier Identifier()
        {
            return rtHandle.nameID;
        }

        /// <summary>
        /// Equality check with another render target wrapper.
        /// </summary>
        /// <param name="other">Other render target wrapper to compare with.</param>
        /// <returns>True if the handles are equal.</returns>
        public bool Equals(RenderTargetWrapper other)
        {
            return rtHandle == other.rtHandle;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is RenderTargetWrapper && Equals((RenderTargetWrapper)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return rtHandle != null ? rtHandle.GetHashCode() : 0;
        }

        /// <summary>
        /// Equality check between two render target wrappers.
        /// </summary>
        public static bool operator ==(RenderTargetWrapper c1, RenderTargetWrapper c2)
        {
            return c1.Equals(c2);
        }

        /// <summary>
        /// Inequality check between two render target wrappers.
        /// </summary>
        public static bool operator !=(RenderTargetWrapper c1, RenderTargetWrapper c2)
        {
            return !c1.Equals(c2);
        }
    }
}
