Shader "Custom/exemple" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_LighthouseLocation("Lighthouse Location", Vector) = (0.0, 0.0, 0.0, 0.0)
		_LighthouseVector("Lighthouse Vector", Vector) = (1.0, 0.0, 0.0, 0.0)
		_LighthouseWidth("Lighthouse Width", Float) = 0.5
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard vertex:vert alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float customAlpha;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float3 _LighthouseLocation;
		float4 _LighthouseVector;
		float _LighthouseWidth;

		//Use to isolate a feature, or as cheap replacement for gaussian
		float cubicImpulse(float c, float w, float x)
		{
			x = abs(x - c);
			if (x>w) return 0.0;
			x /= w;
			return 1.0 - x*x*(3.0 - 2.0*x);
		}

		void vert(inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT(Input, o);
			float4 WSvertex = mul(unity_ObjectToWorld, v.vertex);
			float3 vect = normalize(WSvertex.xyz - _LighthouseLocation.xyz);
			o.customAlpha = cubicImpulse(1, _LighthouseWidth, abs(dot(vect, _LighthouseVector)));
			//float dotProd = abs(dot(vect, _LighthouseVector));
			//o.customAlpha = step(1 - _LighthouseWidth*0.5, dotProd) -step(1 + _LighthouseWidth*0.5, dotProd);
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			
			o.Albedo = _Color*(0.5 + IN.customAlpha*0.5);
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = 1- IN.customAlpha;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
