// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Composited/Ambient" 
{
	Properties {
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
	}

	SubShader {
		Pass {

			Tags { "LightMode" = "ForwardBase" }
			CGPROGRAM
			#pragma vertex Vert
			#pragma fragment Frag

			//User-Def Variables
			uniform float4 _Color;
			
			//Unity Defined Variables
			uniform float4 _LightColor0;
			
			//Unity 3 Definitions below
			//float4x4 _Object2World;
			//float4x4 _World2Object;
			//float4 _WorldSpaceLightPos0;

			//Base Input Structs : Vertex
			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;

			};

			struct VertexOutput
			{
				float4 pos : SV_POSITION;
				float4 col : COLOR;
			};

			//Vertex Functions
			VertexOutput Vert(VertexInput vIn)
			{
				VertexOutput vOut;
				
				float3 normalDirection = normalize( mul( float4(vIn.normal, 0.0), unity_WorldToObject).xyz);
				float3 lightDirection;
				float atten = 1.0f;

				lightDirection = normalize( _WorldSpaceLightPos0.xyz);

				float3 diffuseReflection = atten * _LightColor0.xyz * max(0.0, dot(normalDirection, lightDirection));
				//float3 diffuseReflection = atten * _LightColor0.xyz * abs( dot(normalDirection, lightDirection));
				float3 lightFinal = diffuseReflection + UNITY_LIGHTMODEL_AMBIENT.xyz;

				vOut.col = float4(lightFinal * _Color.rgb, 1.0);
				vOut.pos = UnityObjectToClipPos(vIn.vertex);

				return vOut;
			}

			//Fragment Functions
			float4 Frag(VertexOutput i) : Color
			{
				return i.col;
			}
			ENDCG
		}
	}

	Fallback "Diffuse"
}