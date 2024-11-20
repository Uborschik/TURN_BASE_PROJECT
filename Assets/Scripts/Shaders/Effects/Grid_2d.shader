Shader "Effects/Grid_2d"
{
    Properties
    {
        [Header(Grid)][Space(10)]
        _Color ("Color", color) = (1, 1, 1, 1)
        _Tiling ("Tiling", float) = 1
        _Power ("Power", float) = 1

        [HideInInspector] _MainTex("MainTex", 2D) = "white" {}

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


            v2f Vert(appdata In)
            {
                v2f o = (v2f)0;

                o.positionCS = TransformObjectToHClip(In.positionOS);
                o.uvWS = TransformObjectToWorld(In.positionOS).xy - UNITY_MATRIX_M._m03_m13;

                return o;
            }

            uniform half4 _Color;
            uniform float _Tiling, _Power;

            
            float lengthSqr(float2 a)
            {
                return a.x * a.x + a.y * a.y;
            }

            float GridA(float2 uv, float power)
            {
                float2 vec = pow(abs(frac(uv) - float2(0.5, 0.5)) * 2, power);
                return vec.x + vec.y - vec.x * vec.y;
            }

            half4 Frag(v2f In) : SV_TARGET
            {
                float gridMask = GridA(In.uvWS * _Tiling, _Power);

                return half4(_Color.rgb, gridMask * _Color.a);
            }

            ENDHLSL
        }
    }
}
