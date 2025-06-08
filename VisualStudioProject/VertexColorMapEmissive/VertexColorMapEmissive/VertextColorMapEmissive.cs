using System;
using UnityEngine;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
using Kopernicus.Configuration.ModLoader;
using Kopernicus.Configuration.Parsing;
using Kopernicus.ConfigParser.Enumerations;
using Kopernicus.ConfigParser.Interfaces;
using static VertexColorMapEmissive.PQSMod_VertexColorMapEmissive;

namespace VertexColorMapEmissive
{
    [RequireConfigType(ConfigType.Node)]
    public class VertexColorMapEmissive : ModLoader<PQSMod_VertexColorMapEmissive>, IParserEventSubscriber
    {

        [ParserTarget("blendMode")]
        public EnumParser<BlendModes> BlendMode
        {
            get { return Mod.blendMode; }
            set
            {
                Mod.blendMode = value.Value;
                if (
                    Mod.blendMode == BlendModes.Additive)
                {
                    Mod.EmissiveMaterial = new Material(Kopernicus.Components.ShaderLoader.GetShader("VertexColorMapEmissive/PQSEmissive"));
                }
                else
                {
                    Mod.EmissiveMaterial = new Material(Kopernicus.Components.ShaderLoader.GetShader("VertexColorMapEmissive/PQSEmissiveOld"));
                }
            }
        }

        [ParserTarget("map")]
        public Texture2DParser EmissiveMap
        {
            get { return (Texture2D)Mod.EmissiveMaterial.GetTexture("_Map"); }
            set { Mod.EmissiveMaterial.SetTexture("_Map", value); }
        }

        [ParserTarget("brightness")]
        public NumericParser<Single> Brightness
        {
            get { return Mod.EmissiveMaterial.GetFloat("_Brightness"); }
            set { Mod.EmissiveMaterial.SetFloat("_Brightness", value); }
        }

        [ParserTarget("transparency")]
        public NumericParser<Single> Transparency
        {
            get { return Mod.EmissiveMaterial.GetFloat("_Transparency"); }
            set { Mod.EmissiveMaterial.SetFloat("_Transparency", value); }
        }

        void IParserEventSubscriber.Apply(ConfigNode node)
        {
            Mod.EmissiveMaterial = new Material(Kopernicus.Components.ShaderLoader.GetShader("VertexColorMapEmissive/PQSEmissiveOld"));
        }

        void IParserEventSubscriber.PostApply(ConfigNode node) { }
    }
}
