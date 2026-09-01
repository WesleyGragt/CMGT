Shader "Unlit/WoodRings"
{
	Properties
	{
		_Ambient("Ambient", Float) = 0
		_RingColor("Ring Color", Color) = (1, 1, 1, 1)
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
				float4 normal : NORMAL;
				//float2 uv : TEXCOORD0; // not needed

				float4 object : TEXCOORD0;
				float4 camera : TEXCOORD1;
			};

			struct v2f
			{
				//float2 uv : TEXCOORD0; // not needed
				float4 position : SV_POSITION;
				float4 normal : NORMAL;

				float4 object : TEXCOORD0;
				float4 camera : TEXCOORD1;
			};

			float _Ambient;
			float4 _RingColor;

			v2f vert(appdata vertex)
			{
				v2f o;
				o.position = UnityObjectToClipPos(vertex.position);
				
				o.object = vertex.position;
				o.camera = mul(UNITY_MATRIX_MV, vertex.position);

				o.normal = vertex.normal;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				//  Depending on the object space position, add a wood pattern

				float dist = sqrt(pow(i.object.x, 2) + pow(i.object.y, 2));

				//  Depending on the camera space position, fade in a fog color

				float4 col = float4(0.5,0.5,0,1) * dist;
				return sin (dist * 25); // fmod is modulo (%)

				col *= (_Ambient + (1 - _Ambient) * saturate(i.normal.y)); // VERY basic lighting. TODO LATER: improve

				return col;
			}
			ENDCG
		}
	}
}
