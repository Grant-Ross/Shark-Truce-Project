Shader "Custom/JoepieShaderWobble"
{
	Properties
	{
		_Color("Main Color",Color) = (1,1,1,1)
		_Offset("Z-Offset", int) = 0

		_NormalOffset("Normal Offset", Float) = 0
		_TimeInfluence("Time Influence", Float) = 0
		_YDiv("Ydiv", Float) = 0
	}

		SubShader
	{
		Tags
		{
			"RenderType" = "Opaque"
			"Queue" = "Geometry"
			"IgnoreProjector" = "True"
		}

		Offset[_Offset], 0
		Cull Off

		CGPROGRAM
		#pragma surface surf Unlit vertex:vert addshadow
		#pragma target 3.0

		fixed4 _Color;
		fixed _ShadowMultiplier;

		float _NormalOffset;
		float _TimeInfluence;
		float _YDiv;


		struct Input
		{
			fixed2 uv_MainTex;
			float3 vertexColor; // Vertex color stored here by vert() method
		};


		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);


			v.vertex.x += sin(v.vertex.y * _YDiv + round(_Time.y * _TimeInfluence)) * _NormalOffset;
			v.vertex.y += sin(v.vertex.x * _YDiv + round(_Time.y * _TimeInfluence)) * _NormalOffset;
			v.vertex.z += sin(v.vertex.y * _YDiv + round(_Time.y * _TimeInfluence)) * _NormalOffset;
			o.vertexColor = v.color; // Save the Vertex Color in the Input for the surf() method
		}

		fixed4 LightingUnlit(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			fixed4 c;

			c.rgb = s.Albedo;
			c.a = s.Alpha;

			//atten = step(.1,atten) * .5;
			c.rgb *= atten;

			return c * _LightColor0;
		}

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 c = _Color;

			o.Albedo = _Color.rgb * IN.vertexColor; // Combine normal color with the vertex color
			o.Alpha = 1;
		}
		ENDCG
	}

		Fallback "VertexLit"
}
