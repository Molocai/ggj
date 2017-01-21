// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Transparent"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_LighthouseLocation("Lighthouse Location", Vector) = (0.0, 0.0, 0.0, 0.0)
		_LighthouseVector("Lighthouse Vector", Vector) = (1.0, 0.0, 0.0, 0.0)
		_LighthouseWidth("Lighthouse Width", Float) = 0.5
	}
		SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 200

		CGPROGRAM

	#pragma surface surf Lambert vertex:vert alpha
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
	}
	sampler2D _MainTex;
	void surf(Input IN, inout SurfaceOutput o) {
		o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
		o.Alpha = IN.customAlpha;
	}

	ENDCG
	}
		FallBack "Standard"
}