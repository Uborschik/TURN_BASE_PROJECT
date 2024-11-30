Shader "Effects/Grid_2dField"
{
    Properties
    {
        [Header(Grid)][Space(10)]
        _Color ("Color", color) = (1, 1, 1, 1)
        _Power ("Power", float) = 1
        _SelectedPower ("Selected Power", float) = 1 

        [HideInInspector] _MainTex("MainTex", 2D) = "white" {}
        [HideInInspector] _TextureMap("TextureMap", 2D) = "black" {}

    }
    SubShader
    {
        Tags { "RenderPipeline" = "UniversalPipeline" "Queue" = "Transparent"}
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            #pragma vertex Vert
            #pragma fragment Frag

            struct appdata
            {
                float3 positionOS : POSITION;
            };

            struct v2f
            {
                float4 positionCS : SV_POSITION;
                float2 uvWS : TEXCOORD0;
            };

            uniform float2 _Scale;

            v2f Vert(appdata In)
            {
                v2f o = (v2f)0;

                o.positionCS = TransformObjectToHClip(In.positionOS);
                o.uvWS = TransformObjectToWorld(In.positionOS).xy - UNITY_MATRIX_M._m03_m13 + _Scale * 0.5;

                return o;
            }

            uniform half4 _Color;
            uniform float _Tiling, _Power, _SelectedPower;

            uniform SamplerState _pointClampSampler;
            uniform Texture2D _TextureMap;

            
            float lengthSqr(float2 a)
            {
                return a.x * a.x + a.y * a.y;
            }

            float GridA(float2 uv, float power, float s, float q, float x, float y)
            {
                [branch] if(s > 0.5 && x > 0.5 && y > 0.5) {
                   if(q < 0.5) {
                       return pow(1 - saturate(distance(abs(frac(uv) - float2(0.5, 0.5)) * 2, float2(1, 1))), power);
                   }

                   return 0.0;
                }

                float2 vec = pow(abs(frac(uv) - float2(0.5, 0.5)) * 2, power);

                if(s > 0.5) {
                    if(x > 0.5) {
                        return vec.y;
                    }

                    if(y > 0.5) {
                        return vec.x;
                    }
                }

                return vec.x + vec.y - vec.x * vec.y;
            }

            float2 GetClosestQuad(float2 uv) {
                float2 fracUv = frac(uv) - float2(0.5, 0.5);
                return floor(uv) + float2(0.5, 0.5) + sign(fracUv) * sqrt(2);
            }

            float2 GetClosestDiamondX(float2 uv) {
                float2 fracUv = frac(uv) - float2(0.5, 0.5);
                if(fracUv.x > 0) {
                    return float2(floor(uv.x) + 1.5, floor(uv.y) + 0.5);
                }
                    
                return float2(floor(uv.x) - 0.5, floor(uv.y) + 0.5);
            }

            float2 GetClosestDiamondY(float2 uv) {
                float2 fracUv = frac(uv) - float2(0.5, 0.5);          
                if(fracUv.y > 0) {
                    return float2(floor(uv.x) + 0.5, floor(uv.y) + 1.5);
                }
                    
                return float2(floor(uv.x) + 0.5, floor(uv.y) - 0.5);
            }

            half4 Frag(v2f In) : SV_TARGET
            {
                float2 pointX = GetClosestDiamondX(In.uvWS);
                float2 pointY = GetClosestDiamondY(In.uvWS);
                float2 pointQ = GetClosestQuad(In.uvWS);

                float sample = _TextureMap.SampleLevel(_pointClampSampler, In.uvWS / _Scale, 0).r;
                float sampleX = _TextureMap.SampleLevel(_pointClampSampler, pointX / _Scale, 0).r;
                float sampleY = _TextureMap.SampleLevel(_pointClampSampler, pointY / _Scale, 0).r;
                float sampleQ = _TextureMap.SampleLevel(_pointClampSampler, pointQ / _Scale, 0).r;

                float gridMask = GridA(In.uvWS, lerp(_Power, _SelectedPower, sample), sample, sampleQ, sampleX, sampleY);
                return half4(_Color.rgb, gridMask * _Color.a);
            }

            ENDHLSL
        }
    }
}
