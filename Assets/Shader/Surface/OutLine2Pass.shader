Shader "Custom/OutLine2Pass" 
{
	Properties 
	{
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_OutLineAmout ("OutLineAmout", Range(0, 1)) = 0.01
		_OutLineColor ("Outline Color", Color) = (0,0,0,1)
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		
		cull front
		//1-Pass
		CGPROGRAM
		#pragma surface surf Nolight vertex:vert noshadow noambient

		float _OutLineAmout;
		float4 _OutLineColor;

		void vert(inout appdata_full v) 
		{
			v.vertex.xyz = v.vertex.xyz + v.normal.xyz * _OutLineAmout * (sin(_Time.w) * 0.5 + 0.5);
		}

		struct Input
		{
			float4 color:COLOR;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{

		}

		float4 LightingNolight(SurfaceOutput s, float3 lightDir, float atten)
		{
			return _OutLineColor;
		}

		ENDCG
		
		cull back

		//2-Pass
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
