using System.IO;
using System.Text;
using System.Threading.Tasks;
using SFB;
using UnityEngine;
using UnityEngine.Networking;

namespace ImageGray
{
    public class ImageGray : MonoBehaviour
    {
#region === VARIABLES ===
        private string[] m_pathsToImage;
        private string m_pathToSaveImage;
        private Texture2D m_texture;
        private StringBuilder m_stringBuilder = new StringBuilder();

        private readonly ExtensionFilter[] m_extensionImageFilter = new [] {
            new ExtensionFilter("Image file (png)", "png")
        };

        private const string TITLE_OPEN_FILE = "Open File";
        private const string TITLE_SAVE_FILE = "Save File";
        private const string IMAGE_POSTFIX = "_gray.png";
#endregion

#region === DO SOME CODE ===
        public async void OpenImage()
        {
            m_pathsToImage = StandaloneFileBrowser.OpenFilePanel(TITLE_OPEN_FILE, string.Empty, m_extensionImageFilter, false);

            if (m_pathsToImage.Length == 0 || m_pathsToImage[0] == string.Empty)
            {
                return;
            }

            await GetImageWebRequest(new System.Uri(m_pathsToImage[0]).AbsoluteUri);
            ConvertToGrayscale();
        }

        public void SaveImage(byte[] textureBytes)
        {
            m_stringBuilder.Append(GetCutString());
            m_stringBuilder.Append(IMAGE_POSTFIX);

            m_pathToSaveImage = StandaloneFileBrowser.SaveFilePanel(TITLE_SAVE_FILE, string.Empty, m_stringBuilder.ToString(), m_extensionImageFilter);
            m_stringBuilder.Clear();

            if (!string.IsNullOrEmpty(m_pathToSaveImage))
            {
                File.WriteAllBytes(m_pathToSaveImage, textureBytes);
            }
        }

//      обрезка полного пути файла вместе с .png
        private string GetCutString()
        {
            int index = m_pathsToImage[0].LastIndexOf(Path.DirectorySeparatorChar) + 1;
            return m_pathsToImage[0].Substring(index, m_pathsToImage[0].Length - 4 - index);
        }

        private async Task GetImageWebRequest(string url)
        {
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                request.SendWebRequest();

                while (!request.isDone)
                {
                    await Task.Yield();
                }

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(request.error);
                    return;
                }

                m_texture = DownloadHandlerTexture.GetContent(request);
            }
        }

        private void ConvertToGrayscale()
        {
            Color32[] pixels = m_texture.GetPixels32();
            Color32 pixel;
            Color c;
            int parameter;
            int blue;
            int green;
            int red;
            float l;

            for (int x = 0; x < m_texture.width; x++)
            {
                for (int y = 0; y < m_texture.height; y++)
                {
                    pixel = pixels[x + y * m_texture.width];

                    parameter = ((256 * 256 + pixel.r) * 256 + pixel.b) * 256 + pixel.g;
                    blue = parameter % 256;
                    parameter = parameter / 256;
                    green = parameter % 256;
                    parameter = parameter / 256;
                    red = parameter % 256;
                    l = (0.2126f * red / 255f) + 0.7152f * (green / 255f) + 0.0722f * (blue / 255f);

                    c = new Color(l, l, l,1);
                    m_texture.SetPixel(x, y, c);
                }
            }

            m_texture.Apply(false);
            SaveImage(m_texture.EncodeToPNG());
        }
#endregion
    }
}
