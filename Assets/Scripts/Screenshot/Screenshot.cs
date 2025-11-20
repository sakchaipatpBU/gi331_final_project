using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Screenshot : MonoBehaviour
{
    public string gameName = "Domitory Service";

    public RawImage showImg;
    byte[] currentTexture;
    string currentFilePath;

    public GameObject screenshotPanel;
    public GameObject capturePanel;
    public GameObject saveImagePanel;


    #region not
    public static string ScreenShotName(int width, int height)
    {
        return string.Format("screen_{0}x{1}_{2}.png",
            width, height,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
    #endregion

    public string ScreenShotName()
    {
        return string.Format("{0}_{1}.png", gameName, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void Capture()
    {
        StartCoroutine(TakeScreenshot());
    }

    private IEnumerator TakeScreenshot()
    {
        HideUIforCapture();
        yield return new WaitForEndOfFrame();
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        currentFilePath = Path.Combine(Application.temporaryCachePath, "temp_img.png");

        currentTexture = screenshot.EncodeToPNG();
        File.WriteAllBytes(currentFilePath, currentTexture);
        ShowImage();
        ShowUIforCapture();
        // To avoid memory leaks
        Destroy(screenshot);
    }

    private void HideUIforCapture()
    {
        capturePanel.SetActive(false);
    }

    private void ShowUIforCapture()
    {
        capturePanel.SetActive(true);
    }

    public void ShowImage()
    {
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tex.LoadImage(currentTexture);
        showImg.material.mainTexture = tex;
        screenshotPanel.SetActive(true);
    }

    public void ShareImage()
    {
        new NativeShare().AddFile(currentFilePath)
            .SetSubject("Share The Conducktor Game").SetText("Hello world!")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + "selected app: " + shareTarget))
            .Share();
    }

    public void SaveToGallery()
    {
        /*NativeGallery.Permission permission = */
        NativeGallery.SaveImageToGallery(currentFilePath, gameName, ScreenShotName(),
            (success, path) =>
            {
                Debug.Log("Media save result: " + success + " " + path);
                if (success)
                {
                    saveImagePanel.SetActive(true);
#if UNITY_EDITOR
                    string editorFilePath = Path.Combine(Application.persistentDataPath, ScreenShotName());
                    File.WriteAllBytes(editorFilePath, currentTexture);
#endif
                }
            }
            );

        // Debug.Log("Permission result: "+ permission);
    }

}