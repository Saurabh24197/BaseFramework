
Shader "Composited/Specular: Specular Reflection Test" {
	Properties { 
		//_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		//_SpecColor("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
		//_Shininess("Shininess", Float) = 10
	
	}

	SubShader{
		Tags { "Lightmode" = "ForwardBase"}

		Pass {
			CGPROGRAM
			#pragma vertex Vert
			#pragma fragment Frag

			////User Defined Variables
			//uniform float4 _Color;
			//uniform float4 _SpecColor;
			//uniform float _Shininess;

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
				float4 col : COLOR;
			};

			//Vertex Function
			VertexOutput Vert(VertexInput vIn)
			{
				VertexOutput vOut;
					
				//Vectors
				float3 normalDirection =  normalize( mul( float4(vIn.normal, 0.0), unity_WorldToObject).xyz );
				float3 viewDirection = normalize( float3( float4( _WorldSpaceCameraPos.xyz, 1.0) - UnityObjectToClipPos(vIn.vertex).xyz ));

				float3 lightDirection;
				float atten = 1.0;

				//Lighting
				lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				float3 diffuseReflection = atten * _LightColor0.xyz * max(0.0, dot(normalDirection, lightDirection));
				float3 specularReflection = reflect(-lightDirection, normalDirection);


				vOut.col = float4(specularReflection, 1.0);
				vOut.pos = UnityObjectToClipPos(vIn.vertex);
				return vOut;
			}

			//Fragment Function
			float4 Frag(VertexOutput i) : COLOR
			{
				return i.col;
			}

			ENDCG
		}
	}

	Fallback "Diffuse"
}