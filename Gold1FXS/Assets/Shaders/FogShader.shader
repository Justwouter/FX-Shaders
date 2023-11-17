Shader "Custom/FogShader"
{
    Properties {
        _FogColor("Fog Color", Color) = (.5, .6, .7, 1)
        _FogStart("Fog Start", Range(0, 500)) = 0
        _FogEnd("Fog End", Range(1, 1000)) = 100
    }
    
    CGINCLUDE
    #include "UnityCG.cginc"
    ENDCG

    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 pos : POSITION;
                float4 color : COLOR;
            };

            v2f vert(appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = v.vertex.y; // You can use this as a parameter for fog density or something
                return o;
            }

            fixed4 _FogColor;
            float _FogStart;
            float _FogEnd;

            fixed4 frag(v2f i) : COLOR {
                // Linear fog calculation
                float fogFactor = saturate((i.pos.z - _FogStart) / (_FogEnd - _FogStart));
                fixed4 col = _FogColor * fogFactor + (1 - fogFactor) * i.color; // Adjust this based on your needs
                return col;
            }
            ENDCG
        }
    }
}
