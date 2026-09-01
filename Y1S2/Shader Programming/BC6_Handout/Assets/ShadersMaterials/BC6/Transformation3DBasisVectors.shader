Shader "Unlit/Transformation3DBasisVectors"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Ambient("Ambient", Float) = 0
		_Right("RightVector", Vector) = (1,0,0,0)
		_Up("UpVector", Vector) = (0,1,0,0)
		_Forward("Forward", Vector) = (0,0,1,0)
		_Translation("Translation", Vector) = (0,0,0,0)
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
				float4 vertex : POSITION;
				float4 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 normal : NORMAL;
			};

			sampler2D _MainTex;
			float _Ambient;

			// Use these:
			float4 _Right;
			float4 _Up;
			float4 _Forward;
			float4 _Translation;

			v2f vert(appdata v)
			{
				v2f o;
				// TODO: use _Right, _Up, _Forward and _Translation to transform this object 
				//   from object to world space:

				//v = ap + bq + cr

				float4 world = _Right * v.vertex.x + _Up * v.vertex.y + _Forward * v.vertex.z + _Translation;



				// Take it the rest of the way (camera, clip, etc.):
				world.w = 1;
				o.vertex = mul(UNITY_MATRIX_VP, world);
				o.normal = v.normal;
				o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col =
					tex2D(_MainTex, i.uv) *
					(_Ambient + (1 - _Ambient) * saturate(i.normal.y)); // VERY basic lighting. TODO LATER: improve
				return col;
			}
			ENDCG
		}
	}
}
