using System;
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
        }

        public override void OnQuadBuilt(PQ quad)
        {
            if (quad.sphereRoot != sphere)
                return;
            Material surfaceMaterial = quad.meshRenderer.sharedMaterial;
            Material emissiveMaterial = new Material(EmissiveMaterial)
            {
                renderQueue = surfaceMaterial.renderQueue + 10
            };
            quad.meshRenderer.sharedMaterials = new Material[] { surfaceMaterial, emissiveMaterial };
        }

        public override void OnQuadDestroy(PQ quad)
        {
            quad.meshRenderer.sharedMaterials = new Material[] { quad.meshRenderer.sharedMaterials[0] };
        }
    }
}
