Shader "Unlit/ShowNormals"
{
	Properties
	{
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
				};

				struct v2f
				{
					float4 vertex : SV_POSITION;
					float4 normal : NORMAL;
				};

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);

					o.normal = mul(UNITY_MATRIX_M, float4(v.normal.xyz, 0)); // TODO: fix this; show world space normals.
					o.normal = normalize(o.normal);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					return float4(i.normal.xyz,1);
				}
				ENDCG
			}
		}
}
