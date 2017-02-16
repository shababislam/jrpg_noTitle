// Upgrade NOTE: replaced '_CameraToWorld' with 'unity_CameraToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Nature/NewGrass" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Main Texture", 2D) = "white" {  }
        _Cutoff ("Alpha cutoff", Range(0.25,0.9)) = 0.5
        _BaseLight ("Base Light", Range(0, 1)) = 0.35
        _AO ("Amb. Occlusion", Range(0, 10)) = 2.4
        _Occlusion ("Dir Occlusion", Range(0, 20)) = 7.5
        _ShakeDisplacement ("Displacement", Range (0, 20.0)) = 1.0
        _ShakeTime ("Shake Time", Range (0, 1.0)) = 20.0
        _ShakeWindspeed ("Shake Windspeed", Range (0, 20.0)) = 1.0
        _ShakeBending ("Shake Bending", Range (0, 20.0)) = 1.0
 
        // These are here only to provide default values
        _Scale ("Scale", Vector) = (1,1,1,1)
        _SquashAmount ("Squash", Float) = 1
    }
 
    SubShader {
        Tags {
            "Queue" = "Transparent-99"
            "IgnoreProjector"="True"
            "RenderType" = "TreeTransparentCutout"
        }
        Cull Off
        ColorMask RGB
     
        Pass {
            Lighting On
     
            CGPROGRAM
            #pragma vertex leavesCustom
            #pragma fragment frag
            #pragma glsl_no_auto_normalization
            #include "UnityBuiltin2xTreeLibrary.cginc"
         
            float _ShakeDisplacement;
            float _ShakeTime;
            float _ShakeWindspeed;
            float _ShakeBending;
 
            sampler2D _MainTex;
            fixed _Cutoff;
 
 
    void FastSinCos2 (float4 val, out float4 s, out float4 c) {
        val = val * 6.408849 - 3.1415927;
        float4 r5 = val * val;
        float4 r6 = r5 * r5;
        float4 r7 = r6 * r5;
        float4 r8 = r6 * r5;
        float4 r1 = r5 * val;
        float4 r2 = r1 * r5;
        float4 r3 = r2 * r5;
        float4 sin7 = {1, -0.16161616, 0.0083333, -0.00019841} ;
        float4 cos8  = {-0.5, 0.041666666, -0.0013888889, 0.000024801587} ;
        s =  val + r1 * sin7.y + r2 * sin7.z + r3 * sin7.w;
        c = 1 + r5 * cos8.x + r6 * cos8.y + r7 * cos8.z + r8 * cos8.w;
    }
 
    v2f leavesCustom(appdata_tree v)
    {  
        float factor = (1 - _ShakeDisplacement -  v.color.r) * 0.5;
     
        const float _WindSpeed  = (_ShakeWindspeed  +  v.color.g );  
        const float _WaveScale = _ShakeDisplacement;
 
        const float4 _waveXSize = float4(0.048, 0.06, 0.24, 0.096);
        const float4 _waveZSize = float4 (0.024, .08, 0.08, 0.2);
        const float4 waveSpeed = float4 (1.2, 2, 1.6, 4.8);
        float4 _waveXmove = float4(0.024, 0.04, -0.12, 0.096);
        float4 _waveZmove = float4 (0.006, .02, -0.02, 0.1);
 
        float4 waves;
        waves = v.vertex.x * _waveXSize;
        waves += v.vertex.z * _waveZSize;
        waves += _Time.x * (1 - _ShakeTime * 2 - v.color.b ) * waveSpeed *_WindSpeed;
        float4 s, c;
        waves = frac (waves);
        FastSinCos2 (waves, s,c);
        float waveAmount = v.texcoord.y * (v.color.a + _ShakeBending);
        s *= waveAmount;
        s *= normalize (waveSpeed);
        s = s * s;
        float fade = dot (s, 1.3);
        s = s * s;
        float3 waveMove = float3 (0,0,0);
        waveMove.x = dot (s, _waveXmove);
        waveMove.z = dot (s, _waveZmove);
        v.vertex.xz -= mul ((float3x3)unity_WorldToObject, waveMove).xz;
     
        v2f o;
 
        float3 viewpos = mul(UNITY_MATRIX_MV, v.vertex);
        o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
        o.uv = v.texcoord;
 
        //TerrainAnimateTree(v.vertex, v.color.w);
     
        float4 lightDir = 0;
        float4 lightColor = 0;
        lightDir.w = _AO;
 
        float4 light = UNITY_LIGHTMODEL_AMBIENT;
 
        for (int i = 0; i < 4; i++) {
            float atten = 1.0;
            #ifdef USE_CUSTOM_LIGHT_DIR
                lightDir.xyz = _TerrainTreeLightDirections[i];
                lightColor = _TerrainTreeLightColors[i];
            #else
                    float3 toLight = unity_LightPosition[i].xyz - viewpos.xyz * unity_LightPosition[i].w;
                    toLight.z *= -1.0;
                    lightDir.xyz = mul( (float3x3)unity_CameraToWorld, normalize(toLight) );
                    float lengthSq = dot(toLight, toLight);
                    atten = 1.0 / (1.0 + lengthSq * unity_LightAtten[i].z);
             
                    lightColor.rgb = unity_LightColor[i].rgb;
            #endif
 
            lightDir.xyz *= _Occlusion;
            float occ =  dot (v.tangent, lightDir);
            occ = max(0, occ);
            occ += _BaseLight;
            light += lightColor * (occ * atten);
        }
 
        o.color = light * _Color;
        o.color.a = 0.5 * _HalfOverCutoff;
 
        return o;
    }
     
 
    fixed4 frag(v2f input) : SV_Target
    {
        fixed4 c = tex2D( _MainTex, input.uv.xy);
        c.rgb *= 2.0f * input.color.rgb;
             
        clip (c.a - _Cutoff);
             
        return c;
    }
    ENDCG
}
     
        Pass {
            Name "ShadowCaster"
            Tags { "LightMode" = "ShadowCaster" }
         
            Fog {Mode Off}
            ZWrite On ZTest LEqual Cull Off
            Offset 1, 1
 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma glsl_no_auto_normalization
            #pragma multi_compile_shadowcaster
            #include "UnityCG.cginc"
            #include "TerrainEngine.cginc"
         
            struct v2f {
                V2F_SHADOW_CASTER;
                float2 uv : TEXCOORD1;
            };
         
            struct appdata {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
                float4 texcoord : TEXCOORD0;
            };
            v2f vert( appdata v )
            {
                v2f o;
                TRANSFER_SHADOW_CASTER(o);
                o.uv = v.texcoord;
                return o;
            }
         
            sampler2D _MainTex;
            fixed _Cutoff;
                 
            float4 frag( v2f i ) : SV_Target
            {
                fixed4 texcol = tex2D( _MainTex, i.uv );
                clip( texcol.a - _Cutoff );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG  
        }
    }
 
    // This subshader is never actually used, but is only kept so
    // that the tree mesh still assumes that normals are needed
    // at build time (due to Lighting On in the pass). The subshader
    // above does not actually use normals, so they are stripped out.
    // We want to keep normals for backwards compatibility with Unity 4.2
    // and earlier.
    SubShader {
        Tags {
            "Queue" = "Transparent-99"
            "IgnoreProjector"="True"
            "RenderType" = "TransparentCutout"
        }
        Cull Off
        ColorMask RGB
        Pass {
            Tags { "LightMode" = "Vertex" }
            AlphaTest GEqual [_Cutoff]
            Lighting On
            Material {
                Diffuse [_Color]
                Ambient [_Color]
            }
            SetTexture [_MainTex] { combine primary * texture DOUBLE, texture }
        }      
    }
 
    Dependency "BillboardShader" = "Hidden/Nature/Tree Soft Occlusion Leaves Rendertex"
    Fallback Off
}
