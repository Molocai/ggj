// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Water"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_LighthouseLocation("Lighthouse Location", Vector) = (0.0, 0.0, 0.0, 0.0)
		_LighthouseVector("Lighthouse Vector", Vector) = (1.0, 0.0, 0.0, 0.0)
		_LighthouseWidth("Lighthouse Width", Float) = 0.5
		_Glossiness("Smoothness", Range(0,1)) = 0.8	
		_Metallic("Metallic", Range(0,1)) = 0.313
	}
		SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 200

		CGPROGRAM

#pragma surface surf Surface vertex:vert alpha
#pragma target 3.0


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

	void surf(Input IN, inout SurfaceOutputStandard o) {
		o.Metallic = _Metallic;
		o.Smoothness = _Glossiness;
		o.Albedo = _Color;// *IN.customAlpha;
		o.Alpha = 1;// 0.999 - (IN.customAlpha)*0.5;
	}

	ENDCG
	}
		FallBack "Standard"
}