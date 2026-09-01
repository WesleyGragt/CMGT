Shader "Unlit/Transparency"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Ambient("Ambient", Float) = 0
		_Color("Color", Color) = (1,1,1,1)
		_Alpha("Alpha", Float) = 0.5
	}

	SubShader
	{
			// TODO: Try out different combinations of options for ZWrite / ZTest / Cull / BlendOp / Blend - What do they mean?
			ZWrite On
			//ZWrite Off
			
			ZTest Off
			//ZTest Greater
			//ZTest Less

			Cull Off
			//Cull Front
			//Cull Back

			BlendOp Sub 
			//BlendOp Add
			//BlendOp Max
			//BlendOp Min

			Blend One One	
			//Blend DstColor Zero
			//Blend DstColor One
			//Blend Zero OneMinusDstColor
			//Blend Zero OneMinusSrcColor
			//Blend SrcAlpha OneMinusSrcAlpha
			//Blend One OneMinusSrcAlpha
			//Blend OneMinusDstColor One


		Tags {
			// TODO: Try out different queues for this shader. What do they mean?
			//"Queue" = "BackGround" 
			//"Queue" = "Geometry-1"
			//"Queue" = "Geometry"
			"Queue" = "Geometry+1"
			//"Queue" = "Transparent"
		}

		////// To get a perfect transparent material, nothing below this line needs to be changed! //////////////////////////////////////////////////
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
			float4 _Color;
			float _Alpha;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.normal = v.normal;
				o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col =
					_Color *
					tex2D(_MainTex, i.uv) *
					(_Ambient + (1 - _Ambient) * saturate(i.normal.y)); // VERY basic lighting. TODO LATER: improve
				col.a = _Alpha;
				return col;
			}
			ENDCG
		}

		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"	// An easy way to enable shadow casting
	}
}
