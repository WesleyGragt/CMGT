Shader "Unlit/Transformation2D"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Translation("Translation", Vector) = (0,0,0,0)
		_Rotation("Rotation", Float) = 0
		_Scale("Scale", Vector) = (1,1,1,1)
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
			};

			sampler2D _MainTex;
			float4 _Translation;
			float _Rotation;
			float4 _Scale;

			v2f vert(appdata v)
			{
				v2f o;

				// TODO: apply the given translation, rotation and scale to the (2D) vector world,
				//  until the transformation is the same as the normal example quad in the scene.
				//  (In which order would you apply these steps?)				
				float2x2 scale = {
					_Scale.x, 0,
					0, _Scale.y
					};

				float angleRadians = radians(_Rotation);

				float2x2 rotation = {
					cos(angleRadians), -sin(angleRadians),
					sin(angleRadians), cos(angleRadians)
					};

				float2x2 translation = {
					1, 0,
					0, 1
					};

				// This is correct:
				//  first apply the scale operation:
				float2 scaleVec = mul(scale, v.vertex);
				//  ...next apply the rotation, to the scaled vector: 
				float2x2 combines = mul(rotation, scale);
				float2 world = mul(combines, v.vertex);

				// wrong way: 
				// float2 d = mul(rotation, v.vertex);
				// float2 world = mul(scale, d);

				// Do the rest of the translation:
				o.vertex = mul(UNITY_MATRIX_VP, float4(world,v.vertex.zw));

				o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
	}
}
