Shader "Custom/ToonShaderCG" {
    Properties {
        // Main object color
        [HDR] _Albedo("Albedo", Color) = (1,1,1,1)
        // Define amount of gradients
        _Shades("Shades", Range(1, 20)) = 3

        // Outline stuff
        _OutlineSize("Outline Thickness", Range(2, 100)) = 20
        [HDR] _OutlineColor("Outline Color", Color) = (0,0,0,1)
    }
    
    SubShader {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline"}
        LOD 100


        // Outline - renders a bigger version of the object w/ the front culled
        Pass {
            Name "Outline Pass"
            Tags { "LightMode" = "UniversalForward" } // Hacky workaround for URP not supporting multiple passes apperantly?
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };
            
            float _OutlineSize;
            float4 _OutlineColor;


            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex + v.normal/_OutlineSize);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
               

                return _OutlineColor;
            }
            ENDCG
        }

        // Cell shading
        Pass {
            Name "Shading pass"
            Tags { "LightMode" = "SRPDefaultUnlit" } // Hacky workaround for URP not supporting multiple passes apperantly?

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
            };
            
            // Make the properties accessible in CG
            float4 _Albedo;
            float _Shades;

            
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
               float cosineAngle = dot(normalize(i.worldNormal),normalize(_WorldSpaceLightPos0.xyz));
                
                cosineAngle = max(cosineAngle,0.0);

                // Add gradient
                cosineAngle = floor(cosineAngle * _Shades) / _Shades;

                // Apply gradient to the color and return
                return _Albedo * cosineAngle;
            }
            ENDCG
        }
    }
    // Use the default lit shader as fallback so shadows etc will be rendered
    Fallback "VertexLit"
}
