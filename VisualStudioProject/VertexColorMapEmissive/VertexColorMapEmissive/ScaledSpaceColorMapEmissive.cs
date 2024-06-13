
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
using Kopernicus.ConfigParser.Interfaces;
using Kopernicus.Configuration.Parsing;
using Kopernicus.Components;

namespace VertexColorMapEmissive
{
    [ParserTargetExternal("ScaledVersion", "VertexColorMapEmissive", "Kopernicus")]
    public class ScaledSpaceColorMapEmissive : BaseLoader, IParserEventSubscriber
    {
        private Material EmissiveMaterial;

        [ParserTarget("map")]
        private Texture2DParser map
        {
            get { return (Texture2D)EmissiveMaterial.GetTexture("_Map"); }
            set { EmissiveMaterial.SetTexture("_Map", value); }
        }

        [ParserTarget("brightness")]
        private NumericParser<Single> brightness
        {
            get { return EmissiveMaterial.GetFloat("_Brightness"); }
            set { EmissiveMaterial.SetFloat("_Brightness", value); }
        }

        [ParserTarget("transparency")]
        private NumericParser<Single> transparency
        {
            get { return EmissiveMaterial.GetFloat("_Transparency"); }
            set { EmissiveMaterial.SetFloat("_Transparency", value); }
        }

        void IParserEventSubscriber.Apply(ConfigNode node)
        {
            EmissiveMaterial = new Material(Kopernicus.Components.ShaderLoader.GetShader("VertexColorMapEmissive/ScaledEmissive"));
            EmissiveMaterial.SetFloat("_Brightness", 1f);
            EmissiveMaterial.SetFloat("_Transparency", 0.5f);

            Material sharedMaterial = generatedBody.scaledVersion.GetComponent<Renderer>().sharedMaterial;
            EmissiveMaterial.SetTexture("_Map", sharedMaterial.GetTexture("_MainTex"));
        }

        // Post Apply Event
        void IParserEventSubscriber.PostApply(ConfigNode node)
        {
            Renderer scaledRenderer = generatedBody.scaledVersion.GetComponent<Renderer>();
            Material surfaceMaterial = scaledRenderer.sharedMaterial;
            scaledRenderer.materials = new Material[] { surfaceMaterial, EmissiveMaterial };
        }
    }
}
