Shader "Custom/WaveImageShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WaveFrequency ("Wave Frequency", Float) = 5.0
        _WaveAmplitude ("Wave Amplitude", Float) = 0.05
        _Speed ("Wave Speed", Float) = 20.0
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        LOD 100
        
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            // User properties
            sampler2D _MainTex;
            float _WaveFrequency;
            float _WaveAmplitude;
            float _Speed;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                // Calculate time-dependent wave offset
                float waveOffset = sin(IN.uv.y * _WaveFrequency + _Time.x * _Speed) * _WaveAmplitude;

                // Offset the position based on the wave effect
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                OUT.positionHCS.x += waveOffset;

                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // Sample the texture
                half4 color = tex2D(_MainTex, IN.uv);

                // Return the color with transparency (UI RawImages expect transparency)
                return color;
            }

            ENDHLSL
        }
    }

    FallBack "Unlit/Transparent"
}
