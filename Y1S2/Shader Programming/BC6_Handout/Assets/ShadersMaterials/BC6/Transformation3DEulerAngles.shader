Shader "Unlit/Transformation3DEulerAngles"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Ambient("Ambient", Float) = 0
		_Pitch("Pitch", Float) = 0
		_Heading("Heading", Float) = 0
		_Bank("Bank", Float) = 0
		_Translation("Translation", Vector) = (0,0,0,0)
		_Scale ("Scale", Vector) = (1,1,1,1)
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
			float4 _Translation;
			float4 _Scale;

			v2f vert(appdata v)
			{
				v2f o;


				float3x3 id = {
					1,0,0,
					0,1,0,
					0,0,1
					};

				float3x3 scale = {
					_Scale.x, 0, 0,
					0, _Scale.y, 0,
					0, 0, _Scale.z
					};

				float3x3 pitch = {
					1, 0, 0,
					0, cos(radians(_Pitch)), -sin(radians(_Pitch)),
					0, sin(radians(_Pitch)), cos(radians(_Pitch))
					};

				float3x3 heading = {
					cos(radians(_Heading)), 0, sin(radians(_Heading)),
					0, 1, 0,
					-sin(radians(_Heading)), 0, cos(radians(_Heading))
					};
				
				float3x3 bank = {
					cos(radians(_Bank)), -sin(radians(_Bank)), 0,
					sin(radians(_Bank)), cos(radians(_Bank)), 0,
					0, 0, 1
					};

				float3x3 rotation = mul(heading, mul(pitch, bank));
				float3x3 combination = mul(rotation, scale);
				float3 world = mul(combination, v.vertex) + _Translation;

				o.vertex = mul(UNITY_MATRIX_VP,float4(world,1));
				o.normal = v.normal;
				o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col =
					tex2D(_MainTex, i.uv) *
					(_Ambient + (1 - _Ambient) * saturate(i.normal.y));
				return col;
			}
			ENDCG
		}
	}
}
