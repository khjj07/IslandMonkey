Shader"Custom/FlagWave"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WaveAmplitude ("Wave Amplitude", Range(0,1)) = 0.1
        _WaveFrequency ("Wave Frequency", Range(0,10)) = 1.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
LOD100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"
            
struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 vertex : SV_POSITION;
};

sampler2D _MainTex;
float _WaveAmplitude;
float _WaveFrequency;

v2f vert(appdata v)
{
    v2f o;
    float wave = _WaveAmplitude * sin(_WaveFrequency * v.vertex.x + _Time.y);
    v.vertex.y += wave;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    return o;
}

half4 frag(v2f i) : SV_Target
{
    half4 col = tex2D(_MainTex, i.uv);
    return col;
}
            ENDCG
        }
    }
}