/*

Gradient Shader Pack - Copyright (c) 2013 Paul West/Venus12 LLC

Version 2.1


THANK YOU

Thank you for purchasing this shader package - you've made my day! Take your time to play with it, but
please take a moment when you're ready to return to the Unity asset store and add a short Review on
the product page - taking just a couple of minutes to help with this will be greatly appreciated!
Your review will help others make informed decisions and help to support me in the development of
quality assets for Unity!


WELCOME

Welcome to the Gradient Shader Pack for Unity! All shaders here are compatible with Shader Model 2.0,
Unity 3.5 or higher, and both Unity `Free` and Pro. They should work on all platforms where fragment shaders
are supported. This pack includes over 110 shaders which have been hand-written and optmized for maximum
performance on mobile and desktop. A material for each shader has been included for your convenience.


USAGE

Simply apply a material to your model as you would normally and tweak the adjustments. The shaders can
be applied to standard Unity prefabs such as the Plane or Cube etc, and there's nothing to stop you
applying them to 3D objects. All of the gradient shaders work based on UV coordinates, so your model
will need to provide appropriate UV's. Typically they only need to range from 0..1 for the gradient to
appear correctly, however the center of the gradients are at 0.0 so you may prefer to map a -1 to +1
range. Even if you use a 0..1 range, each shader includes the ability to position the center of the
gradient where you want it. Typically -0.5,-0.5 places it in the center of Unity's built-in objects,
and you can apply scaling to make it fit.

Provided are versions with `Solid Blend`, which means no blending with the existing background - these are
the fastest. Also provided are `Alpha Blended` versions which blend with the background based on alpha values,
either in the colors you specify or in the textures you use. Alpha values in `Paletted` gradients also work,
provided you use it with an alpha-blending shader. Also provided in some cases are inverted-UV versions
of the shaders, which map the gradient in the opposite direction, or inside out. This allows you to
flip the gradient or simply reverse which colors it begins and ends with to suit your needs.

Most gradients are provided with support for a single color (which fades to transparent) or two colors.
Alternatively a palette texture (or in some cases two) can be applied which will span part of the gradient.
The palette textures will have their UV coordinates computed in the shader across a 0..1 range, and the
texture can be any horizontal size e.g. 2048 x 1, or 256 x 1, preferably a power-of-2 for speed. Versions
which work with a diffuse texture can accept any kind of texture that contains RGB pixels of some kind
(ie not Apha8) and will work with compressed formats/mipmaps/filtering etc.

NEW IN VERSION 2

In Version 2 I have added some extra flexibility, and 28 new shaders.

For the No-Texture Gradient, Palette Gradient and Single-Texture Gradient: Box4, Diamond and Radial now have
an `Offset` adjustment which allows you to animate/adjust color offset from the center of the gradient.
For example you can now offset the palette from the center of a radial gradient. Essentially this allows the
gradients to be cycled in a way that wasn't possible before.

For the No-Texture Gradient and Single-Texture Gradient: I have added Box6 and Box9 as new gradient types.
Box6 is similar to Box4 except it also has a middle color on the bottom and top edges. The bottom edge
can also be offset by a given amount. Box9 is again similar but this time allows you to define color
on a 3x3 grid for extra control.

I've added a new set of gradients - Multi-Palette Gradient. These work the same as the Palette Gradient
except that you're allowed to use a 2D gradient texture. This means every row in the texture is a unique
gradient. Added adjustments allow you to animate/adjust which palette row is used. Careful preparation of
a 2D palette texture (which can be any size) coupled with animation of the Palette Row controls allows you
to animate the palette over time in a whole new way. UV coords range from 0..1 vertically.


HORIZONTAL and VERTICAL

The simplest and fastest gradients are horizontal or vertical, especially with solid blend. These simply
map to the normal 0..1 range of the UV channel. The vertical shader transitions color across the vertical
UV coordinate (V), and the horizontal shader transitions color across the horizontal UV coordinate (U).
As with most of the shaders, the center of the gradient can be adjusted, along with the scale of the
gradient and its angle (which rotates around the center). The horizontal and vertical shaders repeat
their gradient in the opposite direction in the negative UV coordinate spaces, providing a two-sided
`bar-style` gradient if desired. Textured versions allow you to render this on top of a diffuse texture
with a `multiply` blend. Paletted versions pull from a horizontal palette texture which is mapped across the
width or height of the gradient and repeated across the other dimension. The palette texture can be any
size, preferably a power of 2 for speed, and will be read using procedurally-generated UV coords in the
0..1 range. Only the first row of the palette will be accessed. The palette might be e.g. 2048 x 1. Also
as of Version 2 you can now use a multi-row palette which can be any size and the Palette Row can be
animated.

DIAMOND

The diamond gradient produdes a diagonal gradient across the 0..1 UV space, but also mirrors that shape
across the other four quadrants. Around its center, which is adjustable and centered on 0,0, it renders
a diamond gradient. The horizontal and vertical scales can be adjusted independently allowing you to
stretch the shape, and the angle can also be adjusted. Textured versions also multiply with a diffuse
texture, while paletted versions pull from a horizontal palette texture of any size, preferably power-of-2
for speed. The diamond gradients are almost as fast as the horizontal and vertical. As of Version 2 you
can now adjust the offset of color from the center of the diamond for extra animation capabilities.  Also
as of Version 2 you can now use a multi-row palette which can be any size and the Palette Row can be
animated.

BOX3

The `box3` gradients basically fill a box area covering UV coordinates 0..1. It treats two adjacent corners
as though they have their own colors, and then maps a third color to the opposite edge. The colors blend
smoothly across the `box`. This effect is offset from the center 0,0 coordinates, so if you move the center
you will actually see up to 4 mirrored boxes. The box can be scaled independently in horizontal and
vertical directions for stretching, and also rotated to any angle. Textured versions will multiply with
a diffuse texture. The paletted version will replace the two adjacent corner colors with a spread pulled
from a horizontal palette texture of any size, preferably power-of-2 for speed. The third color will then
remain spanning the opposite edge, allowing you to combine a palette with a faded tint on one side. As of
Version 2, the bottom row of the box can be offset from the top. Also as of Version 2 you can now use a
multi-row palette which can be any size and the Palette Row can be animated.

BOX4

The `box4` gradient is similar to the box3 gradient, except that unique colors are assigned to all four
corners of the box area, spanning UV coords 0..1. Again the box is mirrored across UV axis. This allows
you to assign 4 unique colors, somewhat imitating the effects of normal `vertex color` gradients. However,
the box can also have its center repositioned, can scale independently in both dimensions for stretching,
and can be rotated to any angle, all within the shader. The textured versions allows the gradient to
multiply with a diffuse texture. The paletted version actually applies two separate horizontal palette
textures. Both palettes will be read using a 0..1 UV range and can be any size, but only the first row is
accessed and ideally should be a power-of-2 for speed. In paletted mode, the box4 gradient maps one palette
to one edge of the box and the other to the opposite edge, then smoothly transitions between them. This
allows you to blend to a different pattern on opposite sides of the geometry, creating a pleasant effect.
As of Version 2 you can offset the bottom row of the box. Also as of Version 2 you can now use two multi-row
palettes which can be any size and the Palette Rows can be animated individually.

BOX6

New in Version 2, the `box6` gradient is similar to the box4 gradient, except that unique colors are
assigned to all four corners of the box area, spanning UV coords 0..1, plus a unique color is assigned to
the middle of the top row and the middle of the bottom row. Again the box is mirrored across UV axis. This
allows you to assign 6 unique colors. The box can also have its center repositioned, can scale independently
in both dimensions for stretching, and can be rotated to any angle, all within the shader. The textured
versions allows the gradient to multiply with a diffuse texture. Box6 gradients do not currently support
Palettes. You can, however, offset the bottom row of the box.

BOX6

New in Version 2, the `box9` gradient is similar to the box6 gradient, except that unique colors are
assigned on a 3x3 grid, spanning UV coords 0..1. Each corner receives its own color, the middle of each
side receives its own color, and the center receives its own color. Again the box is mirrored across UV axis.
This allows you to assign 9 unique colors. The box can also have its center repositioned, can scale
independently in both dimensions for stretching, and can be rotated to any angle, all within the shader.
The textured versions allows the gradient to multiply with a diffuse texture. Box9 gradients do not
currently support Palettes. You can, however, offset the bottom row of the box and the middle row of the
box independently.

ANGULAR

The angular gradients distribute color around a central point like a flattened cone. The full cone covers
a 1 to -1 UV range so you may want to reposition its center (normally at 0,0). It creates the effet of rays
of color emenating outward from the center. The position of the center of the gradient can be adjusted along
with the angle of the effect, since it has to begin and end at a given angle. The textured version allows
the gradient to be multiplied with a diffuse texture. The paletted version maps a horizontal palette texture
of any size, preferably a power-of-2 for speed, around the circle, allowing you to color individual `rays`
of color. The palette also allows you to make your palette `wrap around` smoothly so that there is no seam.
The angular shader is a little slower than the other gradients due to the math involved. Also
as of Version 2 you can now use a multi-row palette which can be any size and the Palette Row can be
animated.

RADIAL

The radial gradients radiate color out from the center in concentric circles, covering a 1 to -1 UV range,
so you may wish to reposition the center (normally at 0,0). The horizontal and vertical size of the radial
circles can be adjusted independently, allowing you to stretch the shape to produce ellipses. Radial gradients
do not have an adjustable angle. The textured version allows the gradient to combine with a diffuse texture.
The paletted version allows you to map a horizontal palette texture of any size, preferably a power-of-2 for
speed, onto the radial circles producing independently colored bands. This can be an easy way to, for example,
render a `sun` with a curved sunset around it. The radial shader is also a little slower than the other
gradients due to the math involved. As of version 2 you can now adjust the offset of the palette color
from the center of the radial effect. Also as of Version 2 you can now use a multi-row palette which can be
any size and the Palette Row can be animated.


ANIMATION

The gradient shaders compute and render beautiful colorful gradients in realtime. The gradients are
generated on the GPU (Graphics Processing Unit) in hardware. This means there is in most cases, except
where a texture is also applied, no texture memory needed. Even the paletted versions require very little
texture memory (e.g. you could use a 256x2 texture, or smaller, or as big as 4096x2 - note that Unity
restricts the height to no less than 2, but the gradient shader will only read the first row). The
various parameters of the gradients are exposed within the inspector for the Material. So all you need to
do is use one of the provided materials or create a new one and select the shader you want to use.
The shaders have been categorized so that from the shader selection menu within a material, you can choose
the features you want fairly easily. The names of the shaders correspond to the folder structure.

Once you have selected a material to use, you can apply it to your object quite easily using normal Unity
drag-and-drop or via selection from the material selection window. Your model must provide UV coordinates
and vertex coordinates. The UV coordinates normally should map from 0 to 1, however you may also prefer
to map 0.5 to -0.5 or 1 to -1, since many of the gradients are centered around 0,0 by default. You can however
reposition the center of the gradient within the shader itself, as well as in most cases the horizontal
and vertical scale, plus a rotation angle. You can also adjust colors, assign textures, adjust the
offset and tiling of textures (except palette textures which ignore those adjustements) and adjust the
offset of the color from the center, and in the case of the new Multi-Palette shaders you can animate
the Palette Row etc. Therefore you can access these parameters either from Unity scripting languages,
from the Unity animation timeline editor, or from other third-party visual-scripting and animation systems.

Since the gradients are computed from scratch every frame, they are completely dynamic and fully adjustable.
You can animate movement of the center of the gradient, change its size, rotate it, etc. Unlike a texture,
the gradient is calculated with extreme accuracy no matter the size it covers, creating beautifully smooth
colors at all scales - totally free from `jaggies` or texture resolution problems. You can animate the
adjustement to colors, transparency, or perhaps switch out for a different palette or texture. This shader
pack does not include scripts or other methods for producing these animations, but does leave the door open
to allow you to implement whatever kind of animation you can imagine as appropriate for your project, using
whatever method you prefer.

Play with adjusting the controls on each material to experiment with the effects you can achieve!


EXTRAS

A few courtesy shaders are included. Vertex Color shaders require you to supply vertex color data in
your mesh (instead of UV's). By default Unity objects such as the Plane or Cube do not include vertex colors.
You will likely need to generate the meshes from script or model them in a 3D modelling app. Vertex Colors
allow you to utilize standard `gouraud` shading (see http://en.wikipedia.org/wiki/Gouraud_shading).

You assign a color to each vertex of each triangle (3 per triangle) and the hardware then smoothly
interpolates between the colors to produce a gradient. In some cases this may be sufficient, but the
drawback is you have to provide the right colors to create the kind of gradients you want, they're not
easy to adjust without re-computing the mesh data, and implementing any kind of scaling, positioning or
rotation at run-time can be difficult. The gradient shaders provided make it much easier to adjust and
animate the gradients at runtime without having to modify meshes, but the vertex shaders are provided as
a legacy for completeness. A version is included which also renders a standard texture in combination with
vertex colors, for your convenience.

The pack also includes a simple `texture with a tint` shader allowing you to color a texture and also
to apply an adjustable alpha value which can animate the alpha-blending of the texture at runtime to fade
the texture in or out, with or without a tint (use white for no tint). Also included is a simple `texture-only`
shader - this is nothing special, except it's very fast at just drawing a plain diffuse texture - so is ideal
if you prefer to generate a gradient or other texture outside of Unity and simply display it.

Also included is a bundle of 40 natural `sky` palettes, pulled from photographs of real nature scenes.
They therefore represent actual sky palettes which you can use in your project to instantly create
a realistic colored sky background. The sky palettes can easily be used with the Paletted versions of the
gradient shaders, and are all 2048 x 1 in size. They have been set to `generate alpha values from grayscale`
on import, only to show that the alphablended versions of the shaders are working - they do not contain
any alpha channel data of their own. They have also be set to not generate mipmaps.

Some simple example demo scenes are also included.

Also included are some demo prefabs where various gradients have been applied to a simple Unity Plane object,
so you can quickly see an example of some settings.


TIPS, BENEFITS, IDEAS

Beautiful gradients can be used in a variety of projects and in a variety of ways. Whether you need
dynamically adjustable color or just a simple clean background, a gradient can fit the purpose. Here are
some further ideas, tips and benefits of using gradient shaders in your projects.

* Apply gradients to 3D objects, not just flat planes - provided you have some relevant UV coordinates
the gradient will apply across the object and still be adjustable.

* Set up a mesh object that has carefully crafted UV coordinates to accomplish something along the lines
of a `gradient mesh` effect. Highly complex gradients can be greated this way but it does take some effort.

* Layer multiple gradients using the alpha-blended versions - if you can't get the detail you want from
a single gradient, just use several of them on top of each other and move them around individually in
the scene view to get the effect you want.

* Combine a texture with a gradient - sometimes you just have to resort to using a texture, it could
even be a texture containing an image of a gradient you created in some other app. Bring it in and combine
it with a realtime adjustable gradient to bring it to life.

* Create beautiful backgrounds for your projects quickly and easily. You'll save significant texture storage
space by allowing the gradients to be calculated in realtime - great for mobile devices, minimizing load times,
maximizing download speeds from the web, etc. Also since the gradients are not texture-based, they are
perfect quality at any resolution without jaggies/aliasing problems.

* Add gradient backgrounds to your plain old menu screens and create a warm fuzzy glow for your users.

* Apply gradients to flat, boring buttons and other GUI elements to give them that extra subtlety that
says `quality polish`.

* Use gradients in your game environment as a beautiful dynamic sky. Use one of the included sky palettes
to instantly fill the sky with a lovely sunset or clear blue. It will be faster than using a skybox, and
save a tonne of texture memory.

* Use gradients for the background in 2D games - you know, that flat monotone background you have - bring
it to life with a nice color transition. Even animate and adjust it in-game to simulate different times of
the day, to match weather conditions, or create added mood.

* Animate your gradients to give your apps an added touch of subtlety and engagement - something moving on
the screen that isn't too imposing helps to keep eyes focussed on and excited about your app, plus smooth
gradients are soothing to the eye.

* Create an animated transition or spotlight by using a moving/adjusting gradient to wipe across a texture.
Perhaps render your game screen to a texture and use this in combination with an animated gradient to
smoothly wipe in/out to/from another screen, or to show/hide a menu.

* Apply gradients to your texture images to re-color them in-game, re-using your resources and minimizing
texture overhead. Why make the user see the same image in the background on every level - jazz it up with
a splash of color that changes as the game progresses.

* Use an animated gradient on an in-game 3D object to represent a reveal, action or transition between states,
or perhaps just to make it look cool.

* Overlay semi-transparent gradients on top of your screen and move the meshes around to animate them
as a screen-wipe effect.

* Use a vertical gradient to create a semi-transparent fog effect.

* Use an angular gradient and animate its angle of rotation to simulate a radar scanner

* Render a gradient to a RenderTexture, or grab from the backbuffer, then use that texture as input to
a second gradient shader to mix things up while animating both separately.

* Apply gradients to a 3D mesh so `undulate` the gradient shape as you animate it, or to modify its shape
in general. Although the gradient is applied to texture coordinates, the shape of the geometry does not have
to be rectangular - the gradient will wrap to the surface.

* Take a slice from a photograph to use as a palette texture. This could be a slice of sky gradient, a
sunset, or some other pattern. You could also take a slice from a 3D sphere, for example, and use it with
the radial gradient to produce a fake 3D effect. Try it with other `contours` too!

* Add a bevel to the edge of your gradient by simply applying bevel shading to one end of your palette
texture. This small cross-section of a bevel will spread across the gradient.

* Use graphics software to smooth out the sky gradients if you find them to show too many `lines` across
the image - these lines mainly come from small artefacts in the photographs they were taken from.

* Design a gradient in your favorite graphics software and use it to draw a filled rectangle there. Then
take a slice from that gradient to make a palette texture. Then use the paletted texture to re-create the
same gradient in your realtime shader.

* Apply fake lighting effects by using carefully-adjusted gradients and colors.

* Use a radial gradient with alpha blending and a gradual alpha falloff for a nice vignette or spotlight

* Create a game that renders everything entirely using gradient shaders!

* Animate a vertical bar gradient across the inside of text geometry to show a moving glint of light sheen.

* Use vertical or horizontal gradients with solid-blend for the fastest possible realtime backgrounds, still adjustabale!

* Position an alphablended radial gradient on the horizon, color it to represent the sun, then rise and set it over time with color adjustments to create a beautiful sunrise and sunset. Alphablend it over a background gradient for added mood and color.

* Apply a gradient over your sprite to animate a tint, pulsate, flash, flicker, etc.

* Use a funky color palette with the angular gradient, centered on the screen, then rotate it for a cool disco effect.

* Replace your gradient textures with realtime gradients to save lots of texture memory and improve loading times.

* Choose an interesting background, then re-color it with different gradients as part of a kiosk app or presentation.

* Create a circular mesh and apply the radial gradient to give it a fake highlight looking like a sphere.

* Create a polygon mesh representing a hilly background, then color it with lovely undulating gradients.

* Have fun, play, and remember to laugh!


ADVANCED - MODIFYING THE SHADERS

In case you need to modify the shaders, extensive comments have been included. All shaders are CG-based
in Shader Model 2.0. They have been highly optimized including efforts to minimize the size of data
variables. This means most UV coordinates for example are interpreted with `half` precision, and most
colors are dealt with in `fixed` precision. If you for any reason find your UV's are not accurate enough
or the colors are not perfect enough, convert these to `float` types throughout.

I've only included solid-blend and alpha-blend versions of the shaders. Creating versions for every possible
blend mode is an extremely lengthy task, plus Unity takes a long time to import the shaders so a large
number of them is impractical. Within the shader sourcecode, however, I have included some commented-out
lines which offer alternative blend modes such as Multiply, Soft Additive, etc. Plus there are many more
possibilities, but these should get you started. Using different blend modes will allow you to layer
the gradients in more sophisticated ways to combine then with your backgrounds and environments. I would
like to have included a full selection of 20-30 blends but it was not practical.

You will also find that in most cases where a diffuse Texture is used, the texture color is simpy multiplied
with the result of the gradient generator. If you prefer, other operations could be performed there
such as addition, subtraction, division, or some other more complex blending similar to those found in
graphics software. See my other asset - Shader Wizard - for some alternative shader code which might help.

These shaders do not support lighting or shadows or other advanced 3D techniques. They are designed to be
fast and efficient at rendering gradients, which for most people will be applied to 2D environments where
light and shadow is not so common.


NOTES

Version 2.1 is a bug-fix, numerous shaders were not compiling fully for GLSL in Unity 4.1.5.
I implemented a modification to all affected shaders and they now work in 4.1.5


THANK YOU FOR USING THE GRADIENT SHADER PACK!

Copyright (c) 2013 - Paul West/Venus12 LLC

*/