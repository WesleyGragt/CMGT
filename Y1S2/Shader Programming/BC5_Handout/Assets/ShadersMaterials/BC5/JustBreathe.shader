Shader "Unlit/JustBreathe"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Ambient ("Ambient", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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
				float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
            };

            sampler2D _MainTex;
			float _Ambient;

            v2f vert (appdata v)
            {
                v2f o;
				// TODO: move the vertex in the direction of the normal:
				float4 newVertex =
                    //v.vertex + _SinTime.w; // automatically converted to float4(_SinTime.w,_SinTime.w, ...)
                    v.vertex +  0.2*_SinTime.w * float4(v.normal , 0); // automatically converted to float4(normal.x,normal.y,normal.z,??)
                    // w=1 => ??!?!?! -> see BC7 :-)
                float4x4 MVP = UNITY_MATRIX_MVP; // needed to stop Unity from meddling

                o.vertex = // UnityObjectToClipPos(newVertex);
                    mul(MVP,newVertex);
				o.normal = v.normal;
				o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = 
					tex2D(_MainTex, i.uv) * 
					(_Ambient + (1-_Ambient) * saturate(i.normal.y)); // VERY basic lighting. TODO LATER: improve
                return col;
            }
            ENDCG
        }
    }
}
