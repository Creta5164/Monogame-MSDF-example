# Monogame MSDF example

![previewGIF](https://raw.githubusercontent.com/Creta5164/Monogame-MSDF-example/master/images/Preview.gif)

This is an example of how to use MSDF texture in Monogame.

## Import MSDF shader
The MSDF shader is stored as MSDFShader.fx in the Content folder of this repository.

A shader that converts the GLSL shader that was created in Chlumsky's msdfgen repository to HLSL.

Copy this file and place it in the Content folder of the project you want to apply.

## Convert to MSDF texture

Chlumsky has created a nice converter, use it convert to MSDF texture.

Refer to his README.md guide.

Chlumsky's msdfgen repo: https://github.com/Chlumsky/msdfgen

### convert SVG format to MSDF (Chulmsky's converter guide)

![tutorial_wrongSVG](https://raw.githubusercontent.com/Creta5164/Monogame-MSDF-example/master/images/WrongSVG.png)

If you try to convert to SVG like above (you can check it open to notepad), you will get a failure to load.

![tutorial_tryingWithWrongSVG](https://raw.githubusercontent.com/Creta5164/Monogame-MSDF-example/master/images/TryingWithWrongSVG.png)

README.md of Chulmsky's converter it is written like this...

> - **-svg \<filename.svg\>** &ndash; to load an SVG file. Note that only the last vector path in the file will be used.

So, we need to try convert path from every elements in SVG file.

If you have Adobe illustrator then, you can try convert to path easier way!

Try this. (Select all the objects you want to convert, from the top menu -> **[Object]** -> **[Compound Path]** -> **[Make]**)

![tutorial_makeCompoundPath](https://raw.githubusercontent.com/Creta5164/Monogame-MSDF-example/master/images/ConvertToPathUsingIllustrator.png)

(I have using Korean version, this picture is for example.)

If you have converted it, save it as SVG and open it using notepad.

And you can see now rect, circle, other tags are changed to path tag.

![tutorial_resultSVG](https://raw.githubusercontent.com/Creta5164/Monogame-MSDF-example/master/images/CorrectSVG.png)

Let's try convert it to MSDF texture, you can see correctly result like below.

![tutorial_finalMSDFresult](https://raw.githubusercontent.com/Creta5164/Monogame-MSDF-example/master/images/CorrectMSDF.png)

![tutorial_finalMSDFrender](https://raw.githubusercontent.com/Creta5164/Monogame-MSDF-example/master/images/CorrectMSDFRender.png)
![tutorial_finalMSDFtexture](https://raw.githubusercontent.com/Creta5164/Monogame-MSDF-example/master/images/CorrectMSDFResult.png)

But you need to remember, read again this.

> - **-svg \<filename.svg\>** &ndash; to load an SVG file. **Note that only the last vector path in the file will be used**.

Yes, That's why I told you to select all the objects you want to convert.

Let's check it.

![tutorial_multiplePathSVG](https://raw.githubusercontent.com/Creta5164/Monogame-MSDF-example/master/images/MultiplePathSVG.png)

If you try this SVG to convert MSDF texture, will result makes only one object came out.

![tutorial_multiplePathSVGResult](https://raw.githubusercontent.com/Creta5164/Monogame-MSDF-example/master/images/MultiplePathSVGResult.png)

That's it!

If you do not have an illustrator, I can not tell you how to do it.

But if I have find it solution, I'll update this README.md.

If anyone else in this article knows the alternative, do pull request to this repository!

## Monogame Content Pipeline Settings

Disable ColorKeyEnabled in the texture settings.
![tutorial_disableColorKey](https://raw.githubusercontent.com/Creta5164/Monogame-MSDF-example/master/images/ColorKeyEnabled.png)

## MSDF Shader uniforms

float  pxRange     : This determines the degree of clarity, best value is 5, if you set it to negative value(for example, -5), you can make it invert color.

float2 textureSize : set it to texture's size (use Vector2).

float4 bgColor     : determines drawing background color in shader. (use Vector4, also can use Color -> toVector4)

float4 fgColor     : determines drawing shape color in shader. (use Vector4, also can use Color -> toVector4)

