/*

Gradient Shader Pack - Vertex Color Gradient with Texture

The vertex-color with texture shader is provided as a courtesy and allows you to create gradients using the
built-in Vertex Color vertex attribute, and also combine it with a simple diffuse texture. Provided you create
a mesh which has vertex colors in it, this shader will render only those colors. It multiplies the gradient
by the texture colors only and is extremely efficient at rendering only this special case scenario. This allows
you to apply a reltime-adjustable tint for re-coloring/re-use of your textures to save on texture storage
or download time. In order to use this shader you will additionally need UV coordinates in your mesh so
that the texture can be rendered, but the UV coordinates will have nothing to do with the vertex colors.

Vertex colors allow you to assign a unique color to each vertex in your mesh, which typically means each
corner of each triangle in the mesh. Standard graphics hardware has been providing `gouraud shading` for
many years. This is simply where the colors at each vertex are smoothly and linearly interpolated across
the triangle, creating a pleasant `gradient`. This system is supported on all hardware and is fast.

However, it has some limitations. You can only define 3 colors per triangle. In order to change the colors
you have to change the mesh data and resubmit the mesh to the graphics card. If you want to animate the
colors in some way this means changing the mesh data every frame. You also cannot easily adjust the
gradient effect - such as scaling it, rotating it, repositioning it, etc. without complex calculations
being performed in a scripting language, plus resubmission to the graphics card. Moving a gradient across
a complex 3D mesh spanning multiple vertices is a significant process.

Vertex colors/gouraud shading can provide simple and pleasant gradients, but may only suit special
circumstances. The beauty of the Gradient Shader Pack is that it provides realtime-generated gradients
that are adjusted within the shader/material at runtime, without having to resubmit any mesh data. Once
your mesh is in the scene, all you need to do is tweak some simple material parameters and you can
adjust all features of the gradient, plus implement much more sophisticated gradients such as those that
use a palette. Provided the UV coordinates are set appropriately, realtime gradients can span an entire
3D mesh and be animated in realtime with minimal overhead.

The vertex-color shaders do not require any UV coordinates in the mesh since they work from Vertex Color data.
This shader pack does not include any scripts or other methods for creating meshes with vertex colors,
and Unity's standard prefab objects do not include vertex colors - you will likely need to generate
these meshes procedurally from script in order to use these shaders.

*/
