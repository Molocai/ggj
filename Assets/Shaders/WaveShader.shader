// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/WaveShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Smoothing ("Smoothing", Float) = 0.0
		_Speed("Speed", Float) = 1.0
		_Scale("Scale", Float) = 1.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 diff : TEXCOORD1;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;					
			};

			sampler2D _MainTex;
			float _Smoothing;
			float _Speed;
			float _Scale;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				float4 v0 = mul(unity_ObjectToWorld, v.vertex);
				float4 v1 = v0 + float4(0.05, 0.0, 0.0, 0.0);
				float4 v2 = v0 + float4(0.0, 0.0, 0.05, 0.0);

				v0.y += sin(_Time*_Speed + v0.x*_Scale);
				v1.y += sin(_Time*_Speed + v1.x*_Scale);
				v2.y += sin(_Time*_Speed + v2.x*_Scale);

				v1.y -= (v1.y - v0.y)*_Smoothing;
				v2.y -= (v2.y - v0.y)*_Smoothing;

				float3 worldNormal = cross(v2.xyz - v0.xyz, v1.xyz - v0.xyz);
				worldNormal = normalize(worldNormal);

				v2f o;
				//o.vertex = UnityObjectToClipPos(v.vertex);
				o.vertex = mul(UNITY_MATRIX_VP, v0);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);


				//half3 worldNormal = UnityObjectToWorldNormal(v.normal);
				half nl = max(0, dot(worldNormal, -_WorldSpaceLightPos0.xyz));
				o.diff = nl * 1.0;

				// the only difference from previous shader:
				// in addition to the diffuse lighting from the main light,
				// add illumination from ambient or light probes
				// ShadeSH9 function from UnityCG.cginc evaluates it,
				// using world space normal
				o.diff.rgb += ShadeSH9(half4(worldNormal, 1));


				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				col *= i.diff;
				return col;
			}
			ENDCG
		}
	}
}