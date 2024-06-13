using HarmonyLib;
using Kopernicus.ShadowMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static PQS;

namespace VertexColorMapEmissive
{
    public class PQSMod_VertexColorMapEmissive : PQSMod
    {
        public Material EmissiveMaterial;

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
