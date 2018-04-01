
Shader "Composited/Specular - Pixel Lit" {
	Properties {
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_SpecColor("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Shininess("Shininess", Float) = 10

	}

	SubShader{
		Tags { "Lightmode" = "ForwardBase"}
		Pass {
			CGPROGRAM
			#pragma vertex Vert
			#pragma fragment Frag
			#include "UnityCG.cginc"

			//User Defined Variables
			uniform float4 _Color;
			uniform float4 _SpecColor;
			uniform float _Shininess;

			//Unity Defined Variables
			uniform float4 _LightColor0;

			//Structs
			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct VertexOutput
			{
				float4 pos : SV_POSITION;
				float4 posWorld : TEXCOORD0;
				float3 normalDir : TEXCOORD1;
			};

			//Vertex Function
			VertexOutput Vert(VertexInput vIn)
			{
				VertexOutput vOut;

				vOut.posWorld = mul(unity_ObjectToWorld, vIn.vertex);
				vOut.normalDir = normalize(mul(float4(vIn.normal, 0.0), unity_WorldToObject).xyz);
				vOut.pos = UnityObjectToClipPos(vIn.vertex);

				return vOut;
			}

			//Fragment Function
			float4 Frag(VertexOutput i) : COLOR
			{

				//Vectors
				float3 normalDirection = i.normalDir; 
				float3 viewDirection = normalize( _WorldSpaceCameraPos.xyz - i.posWorld.xyz); 
				float atten = 1.0;

				//Lighting
				float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				float3 diffuseReflection = atten * _LightColor0.xyz * max(0.0, dot(normalDirection, lightDirection));
				float3 specularReflection = atten * _SpecColor.rgb * max(0.0, dot(normalDirection, lightDirection)) * pow(max(0.0, dot(reflect(-lightDirection, normalDirection), viewDirection)), _Shininess);
				float3 lightFinal = diffuseReflection + specularReflection + UNITY_LIGHTMODEL_AMBIENT;

				return float4(lightFinal * _Color.rgb, 1.0);
			}

			ENDCG
		}
	}

	Fallback "Diffuse"
}