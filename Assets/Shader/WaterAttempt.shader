Shader "Custom/WaterAttempt"
{
    Properties
    {
        // Color property for material inspector, default to white
        _Color ("Main Color", Color) = (1,1,1,1)
        _Color2 ("Second Color", Color) = (1,1,1,1)

        _MainTex("Base texture", 2D) = "white" {}
		_Ramp("Ramp", 2D) = "white" {}
            
        _Foam("Foam", 2D) = "white" {}
		_FoamStrength ("Foam strength", Range (0, 10.0)) = 1.0
		_FoamGradient ("Foam gradient ", 2D) = "white" {}
		_Tile ("Tiling", float) = 10.0
		_Scale ("Scale", float) = 1.0
		_Freq ("Freq", float) = 1.0
		_Speed ("Speed", float) = 1.0
		_MoveX ("MoveX", float) = 1.0
		_MoveY ("MoveY", float) = 1.0
		_Alpha ("Alpha", Range(0,1.0)) = 1.0
		_r1 ("r1", Range(0,1.0)) = 1.0
		_r2 ("r2", Range(0,1.0)) = 1.0

        }
    SubShader
    {
         Tags { "Queue" = "Transparent" "RenderType"="Transparent"  }
 
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            
           	#include "UnityCG.cginc" // for UnityObjectToWorldNormal
            #include "UnityLightingCommon.cginc" // for _LightColor0
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            struct v2f {
				fixed4 diff : COLOR0; // diffuse lighting color
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 projPos : TEXCOORD1;
				float2 foamuv: TEXCOORD2;
				float nValue: TEXCOORD3;
				UNITY_FOG_COORDS(4)


            };

            // vertex shader
            // this time instead of using "appdata" struct, just spell inputs manually,
            // and instead of returning v2f struct, also just return a single output
            // float4 clip position
			float _Scale, _Speed, _Freq, _MoveX, _MoveY, _Alpha;
			sampler2D _Ramp;

            v2f vert (appdata_full v)
            {
                v2f o;
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
          
                o.uv = v.texcoord;
                o.uv+=_Time/_MoveX;
                o.uv.y+=_Time/_MoveY;

//				half offsetVert = v.vertex.x * v.vertex.z;  
//main				half offsetVert = v.vertex.x * v.vertex.x + v.vertex.z + v.vertex.z;

				half offsetVert = v.vertex.x * v.vertex.x + v.vertex.z + 0.03f;

                half movement = _Scale * sin(_Time.w * _Speed + offsetVert * _Freq);

                o.vertex.y += movement;
                v.normal += movement;
                //o.normal = UnityObjectToWorldNormal(v.normal);
                // get vertex normal in world space
                half3 worldNormal = UnityObjectToWorldNormal(v.normal+movement);
                // dot product between normal and light direction for
                // standard diffuse (Lambert) lighting
                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                // factor in the light color
                half test = dot(v.normal,float3(0,1,0));
                o.nValue = test*0.5+0.5;
                o.diff = _LightColor0;

                //o.diff.rgb += ShadeSH9(half4(worldNormal,1));
                o.diff.rgb += UNITY_LIGHTMODEL_AMBIENT;

                o.foamuv = 7.0f *o.vertex.xz + 0.05 *float2(_SinTime.w,_SinTime.w);
                //o.foamuv.y+=_Time/_MoveY;

                //TRANSFER_VERTEX_TO_FRAGMENT(o);
                o.projPos = ComputeScreenPos (o.vertex);
				COMPUTE_EYEDEPTH(o.projPos.z);
				UNITY_TRANSFER_FOG(o,o.vertex);

                return o;
            }
            
            // color from the material
            fixed4 _Color;
            fixed4 _Color2;

			sampler2D _MainTex;
			sampler2D _Foam;
			sampler2D _FoamGradient;
			uniform float _FoamStrength;
			uniform float _r1;
			uniform float _r2;

			uniform float _Tile;
			sampler2D _CameraDepthTexture;

			fixed4 frag (v2f i) : COLOR
            {

                fixed4 col = tex2D(_MainTex, i.uv * _Tile);
                float sceneZ = LinearEyeDepth (tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD(i.projPos)).r);
                float partZ = i.projPos.z;
                float fade = (abs(sceneZ-partZ)) / _FoamStrength;
				//half3 foamGradient = 1 - tex2D(_FoamGradient, float2(fade + _Time.y*0.5, 0) + i.uv * 0.15);


				//half3 foamColor = tex2D(_Foam, i.foamuv * 0.2).rgb;


			
				if(i.nValue<_r1){
					col.rgb = _Color.rgb;
					col.a*=2;
				} 

				else if (i.nValue >=_r1 && i.nValue < _r2){
					col.rgb = _Color2.rgb;
					col.a*=2;

				} else {
					//col.rgb += foamGradient * fade * foamColor;

					col.a = _Alpha;

				}

				if(fade <= 1){
					col.rgb = lerp(_Color.rgb,col.rgb,float4(fade,fade,fade,fade));
					col.a*=2;

				}
				//fixed4 ret;
				//ret.r = col.r;
				//ret.g = col.g;
				//ret.b = col.b;

				col.rgb *= i.diff;
				UNITY_APPLY_FOG(i.fogCoord, col);

                return col;
            }
            ENDCG

        }
    }
}