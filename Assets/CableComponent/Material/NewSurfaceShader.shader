Shader "Custom/NewSurfaceShader"
{
    Properties{
         _MainTex("Texture", 2D) = "white" {}
        _U("U", float) = 0
        _V("V", float) = 0
        _SPEED("Speed", float) = 0
    }
        SubShader{
          Tags { "RenderType" = "Opaque" }
          CGPROGRAM
          #pragma surface surf Lambert
          struct Input {
              float2 uv_MainTex;
          };
          sampler2D _MainTex;
          float _U;
          float _V;
          float _SPEED = -0.3f;

          void surf(Input IN, inout SurfaceOutput o) {
                float4 c = tex2D(_MainTex, float2(IN.uv_MainTex.x + _Time.x * _SPEED, IN.uv_MainTex.y));
              o.Emission = c.rgb;
          }
          ENDCG
        }
            Fallback "Diffuse"
}
