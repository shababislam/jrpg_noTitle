﻿Shader "Toon/Lit Custom" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _RimColor ("Rim Color", Color) = (0.97,0.88,1,0.75)
        _RimPower ("Rim Power", Float) = 5
        _MainTex ("Diffuse (RGB) Alpha (A)", 2D) = "white" {}
        _BumpMap ("Normal (RGB)", 2D) = "bump" {}
        _Ramp ("Toon Ramp (RGB)", 2D) = "white" {}
        _RimFactor("Rim Factor", Float) = 1
    }
 
    SubShader{
        Tags { "RenderType" = "Opaque" }
       
        CGPROGRAM
            #pragma surface surf RimLight alphatest:_Cutoff
 
            struct Input
            {
                float2 uv_MainTex;
            };
           
            sampler2D _MainTex, _BumpMap, _Ramp;
            float4 _RimColor;
            float _RimPower;
            float _RimFactor;
 
            inline fixed4 LightingRimLight (SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten)
            {
                lightDir = normalize(lightDir);
                viewDir = normalize(viewDir);
               
				half d = dot (s.Normal, lightDir)*0.5 + 0.5;
				half3 ramp = tex2D (_Ramp, float2(d,d)).rgb;
               
                float3 lightDirPerp = normalize(lightDir - dot(lightDir, viewDir) * viewDir);
                fixed3 rimC = s.Albedo.rgb * _RimFactor; 
                fixed3 rim = atten * pow(max(0, dot(lightDirPerp, s.Normal)), _RimPower) * _RimColor * rimC.rgb * _RimColor.a;
               
                fixed4 c;
                c.rgb = ((s.Albedo * ramp * _LightColor0.rgb) * (atten * 2)) + (rim * atten);
                c.a = s.Alpha;
                return c;
            }
 
            void surf (Input IN, inout SurfaceOutput o)
            {
                o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
                o.Alpha = tex2D(_MainTex, IN.uv_MainTex).a;
                o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
            }
        ENDCG
    }
    Fallback "Transparent/Cutout/VertexLit"
}
  