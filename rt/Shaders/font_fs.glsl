#version 330 core

in vec2 texture_map;

uniform sampler2D font;
uniform vec4 font_color;

out vec4 color;

void main()
{
    vec4 sampled = vec4(1.0, 1.0, 1.0, texture(font, texture_map).r);
    color = font_color * sampled;
}
