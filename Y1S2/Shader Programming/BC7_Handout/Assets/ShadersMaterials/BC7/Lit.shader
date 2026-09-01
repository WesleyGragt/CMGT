Shader "Unlit/Lit"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Ambient("Ambient Lighting", Color) = (0,0,0,0)
		_Smoothness("Smoothness", Float) = 0
		_Metallic("Metallic", Float) = 0
		_SpecularStrength("Specular Strength", Float) = 1
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
			#include "UnityLightingCommon.cginc" // NEEDED FOR LightColor0 !

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
				float4 worldPos : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _Ambient;
			float _Smoothness;
			float _Metallic;
			float _SpecularStrength;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(UNITY_MATRIX_M, v.vertex);
				
				o.uv = v.uv;
				// TODO: You probably need to pass more information to the fragment shader...
				o.normal = normalize(mul(UNITY_MATRIX_M, float4(v.normal.xyz, 0)));
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// TODO: calculate the (1) ambient, (2) diffuse and possibly (3) specular light contribution
				//    for this fragment, and change the fragment color accordingly.
				float3 lightDir = _WorldSpaceLightPos0.xyz;
				
				float4 lightColor = _LightColor0;
				float4 camDir = normalize(float4(_WorldSpaceCameraPos,1) - i.worldPos);


				fixed4 albedo = tex2D(_MainTex, i.uv);

				float4 ambient = _Ambient;
				float4 diffuse = saturate(dot(i.normal, normalize(lightDir))) * lightColor;
				

				float3 shineDir = -lightDir; // direction light is moving! (not TO light!)
				float3 reflection = 
					//shineDir - 2 * (dot(shineDir, i.normal) * i.normal);
					reflect(shineDir,i.normal.xyz);
				//return reflection;

				float specular = pow(saturate(dot(reflection, camDir.xyz)), _Smoothness);
				//return specular * float4(1,1,1,1) * 0.2;

				return albedo * (ambient + diffuse + specular * _SpecularStrength);
			}
			ENDCG
		}

		// cast shadows:
		Pass
		{
			Tags{ "LightMode" = "ShadowCaster" }
			CGPROGRAM
			#pragma vertex VSMain
			#pragma fragment PSMain

			float4 VSMain(float4 vertex:POSITION) : SV_POSITION
			{
				return UnityObjectToClipPos(vertex);
			}

			float4 PSMain(float4 vertex:SV_POSITION) : SV_TARGET
			{
				return 0;
			}

			ENDCG
		}
	}
}
