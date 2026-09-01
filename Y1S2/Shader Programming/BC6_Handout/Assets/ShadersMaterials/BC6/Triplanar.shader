Shader "Unlit/Triplanar"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
				_MainTex2("Texture2", 2D) = "white" {}
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
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 normal : NORMAL;
				float4 world: TEXCOORD0;
			};

			sampler2D _MainTex;
				sampler2D _MainTex2;
			float _Ambient;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				o.world = mul(UNITY_MATRIX_M, v.vertex);

				o.normal = normalize(mul(UNITY_MATRIX_M, float4(v.normal.xyz,0)));
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float3 absNormal = abs(i.normal);
				absNormal = pow(absNormal, 100);
				absNormal /= (absNormal.x + absNormal.y + absNormal.z);

				float4 col = tex2D(_MainTex, i.world.xy) * absNormal.z;
				col += tex2D(_MainTex2, i.world.xz) * absNormal.y;
				col += tex2D(_MainTex, i.world.yz) * absNormal.x;

				col = col *	(_Ambient + (1 - _Ambient) * saturate(i.normal.y)); // VERY basic lighting. TODO LATER: improve

				return col;
			}
			ENDCG
		}
	}
}
