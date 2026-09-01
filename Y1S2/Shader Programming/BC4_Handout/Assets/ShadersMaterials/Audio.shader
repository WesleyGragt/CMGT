Shader "CustomRenderTexture/PatternShader"
{
	Properties
	{
        _MyTex("Texture", 2D) = "white"{}
        _Feedback("Feedback", 2D) = "white"{}
        _Translate("Translate", Vector) = (0, 0, 0, 0)
        _Decay("Decay", float) = 0
        _Tint("Tint", Color) = (1,1,1,1)
	}

    SubShader
    {
        Blend One Zero

        Pass
        {
            Name "PatternShader"

            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0

            sampler2D   _MyTex; 
            sampler2D   _Feedback;
            float4      _Translate;
            float       _Decay;
            float4      _Tint;

            float4 frag(v2f_customrendertexture IN) : SV_Target
            {
                float2 uv = IN.localTexcoord.xy;
                float4 newCol = tex2D(_MyTex,uv) * _Tint;


                uv.xy -= _Translate.xy;
                float4 color = tex2D(_Feedback, uv) * _Decay;

                
                return newCol + color;
            
            }
            ENDCG
        }
    }
}