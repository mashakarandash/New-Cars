// Made with Amplify Shader Editor v1.9.6
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "AmplifyShaderPack/Triplanar Projection"
{
	Properties
	{
		_TopAlbedo("Top Albedo", 2D) = "white" {}
		_TopNormal("Top Normal", 2D) = "bump" {}
		_TriplanarAlbedo("Triplanar Albedo", 2D) = "white" {}
		_TriplanarNormal("Triplanar Normal", 2D) = "bump" {}
		_Specular("Specular", Range( 0 , 1)) = 0.02
		_Smoothness("Smoothness", Range( 0 , 1)) = 0.5
		_CoverageFalloff("Coverage Falloff", Range( 0.01 , 5)) = 0.5
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		ZTest LEqual
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf StandardSpecular keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
		};

		uniform sampler2D _TopNormal;
		uniform sampler2D _TriplanarNormal;
		uniform float _CoverageFalloff;
		uniform sampler2D _TopAlbedo;
		uniform sampler2D _TriplanarAlbedo;
		uniform float _Specular;
		uniform float _Smoothness;


inline float3 TriplanarSampling345( sampler2D topTexMap, sampler2D midTexMap, sampler2D botTexMap, float3 worldPos, float3 worldNormal, float falloff, float2 tiling, float3 normalScale, float3 index )
{
	float3 projNormal = ( pow( abs( worldNormal ), falloff ) );
	projNormal /= ( projNormal.x + projNormal.y + projNormal.z ) + 0.00001;
	float3 nsign = sign( worldNormal );
	float negProjNormalY = max( 0, projNormal.y * -nsign.y );
	projNormal.y = max( 0, projNormal.y * nsign.y );
	half4 xNorm; half4 yNorm; half4 yNormN; half4 zNorm;
	xNorm  = tex2D( midTexMap, tiling * worldPos.zy * float2(  nsign.x, 1.0 ) );
	yNorm  = tex2D( topTexMap, tiling * worldPos.xz * float2(  nsign.y, 1.0 ) );
	yNormN = tex2D( botTexMap, tiling * worldPos.xz * float2(  nsign.y, 1.0 ) );
	zNorm  = tex2D( midTexMap, tiling * worldPos.xy * float2( -nsign.z, 1.0 ) );
	xNorm.xyz  = half3( UnpackNormal( xNorm ).xy * float2(  nsign.x, 1.0 ) + worldNormal.zy, worldNormal.x ).zyx;
	yNorm.xyz  = half3( UnpackNormal( yNorm ).xy * float2(  nsign.y, 1.0 ) + worldNormal.xz, worldNormal.y ).xzy;
	zNorm.xyz  = half3( UnpackNormal( zNorm ).xy * float2( -nsign.z, 1.0 ) + worldNormal.xy, worldNormal.z ).xyz;
	yNormN.xyz = half3( UnpackNormal( yNormN ).xy * float2( nsign.y, 1.0 ) + worldNormal.xz, worldNormal.y ).xzy;
	return normalize( xNorm.xyz * projNormal.x + yNorm.xyz * projNormal.y + yNormN.xyz * negProjNormalY + zNorm.xyz * projNormal.z );
}


inline float4 TriplanarSampling343( sampler2D topTexMap, sampler2D midTexMap, sampler2D botTexMap, float3 worldPos, float3 worldNormal, float falloff, float2 tiling, float3 normalScale, float3 index )
{
	float3 projNormal = ( pow( abs( worldNormal ), falloff ) );
	projNormal /= ( projNormal.x + projNormal.y + projNormal.z ) + 0.00001;
	float3 nsign = sign( worldNormal );
	float negProjNormalY = max( 0, projNormal.y * -nsign.y );
	projNormal.y = max( 0, projNormal.y * nsign.y );
	half4 xNorm; half4 yNorm; half4 yNormN; half4 zNorm;
	xNorm  = tex2D( midTexMap, tiling * worldPos.zy * float2(  nsign.x, 1.0 ) );
	yNorm  = tex2D( topTexMap, tiling * worldPos.xz * float2(  nsign.y, 1.0 ) );
	yNormN = tex2D( botTexMap, tiling * worldPos.xz * float2(  nsign.y, 1.0 ) );
	zNorm  = tex2D( midTexMap, tiling * worldPos.xy * float2( -nsign.z, 1.0 ) );
	return xNorm * projNormal.x + yNorm * projNormal.y + yNormN * negProjNormalY + zNorm * projNormal.z;
}


		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_worldTangent = WorldNormalVector( i, float3( 1, 0, 0 ) );
			float3 ase_worldBitangent = WorldNormalVector( i, float3( 0, 1, 0 ) );
			float3x3 ase_worldToTangent = float3x3( ase_worldTangent, ase_worldBitangent, ase_worldNormal );
			float3 triplanar345 = TriplanarSampling345( _TopNormal, _TriplanarNormal, _TriplanarNormal, ase_worldPos, ase_worldNormal, _CoverageFalloff, float2( 1,1 ), float3( 1,1,1 ), float3(0,0,0) );
			float3 tanTriplanarNormal345 = mul( ase_worldToTangent, triplanar345 );
			o.Normal = tanTriplanarNormal345;
			float4 triplanar343 = TriplanarSampling343( _TopAlbedo, _TriplanarAlbedo, _TriplanarAlbedo, ase_worldPos, ase_worldNormal, _CoverageFalloff, float2( 1,1 ), float3( 1,1,1 ), float3(0,0,0) );
			o.Albedo = triplanar343.xyz;
			float3 temp_cast_1 = (_Specular).xxx;
			o.Specular = temp_cast_1;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19600
Node;AmplifyShaderEditor.TexturePropertyNode;346;773,443.2036;Float;True;Property;_TopNormal;Top Normal;1;0;Create;True;0;0;0;False;0;False;None;None;True;bump;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.TexturePropertyNode;347;767.8002,634.3035;Float;True;Property;_TriplanarNormal;Triplanar Normal;3;0;Create;True;0;0;0;False;0;False;None;None;True;bump;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.RangedFloatNode;352;724.6,-15.40111;Float;False;Property;_CoverageFalloff;Coverage Falloff;6;0;Create;True;0;0;0;False;0;False;0.5;2;0.01;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;344;819.6001,-153.6965;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TexturePropertyNode;339;771.9659,246.4995;Float;True;Property;_TriplanarAlbedo;Triplanar Albedo;2;0;Create;True;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.TexturePropertyNode;338;772.4,60.93551;Float;True;Property;_TopAlbedo;Top Albedo;0;0;Create;True;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.TriplanarNode;345;1066.474,253.2618;Inherit;True;Cylindrical;World;True;Top Texture 1;_TopTexture1;white;-1;None;Mid Texture 1;_MidTexture1;white;-1;None;Bot Texture 1;_BotTexture1;white;-1;None;Triplanar Sampler;Tangent;10;0;SAMPLER2D;;False;5;FLOAT;1;False;1;SAMPLER2D;;False;6;FLOAT;0;False;2;SAMPLER2D;;False;7;FLOAT;0;False;9;FLOAT3;0,0,0;False;8;FLOAT3;1,1,1;False;3;FLOAT2;1,1;False;4;FLOAT;2;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;213;1152,560;Float;False;Property;_Smoothness;Smoothness;5;0;Create;True;0;0;0;False;0;False;0.5;0.2;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;212;1152,480;Float;False;Property;_Specular;Specular;4;0;Create;True;0;0;0;False;0;False;0.02;0.02;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;353;1467.754,174.9927;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WireNode;354;1519.755,216.5927;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;355;1561.354,239.9927;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TriplanarNode;343;1061,67;Inherit;True;Cylindrical;World;False;Top Texture 0;_TopTexture0;white;-1;None;Mid Texture 0;_MidTexture0;white;-1;None;Bot Texture 0;_BotTexture0;white;-1;None;Triplanar Sampler;Tangent;10;0;SAMPLER2D;;False;5;FLOAT;1;False;1;SAMPLER2D;;False;6;FLOAT;0;False;2;SAMPLER2D;;False;7;FLOAT;0;False;9;FLOAT3;0,0,0;False;8;FLOAT3;1,1,1;False;3;FLOAT2;1,1;False;4;FLOAT;2;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1672.524,64.84001;Float;False;True;-1;2;ASEMaterialInspector;0;0;StandardSpecular;AmplifyShaderPack/Triplanar Projection;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;Back;0;False;;3;False;;False;0;False;;0;False;;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;0;4;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;1;False;;1;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;17;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;345;0;346;0
WireConnection;345;1;347;0
WireConnection;345;2;347;0
WireConnection;345;9;344;0
WireConnection;345;4;352;0
WireConnection;353;0;345;0
WireConnection;354;0;212;0
WireConnection;355;0;213;0
WireConnection;343;0;338;0
WireConnection;343;1;339;0
WireConnection;343;2;339;0
WireConnection;343;9;344;0
WireConnection;343;4;352;0
WireConnection;0;0;343;0
WireConnection;0;1;353;0
WireConnection;0;3;354;0
WireConnection;0;4;355;0
ASEEND*/
//CHKSM=6FBB67C56876124008C0B885E1580A28FC58DFEA