Shader "Custom/RevampedStandard" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Emission ("Emission", Color) = (0,0,0,0)

		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Tex2 ("Tex2 (RGB)", 2D) = "black" {}
		_Bump("Bump (RGB)", 2D) = "bump" {}
		_BumpDetail ("Bump Detail", Float) = 1
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM

		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D __DetailAbedoMap;
		sampler2D _Tex2;
		sampler2D _Bump;

		struct Input {
			//float2 uv_MainTex;
			//float2 uv_Tex2;
			float2 uv_MainTex		: TEXCOORD0;
			float2 uv2_Tex2;

			float2 uv_Bump;

		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _BumpDetail;
		float4 _Emission;


		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color


			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 c1 = tex2D (_Tex2, IN.uv2_Tex2);

			//o.Albedo = (c*c1).rgb;
			//o.Albedo = c.rgb;
			////////////

			half3 mainTexVisible = c.rgb * (1-c1.a);
         	half3 overlayTexVisible = c1.rgb * (c1.a);
             
         	float3 finalColor = (mainTexVisible + overlayTexVisible) * _Color;
              
            o.Albedo = finalColor.rgb;            
            o.Alpha = 0.5 * ( c.a + c1.a );



			// Metallic and smoothness come from slider variables
			//o.Metallic = _Metallic;
			o.Gloss = _Glossiness;

			fixed3 normal = UnpackNormal(tex2D(_Bump, IN.uv_Bump)); 
			normal.z = normal.z/_BumpDetail; 
			o.Normal = normalize(normal); 

			//o.Normal = UnpackNormal(tex2D(_Bump, IN.uv_Bump));
			//o.Alpha = c.a;
			o.Emission = _Emission.rgb;
		}
		ENDCG
	}
	FallBack "Bumped Diffuse"
}
