#version 330

in vec3 colour;

out vec4 out_Color;

void main()
{
    out_Color = vec4(colour, 1.0);
}
