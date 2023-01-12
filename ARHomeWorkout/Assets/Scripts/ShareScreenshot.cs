using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.XR.ARFoundation;

public class ShareScreenshot : MonoBehaviour
{
	[SerializeField] private GameObject mainMenuCanvas;
	private ARPointCloudManager aRPointCloudManager;

    // Start is called before the first frame update
    void Start()
    {
		aRPointCloudManager = FindObjectOfType<ARPointCloudManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeScreenShot()
    {
		TurnOnOffARContents();
		StartCoroutine(TakeScreenshotAndShare());
    }

	private void TurnOnOffARContents()
    {
		var points = aRPointCloudManager.trackables;
		foreach(var point in points)
        {
			point.gameObject.SetActive(!point.gameObject.activeSelf);
        }
		mainMenuCanvas.SetActive(!mainMenuCanvas.activeSelf);
    }


	private IEnumerator TakeScreenshotAndShare()
	{
		yield return new WaitForEndOfFrame();

		Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		ss.Apply();

		string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
		File.WriteAllBytes(filePath, ss.EncodeToPNG());

		// To avoid memory leaks
		Destroy(ss);

		new NativeShare().AddFile(filePath)
			.SetSubject("Subject goes here").SetText("¡Hola, únete a la comunidad de ARHomeWorkout y ponte en forma!").SetUrl("https://mega.nz/file/wMhw0SiT#-5xri68o5wJhj2H4XGCjLdbvXgiGpOII_Lg42lePncI")
			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
			.Share();
		     TurnOnOffARContents();
		// Share on WhatsApp only, if installed (Android only)
		//if( NativeShare.TargetExists( "com.whatsapp" ) )
		//	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
	}
}
