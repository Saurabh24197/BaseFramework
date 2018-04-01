// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Composited/Flat Color"
{
	Properties {
		_Color("Color", Color) = (0, 0, 0, 0)
	}

	SubShader {
		Pass {
			CGPROGRAM
			//Pragmas
			#pragma vertex Vert
			#pragma fragment Frag

			//User deifned Variables
			uniform float4 _Color;
		
			//Base Input Structures
			struct VertexInput
			{
				float4 vertex : POSITION;
			};

			struct VertexOutput
			{
				float4 pos : SV_POSITION;
			};

			//Vertex Functions
			VertexOutput Vert(VertexInput vIn)
			{
				VertexOutput vOut;
				vOut.pos = UnityObjectToClipPos(vIn.vertex);
				return vOut;
			}

			//Fragment Functions
			float4 Frag(VertexOutput i) : Color
			{
				return _Color;
			}

			ENDCG
		}
	}

	//Fallback runs when none of the above shaders can be run.
	//The Pink Material is executed when there is no Fallback.
	Fallback "Diffuse"
}