#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

texture2D SpriteTexture;
float Time;

sampler SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
	MinFilter = Linear;
	MagFilter = Linear;
	AddressU = Clamp;
	AddressV = Clamp;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(float4 pos : SV_POSITION, float4 color1 : COLOR0, float2 texCoord : TEXCOORD0) : SV_TARGET0
{
	float sine = sin(5*(Time + texCoord.x)) * 0.005;
	float4 screenColor = SpriteTexture.Sample(SpriteTextureSampler, float2(texCoord.x, texCoord.y + sine));
	return screenColor * float4(1, 1, 1 + sine * 5,1);
}

technique Wobble
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};