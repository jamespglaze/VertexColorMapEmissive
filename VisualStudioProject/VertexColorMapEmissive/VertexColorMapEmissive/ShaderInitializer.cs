using System;
using UnityEngine;
using Kopernicus.Components;

namespace VertexColorMapEmissive
{
        [KSPAddon(KSPAddon.Startup.Instantly, true)]
        public class ShaderInitializer : MonoBehaviour
        {
            void Awake()
            {
                ShaderLoader.LoadAssetBundle("001_DuckweedUtils/VertexColorMapEmissive/Shaders", "VertexColorMapEmissive");
            }
        }
}
