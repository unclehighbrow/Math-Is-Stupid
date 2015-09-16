/*

Gradient Shader Pack - Fixed Color with Texture

The fixed-color with texture shader simply applies one diffuse texture and multiplies it by a single color.
The color can include an alpha value. When the alpha value is adjusted and used with the alpha-blended version,
this can be used to fade out the texture. The texture may not otherwise include any alpha values in its
pixel data, so this can be handy to fade in/out a texture - e.g. setting the color to white and adjusting
the alpha value. It is also useful for applying an all-over tint to a texture, or where you want a flat
coloring instead of a gradient, and is very efficient at this special-case scenario.

*/