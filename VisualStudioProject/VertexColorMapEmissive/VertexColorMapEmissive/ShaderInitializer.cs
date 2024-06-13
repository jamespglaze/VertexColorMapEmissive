using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
