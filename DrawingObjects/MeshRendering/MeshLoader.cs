using System;
using System.Drawing;
using System.Collections.Generic;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace TheGameDrawing.MeshRendering
{
    static class MeshLoader
    {
        public static MeshObject LoadMesh(Device targetDevice, string pathsToObject, string pathToTexture)
        {
            //загружает меш-объект, имеющий 1 текстуру
            MeshObject mesh = new MeshObject(); 
            mesh.meshObject = Mesh.FromFile(pathsToObject, MeshFlags.Managed, targetDevice);
            mesh.meshTextures = new Texture[1];
            mesh.meshTextures[0] = TextureLoader.FromFile(targetDevice, pathToTexture);
            mesh.meshMaterials = new Material[1];
            mesh.meshMaterials[0].Ambient = Color.White;
            mesh.meshMaterials[0].Diffuse = Color.White;
            return mesh;
        }

        public static MeshObject LoadMesh(Device targetDevice, string pathToObject)
        {
            //загружает меш-объект, имеющий неск-ко мат-алов и текстур
            MeshObject mesh = new MeshObject();
            ExtendedMaterial[] mtrl;
            mesh.meshObject = Mesh.FromFile(pathToObject, MeshFlags.IbManaged, targetDevice, out mtrl);
            if ((mtrl != null) && (mtrl.Length > 0))
            {
                mesh.meshMaterials = new Material[mtrl.Length];
                mesh.meshTextures = new Texture[mtrl.Length];
                for (int j = 0; j < mtrl.Length; j++)
                {
                    mesh.meshMaterials[j] = mtrl[j].Material3D;
                    if ((mtrl[j].TextureFilename != null) && (mtrl[j].TextureFilename != string.Empty))
                    {
                        string srcFile = "textures\\" + mtrl[j].TextureFilename;
                        mesh.meshTextures[j] = TextureLoader.FromFile(targetDevice, srcFile);
                    }
                }
            }
            return mesh;
        }
    }

}
