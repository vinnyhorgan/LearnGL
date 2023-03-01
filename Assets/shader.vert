#version 330 core

in vec3 position;
in vec2 textureCoords;

out vec2 pass_textureCoords;

uniform mat4 transformationMatrix;
uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;

void main()
{
    gl_Position = vec4(position, 1.0) * transformationMatrix * viewMatrix * projectionMatrix;
    pass_textureCoords = textureCoords;
}
