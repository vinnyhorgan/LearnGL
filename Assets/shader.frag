#version 330

in vec2 pass_textureCoords;
in vec3 surfaceNormal;
in vec3 toLightVector;

out vec4 out_Color;

uniform sampler2D textureSampler;
uniform vec3 lightColor;

void main()
{
    vec3 unitNormal = normalize(surfaceNormal);
    vec3 unitLightVector = normalize(toLightVector);

    float brightness = max(dot(unitNormal, unitLightVector), 0.0);
    vec3 diffuse = brightness * lightColor;

    out_Color = texture(textureSampler, pass_textureCoords) * vec4(diffuse, 1.0);
}
