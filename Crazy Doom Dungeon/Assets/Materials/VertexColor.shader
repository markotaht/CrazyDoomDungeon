Shader "Custom/VertexColor" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		//_RimColor ("RimColor", Color) = (1,1,1,1)
		//_MainTex ("Albedo (RGB)", 2D) = "white" {}
		//_Glossiness ("Smoothness", Range(0,1)) = 0.5
		//_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		Cull off
		
		CGPROGRAM
		#pragma surface surf BlinnPhong fullforwardshadows
		#pragma target 3.0


		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			fixed4 color : COLOR;
			float3 viewDir;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _RimColor;

		void surf (Input IN, inout SurfaceOutput o) {

			/*if (sin( _Time.z + IN.worldPos.y * 50) < 0){
				discard;
			}*/
			//o.Normal;
			fixed4 c = _Color * IN.color;
			fixed4 rim = 1 - dot(IN.viewDir, o.Normal);
			//rim = rim * abs(sin(_Time.z + IN.worldPos.x));
			o.Albedo = c.rgb;
			//o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;
			o.Alpha = c.a;
			o.Emission = rim * _RimColor;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
