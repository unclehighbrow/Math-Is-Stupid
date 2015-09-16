/*

Gradient Shader Pack - Palette Gradient

The palette gradient is a procedurally generated gradient which can be applied to ordinary geometry
where UV coordinates are available. The first UV channel is used to position and adjust the gradient.
The gradient is centered around UV coordinates 0,0. The center can be adjusted or animated within the
shader/material. Also the angle and scaling can be adjusted.

This shader does not require the use of a regular texture but does require the use of a small palette
texture. The palette can be any size, though preferably a power-of-2 for speed. A suggested size may be
256x1, or you can go as big as 4096x1, or even a tiny 2x2 texture. The first row of the palette texture
at UV coordinate 0 will be read only, even if the texture is taller. The shader will generate UV coords
for accessing the palette texture which will range horizontally from 0..1. The palette texture can contain
alpha values which will be used to blend with a background if you use the alpha-blended versions. Since
the palette texture is relatively small, this shader also can save considerable memory and download time
compared to using a large texture.

*/
