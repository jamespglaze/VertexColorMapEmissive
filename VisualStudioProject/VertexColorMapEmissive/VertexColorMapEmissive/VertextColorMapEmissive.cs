using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Kopernicus.Components;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
using Kopernicus.Configuration.ModLoader;
using Kopernicus.Configuration.Parsing;
using Kopernicus.ConfigParser.Enumerations;
using Kopernicus.ConfigParser.Interfaces;

namespace VertexColorMapEmissive
{
    [RequireConfigType(ConfigType.Node)]
    public class VertexColorMapEmissive : ModLoader<PQSMod_VertexColorMapEmissive>, IParserEventSubscriber
    {
        [ParserTarget("map")]
        public Texture2DParser EmissiveMap
        {
            get { return (Texture2D)Mod.EmissiveMaterial.GetTexture("_Map") ; }
            set { Mod.EmissiveMaterial.SetTexture("_Map", value); }
        }

        [ParserTarget("brightness")]
        public NumericParser<Single> PQSbrightness
        {
            get { return Mod.EmissiveMaterial.GetFloat("_Brightness"); }
            set { Mod.EmissiveMaterial.SetFloat("_Brightness", value); }
        }

        [ParserTarget("transparency")]
        public NumericParser<Single> PQStransparency
        {
            get { return Mod.EmissiveMaterial.GetFloat("_Transparency"); }
            set { Mod.EmissiveMaterial.SetFloat("_Transparency", value); }
        }

        void IParserEventSubscriber.Apply(ConfigNode node)
        {
            Mod.EmissiveMaterial = new Material(ShaderLoader.GetShader("VertexColorMapEmissive/PQSEmissive"));
            Mod.EmissiveMaterial.SetFloat("_Brightness", 1f);
            Mod.EmissiveMaterial.SetFloat("_Transparency", 0.5f);
        }

        void IParserEventSubscriber.PostApply(ConfigNode node) { }
    }
}
