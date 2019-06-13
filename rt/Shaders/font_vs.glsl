#version 330 core

in vec4 vertex;

uniform mat4 projection;

out vec2 texture_map;

void main()
{
    gl_Position = projection * vec4(vertex.xy, 0.0, 1.0);
    texture_map = vertex.zw;
}
