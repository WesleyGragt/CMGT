Shader "CustomRenderTexture/PatternShader"
{
	Properties
	{
        _MyTex("Texture", 2D) = "white"{}
		_Color("Color", Color) = (1,1,1,1)
        _Brightness("Brightness", float) = 1
        _Invert("Invert", Range(0, 1)) = 1
        _Saturation("Saturation", float) = 1
        _Type("BrightnessType",int) = 0
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
            float4      _Color;
            float       _Brightness;
            float       _Invert;
            float       _Saturation;
            int _Type;

            float4 frag(v2f_customrendertexture IN) : SV_Target
            {
                float2 uv = IN.localTexcoord.xy;
                float4 color = tex2D(_MyTex, uv);

                color = float4(color.rgb * _Brightness, 1);

                float4 white = float4(1, 1, 1, 1);
                if (_Invert > 0) color = float4(white.rgb * _Invert - color.rgb, 1);
                
                float Lightness = (color.r + color.g + color.b)/3;
                float Brightness = max(color.r, color.g);
                Brightness = max(Brightness, color.b);
                float Luminance = dot(color,float3(0.2989, 0.5870, 0.1140)); //A.x * B.x + A.y * B.y + A.z * B.z
                
                float res=Luminance;                
                if (_Type==0) 
                     res=Brightness;
                else if (_Type==1)
                    res = Lightness;
                //else 
                //return Luminance * _Saturation;

                
                float4 grayScale = res * float4(1,1,1,1);
                grayScale.a=1; 
                
                color = float4(grayScale.rgb + _Saturation * (color.rgb - grayScale.rbg), 1);

                return color;
            
            }
            ENDCG
        }
    }
}

// ---------------------------------Colored Circle
                // float2 center = (.5, .5);
                // float2 vec = uv - center;
                // float length = sqrt(vec.x * vec.x + vec.y * vec.y);

                // if (length > 0.3) color = float4(0, 0, 0, 1);
                // else color = float4(1, 0, 1, 1);

                // ---------------------------------lines
                // input:
                // float2 normal = normalize(float2(2,1)); something like 0.89,0.45?
                // float dist = dot(uv,normal);


                // float x = dist; uv.x + 0.5 * uv.y;  // dist;;

                // output: (create lines)
                // float y = fmod(x*2,0.5);
                
                // color =  y*_Color + (1-y)*float4(1,0,0.3,1);
                // return color;

                // ---------------------------------CheckerBoard
                // color = round(float4(fmod(color.rgb, .2) * 5, 1));

                // if (fmod(color.r + color.g, 2) == 0) color = float4(0, 0, 0, 1);
                // else color = float4(1, 1, 1, 1);