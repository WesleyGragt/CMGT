Shader "Unlit/Snow"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
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
					float4 normal : NORMAL;
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;

				v2f vert(appdata v)
				{
					v2f o;
					// TODO:
					//  Replace the next line.
					//  Offset the vertex in the direction of the snow fall, if the *world* normal is pointing
					//   in that direction.

					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					o.normal = normalize(mul(UNITY_MATRIX_M, float4(v.normal.xyz, 0)));
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 col = tex2D(_MainTex, i.uv);
					// TODO: Add a snow color if the fragment is pointing in the snow fall direction

					if (i.normal.y > 0) col += float4(i.normal.y, i.normal.y, i.normal.y, 0);

					return col; 
				}
				ENDCG
			}
		}
}
