Shader "Unlit/Dice"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Ambient("Ambient", Float) = 0
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
					float4 vertex : SV_POSITION;
					float4 normal : NORMAL;
					float2 uv : TEXCOORD0;
				};

				sampler2D _MainTex;
				float _Ambient;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					// TODO: Pass the world space normal to the fragment shader
					o.normal = mul(UNITY_MATRIX_M, float4(v.normal.xyz, 0));
					o.normal = normalize(o.normal);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 col = tex2D(_MainTex, i.uv);
					// TODO: If the face of the current fragment is pointing up, add a (red?) color highlight
					if (i.normal.y > 0) col += float4(pow(i.normal.y, 15), 0, 0, 0);
					
					return col;
				}
				ENDCG
			}
		}
}
