
using System;
using UnityEngine;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
using Kopernicus.ConfigParser.Interfaces;
using Kopernicus.Configuration.Parsing;

namespace VertexColorMapEmissive
{
    [ParserTargetExternal("ScaledVersion", "VertexColorMapEmissive", "Kopernicus")]
    public class ScaledSpaceColorMapEmissive : BaseLoader, IParserEventSubscriber
    {
        private Material EmissiveMaterial;

        public enum BlendModes
        {
            Additive,
            AlphaBlend,
        }

        public BlendModes blendMode = BlendModes.AlphaBlend;

        [ParserTarget("blendMode")]
        public EnumParser<BlendModes> BlendMode
        {
            get { return blendMode; }
            set
            {
                blendMode = value.Value;
                if (blendMode == BlendModes.Additive)
                {
                    EmissiveMaterial = new Material(Kopernicus.Components.ShaderLoader.GetShader("VertexColorMapEmissive/ScaledEmissive"));
                }
                else
                {
                    EmissiveMaterial = new Material(Kopernicus.Components.ShaderLoader.GetShader("VertexColorMapEmissive/ScaledEmissiveOld"));
                }
            }
        }

        [ParserTarget("map")]
        private Texture2DParser Map
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
            EmissiveMaterial = new Material(Kopernicus.Components.ShaderLoader.GetShader("VertexColorMapEmissive/ScaledEmissiveOld"));
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
