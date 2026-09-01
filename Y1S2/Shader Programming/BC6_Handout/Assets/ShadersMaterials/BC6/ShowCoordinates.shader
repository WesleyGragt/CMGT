Shader "Unlit/ShowCoordinates"
{
	Properties
	{
		_Tint("CoordinateFilter", Vector) = (1,1,1)
		_Type("TransformationType", float) = 0
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 position : SV_POSITION;
				float4 space : TEXCOORD0;
			};

			float3 _Tint;
			float _Type;

			v2f vert(appdata vertex)
			{
				v2f o;
				
				float4x4 MVP = UNITY_MATRIX_MVP;
				o.position = mul(MVP, vertex.position);
				
				if (_Type == 0) {
					o.space = vertex.position; //Object
					}
				else  if (_Type == 1) {
					o.space = mul(UNITY_MATRIX_M, vertex.position); //World
					}
				else if (_Type == 2) {
					o.space = mul(UNITY_MATRIX_MV, vertex.position); //Camera
					}

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// TODO: add different ways to display the coordinates
				return abs(fmod(i.space,1)) * float4(_Tint,1); 
			}
			ENDCG
		}
	}
}