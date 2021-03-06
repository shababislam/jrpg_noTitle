﻿**************************************************
*// Created by sejton. Feel free to use it however you want. You don't have to give me any credits, 
*// however they will be warmly welcomed. Enjoy!
*// ************************************************************************************************
*
*Shader "Custom\GrassBillboard22" 
*{
*	Properties 
*	{
*		_MainTex ("Grass Texture", 2D) = "white" {}
*		_NoiseTexture ("Noise Texture", 2D) = "white" {}
*		_HealthyColor ("Healthy Color", Color) = (0,1,0,1)
*		_DryColor ("Dry Color", Color) = (1,1,0,1)
*		_MinSize ("Min Size", float) = 1
*		_MaxSize ("Max Size", float) = 2
*		_MaxCameraDistance ("Max Camera Distance", float) = 250
*		_Transition ("Transition", float) = 30
*		_Cutoff ("Alpha Cutoff", Range(0,1)) = 0.1
*	}
*
*	SubShader 
*	{
*		Tags { "Queue" = "Geometry" "RenderType"="Opaque" }
*
*		// base pass
*		Pass
*		{
*			Tags { "LightMode" = "ForwardBase"}
*
*			CGPROGRAM
*				#pragma vertex vertexShader
*				#pragma fragment fragmentShader
*				
*				#pragma multi_compile_fwdbase
*				#pragma multi_compile_fog
*
*				#pragma fragmentoption ARB_precision_hint_fastest
*
*				#include "UnityCG.cginc"
*				#include "AutoLight.cginc"
*
*				struct VS_INPUT
*				{
*					float4 position : POSITION;
*					float4 uv_Noise : TEXCOORD0;
*					fixed sizeFactor : TEXCOORD1;
*				};
*
*				struct GS_INPUT
*				{
*					float4 worldPosition : TEXCOORD0;
*					fixed2 parameters : TEXCOORD1;	// .x = noiseValue, .y = sizeFactor
*				};
*
*				struct FS_INPUT
*				{
*					float4 pos	: SV_POSITION;		// has to be called this way because of unity MACRO for light
*					float2 uv_MainTexture : TEXCOORD0;
*					float4 tint : COLOR0;
*					LIGHTING_COORDS(1,2)
*					UNITY_FOG_COORDS(3)
*				};
*
*				uniform sampler2D _MainTex, _NoiseTexture;
*
*				// for billboard
*				uniform fixed _Cutoff;
*				uniform float _MinSize, _MaxSize;
*				uniform fixed4 _HealthyColor, _DryColor;
*				uniform float _MaxCameraDistance;
*				uniform float _Transition;
*				
*				uniform float4 _LightColor0;
*
*
*				// Vertex Shader ------------------------------------------------
*				GS_INPUT vertexShader(VS_INPUT vIn)
*				{
*					GS_INPUT vOut;
*					
*					// set output values
*					vOut.worldPosition =  mul(_Object2World, vIn.position);
*					vOut.parameters.x = tex2Dlod(_NoiseTexture, float4(vIn.uv_Noise.xyz, 0)).r;
*					vOut.parameters.y = vIn.sizeFactor;
*
*					return vOut;
*				}
*
*
*				// Fragment Shader -----------------------------------------------
*				float4 fragmentShader(FS_INPUT fIn) : COLOR
*				{
*					fixed4 color = tex2D(_MainTex, fIn.uv_MainTexture) * fIn.tint;
*					if (color.a < _Cutoff) 
*						discard;
*
*					float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
*					float atten = LIGHT_ATTENUATION(fIn);
*
*					float3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
*					float3 normal = float3(0,1,0);
*					float3 lambert = float(max(0.0,dot(normal,lightDirection)));
*					float3 lighting = (ambient + lambert * atten) * _LightColor0.rgb;
*
*					color = fixed4 (color.rgb * lighting, 1.0f);
*					
*					UNITY_APPLY_FOG(fIn.fogCoord, color);
*
*
*					return color;
*				}
*
*			ENDCG
*		}
*
*
*
*	FallBack "Diffuse"
*} 
