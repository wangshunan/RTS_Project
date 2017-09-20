Shader "Custom/NewSurfaceShader" {
	SubShader{
		Tags{
			"Queue" = "Transparent"
			"RenderType" = "Transparent" 
		}
		LOD 200

		CGPROGRAM
#pragma surface surf Standard alpha:fade
#pragma target 3.0

		struct Input {
			float3 worldPos;
		};

		float4 _AreaPos;
		void surf(Input IN, inout SurfaceOutputStandard o) {
			float dist = distance(_AreaPos, IN.worldPos);
			float radius = 20;

			if (radius < dist) {
				o.Alpha = 0;
			}
			else {
				o.Albedo = fixed4(110 / 255.0, 87 / 255.0, 139 / 255.0, 1);
				o.Alpha = 0.5;
			}
		}
		ENDCG
	}
	FallBack "Diffuse"
}