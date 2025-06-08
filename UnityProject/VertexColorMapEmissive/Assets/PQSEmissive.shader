Shader "VertexColorMapEmissive/PQSEmissive" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Map ("Map", 2D) = "white" {}
		_Brightness ("Brightness", Float) = 1
		_Transparency ("Transparency", Float) = 1
	}

	SubShader {
		Offset -1, -1
        Pass
        {
			Tags { "Queue"="Transparent" "RenderType"="Transparent" }
			LOD 250
			Blend One One

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#define PI 3.1415926535897932384626
			sampler2D _Map;
			float _Brightness;
			float _Transparency;
			float4x4 _WorldtoPlanet;

			struct appdata_t {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
				float4 texcoord2 : TEXCOORD1;
				float3 tangent : TANGENT;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float3 sphereNormal : TEXCOORD5;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.sphereNormal = -(float4(v.texcoord.x, v.texcoord.y, v.texcoord2.x, v.texcoord2.y)).xyz;
				return o;
			}


			fixed4 frag (v2f IN) : COLOR
			{
				half4 color;

				float3 cubeVectNorm = normalize(IN.sphereNormal);
				float4 uv;
				uv.x = .5 + (atan2(cubeVectNorm.x, cubeVectNorm.z) / (PI * 2));
				uv.y = acos(cubeVectNorm.y)/PI;
				uv.zw = float2(0, 0);
				color = tex2Dlod (_Map, uv);

				color.rgb = _Brightness * color.rgb * _Transparency * color.a;
				
				return color;
			}
			ENDCG
		}
	}
}