/*

Gradient Shader Pack - Single Texture with Gradient

The single texture with gradient is a procedurally generated gradient which can be applied to ordinary
geometry where UV coordinates are available. The first UV channel is used to position a regular texture.
The second UV channel is used to position and adjust the gradient. The gradient is centered around UV
coordinates 0,0. The center can be adjusted or animated within the shader/material. Also the angle and
scaling can be adjusted.

This shader pulls in a single texture and combines the gradient with it. This means you will still be using
texture memory for your image, but the gradient can remain dynamic and take up almost no additional memory.
This allows you to re-color and re-use, or apply effects to, your existing texture to make the most of it
and save on memory and download time.

*/
