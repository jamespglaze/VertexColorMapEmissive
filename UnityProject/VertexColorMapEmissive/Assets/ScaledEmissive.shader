Shader "VertexColorMapEmissive/ScaledEmissive" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Map ("Map", 2D) = "white" {}
		_Brightness ("Brightness", Float) = 1
		_Transparency ("Transparency", Float) = 1
	}

	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 250
		Blend One One
	
		CGPROGRAM
		#pragma surface surf Lambert

		float _UseColor;
		sampler2D _Map;
		float _Brightness;
		float _Transparency;
		
		struct Input {
			float3 viewDir;
			float3 worldNormal;
			float2 uv_Map;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Emission = _Brightness * tex2D (_Map, IN.uv_Map) * tex2D (_Map, IN.uv_Map).a * _Transparency;
		}
		ENDCG
	}

	Fallback "Diffuse"
}