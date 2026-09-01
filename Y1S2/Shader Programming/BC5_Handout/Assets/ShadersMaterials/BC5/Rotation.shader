Shader "Unlit/Rotation"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Ambient("Ambient", Float) = 0
		_Pitch("Pitch", Float) = 0
		_Heading("Heading", Float) = 0
		_Bank("Bank", Float) = 0
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
			float _Pitch;
			float _Heading;
			float _Bank;

			v2f vert(appdata v)
			{
				v2f o;
				float4 vert = v.vertex;
				// TODO: apply manual rotation using _Pitch, _Heading and _Bank, before applying 
				//  UnityObjectToClipPos

				//[x cos a - y sin a, x sin a + y cos a]
				//vert.x * cosB - vert.y * sinB, 

				float sinB = sin(radians(_Bank));
				float cosB = cos(radians(_Bank));
				float4 rotatedZ = float4(vert.x * cosB - vert.y * sinB, vert.x * sinB + vert.y * cosB ,vert.z,vert.w);
				
				float sinB = sin(radians(_Heading));
				float cosB = cos(radians(_Heading));
				float4 rotatedY = float4(?, vert.y, ?, vert.w);

				o.vertex = UnityObjectToClipPos(rotatedZ);
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
