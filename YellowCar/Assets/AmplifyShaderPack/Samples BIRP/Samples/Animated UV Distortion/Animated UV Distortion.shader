// Made with Amplify Shader Editor v1.9.6
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "AmplifyShaderPack/Animated UV Distortion"
{
	Properties
	{
		_MainTexture("Main Texture", 2D) = "white" {}
		_DistortTexture("Distort Texture", 2D) = "bump" {}
		[HDR]_TintColor("Tint Color", Color) = (1,0.4196078,0,1)
		_Speed("Speed", Float) = 0
		_UVDistortIntensity("UV Distort Intensity", Range( 0 , 0.04)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _TintColor;
		uniform sampler2D _MainTexture;
		uniform float _Speed;
		uniform sampler2D _DistortTexture;
		uniform float4 _DistortTexture_ST;
		uniform float _UVDistortIntensity;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float mulTime10 = _Time.y * _Speed;
			float2 panner11 = ( mulTime10 * float2( -1,-1 ) + float2( 0,0 ));
			float2 uv_TexCoord9 = i.uv_texcoord + panner11;
			float2 uv_DistortTexture = i.uv_texcoord * _DistortTexture_ST.xy + _DistortTexture_ST.zw;
			float3 tex2DNode6 = UnpackScaleNormal( tex2D( _DistortTexture, uv_DistortTexture ), _UVDistortIntensity );
			float mulTime15 = _Time.y * _Speed;
			float2 panner17 = ( mulTime15 * float2( 1,0.5 ) + float2( 0,0 ));
			float2 uv_TexCoord18 = i.uv_texcoord + panner17;
			float4 temp_output_20_0 = ( _TintColor * ( tex2D( _MainTexture, ( float3( uv_TexCoord9 ,  0.0 ) + tex2DNode6 ).xy ) * tex2D( _MainTexture, ( float3( uv_TexCoord18 ,  0.0 ) + tex2DNode6 ).xy ) ) );
			o.Albedo = temp_output_20_0.rgb;
			o.Emission = temp_output_20_0.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19600
Node;AmplifyShaderEditor.RangedFloatNode;23;-1904,48;Float;False;Property;_Speed;Speed;3;0;Create;True;0;0;0;False;0;False;0;0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;10;-1696,-160;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;15;-1696,240;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;17;-1504,192;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,0.5;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;11;-1504,-208;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-1,-1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1616,-32;Float;False;Property;_UVDistortIntensity;UV Distort Intensity;4;0;Create;True;0;0;0;False;0;False;0;0.0286;0;0.04;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;18;-1280,144;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;9;-1280,-256;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;6;-1328,-80;Inherit;True;Property;_DistortTexture;Distort Texture;1;0;Create;True;0;0;0;False;0;False;-1;None;8ec217f770e34536be4d0dee12abb7a6;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-976,-256;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-1008,144;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;1;-832,-256;Inherit;True;Property;_MainTexture;Main Texture;0;0;Create;True;0;0;0;False;0;False;-1;None;4bd6618be485426998392fc8a5e9bc18;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SamplerNode;14;-832,128;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Instance;1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-480,-256;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;7;-768,-448;Float;False;Property;_TintColor;Tint Color;2;1;[HDR];Create;True;0;0;0;False;0;False;1,0.4196078,0,1;1.064,0.4172549,0,1;False;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-272,-448;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,-448;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;AmplifyShaderPack/Animated UV Distortion;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;17;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;10;0;23;0
WireConnection;15;0;23;0
WireConnection;17;1;15;0
WireConnection;11;1;10;0
WireConnection;18;1;17;0
WireConnection;9;1;11;0
WireConnection;6;5;13;0
WireConnection;12;0;9;0
WireConnection;12;1;6;0
WireConnection;19;0;18;0
WireConnection;19;1;6;0
WireConnection;1;1;12;0
WireConnection;14;1;19;0
WireConnection;8;0;1;0
WireConnection;8;1;14;0
WireConnection;20;0;7;0
WireConnection;20;1;8;0
WireConnection;0;0;20;0
WireConnection;0;2;20;0
ASEEND*/
//CHKSM=CC4DDA028E7AA743D5713632DDF23F2F5C0C4933