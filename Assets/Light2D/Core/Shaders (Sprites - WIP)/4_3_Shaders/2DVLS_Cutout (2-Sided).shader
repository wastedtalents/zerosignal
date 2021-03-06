﻿Shader "2DVLS/Sprites/Cutout (2-Sided)"
{
	Properties
	{
		[PerRendererData] _MainTex ("Base (RGB) Trans. (Alpha)", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_Cutoff ("Alpha cutoff", Range (0,1)) = 1
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite On
		Fog { Mode Off }
		Blend DstColor DstAlpha
		 
		Pass
		{
			AlphaTest GEqual [_Cutoff]
			
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile DUMMY PIXELSNAP_ON
				#include "UnityCG.cginc"
				
				struct appdata_t
				{
					float4 vertex   : POSITION;
					float4 color    : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					fixed4 color    : COLOR;
					half2 texcoord  : TEXCOORD0;
				};
				
				fixed4 _Color;

				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
					OUT.texcoord = IN.texcoord;
					OUT.color = IN.color * _Color;
					#ifdef PIXELSNAP_ON
					OUT.vertex = UnityPixelSnap (OUT.vertex);
					#endif

					return OUT;
				}

				sampler2D _MainTex;

				fixed4 frag(v2f IN) : COLOR
				{
					return tex2D(_MainTex, IN.texcoord) * IN.color;
				}
			ENDCG
		}
	}
}
