using Smooth.Collections;
using System;
using System.Linq;
using UnityEngine;
using static PQS;

namespace VertexColorMapEmissive
{
    public class PQSMod_VertexColorMapEmissive : PQSMod
    {
        public Material EmissiveMaterial;

        public enum BlendModes
        {
            Additive,
            AlphaBlend,
        }

        public BlendModes blendMode = BlendModes.AlphaBlend;

        public override void OnPostSetup()
        {
            sphere.useSharedMaterial = true;
            EmissiveMaterial.renderQueue = sphere.surfaceMaterial.renderQueue + 10;
            sphere.materialsForUpdates.Add(EmissiveMaterial);
        }

        public override void OnQuadBuilt(PQ quad)
        {
            if (quad.sphereRoot != sphere)
                return;
            Material[] sharedMaterials = quad.meshRenderer.sharedMaterials;
            quad.meshRenderer.sharedMaterials = sharedMaterials.Append(EmissiveMaterial).ToArray();
        }

        public override void OnQuadDestroy(PQ quad)
        {
            quad.meshRenderer.sharedMaterials = new Material[] { quad.meshRenderer.sharedMaterials[0] };
        }
    }
}
