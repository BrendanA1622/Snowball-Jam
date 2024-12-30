Shader "Custom/OcclusionOutline"
{
    Properties
    {
        _Color ("Outline Color", Color) = (1, 1, 1, 1)
        _OutlineThickness ("Outline Thickness", Float) = 0.05
    }
    SubShader
    {
        // First pass: Write to the stencil buffer only when the object is behind other objects
        Tags { "RenderType"="Opaque" }
        Pass
        {
            ZTest Greater
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }
        }

        // Second pass: Render the outline only where the stencil buffer is set
        Tags { "RenderType"="Opaque" }
        Pass
        {
            Cull Front
            ZTest Always
            Stencil
            {
                Ref 1
                Comp Equal
            }

            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : POSITION;
            };

            float _OutlineThickness;
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                float3 norm = mul((float3x3)unity_ObjectToWorld, float3(0, 0, 1));
                o.pos = UnityObjectToClipPos(v.vertex + float4(norm * _OutlineThickness, 0));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}



