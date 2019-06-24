#version 330 core

in vec3 position;

uniform mat4 light_space;
uniform mat4 model;

void main()
{
    gl_Position = light_space * model * vec4(position, 1.0);
}
