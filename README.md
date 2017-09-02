# Monogame MSDF example
This is an example of how to use MSDF texture in Monogame.

## Import MSDF shader
The MSDF shader is stored as MSDFShader.fx in the Content folder of this repository.

A shader that converts the GLSL shader that was created in Chlumsky's msdfgen repository to HLSL.



Copy this file and place it in the Content folder of the project you want to apply.

## Convert to MSDF texture

Chlumsky has created a nice converter, use it convert to MSDF texture.

Refer to his README.md guide.

Chlumsky's msdfgen repo: https://github.com/Chlumsky/msdfgen

## Monogame Content Pipeline Settings

Disable ColorKeyEnabled in the texture settings.
![tutorial_disableColorKey](https://raw.githubusercontent.com/Creta5164/Monogame-MSDF-example/master/images/ColorKeyEnabled.png)

## MSDF Shader uniforms

float  pxRange     : This determines the degree of clarity, best value is 5.

float2 textureSize : set it to texture's size (use Vector2).

float4 bgColor     : determines drawing background color in shader. (use Vector4, also can use Color -> toVector4)

float4 fgColor     : determines drawing shape color in shader. (use Vector4, also can use Color -> toVector4)

