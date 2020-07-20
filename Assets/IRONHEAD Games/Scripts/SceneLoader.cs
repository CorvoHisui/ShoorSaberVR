using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{

    public OVROverlay overlayBackground;
    public OVROverlay overlayLoadingText;
    public static SceneLoader instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadScene (string sceneName)
    {
        StartCoroutine(ShowOverlayAndLoad(sceneName));
    }
    IEnumerator ShowOverlayAndLoad (string sceneName)
    {
        overlayBackground.gameObject.SetActive(true);
        overlayLoadingText.gameObject.SetActive(true);

        GameObject centerEyeAnchor = GameObject.Find("CenterEyeAnchor");
        overlayLoadingText.gameObject.transform.position = centerEyeAnchor.transform.position + new Vector3(0,0,3);


        yield return new WaitForSeconds(3f);

        //Load Scene and wait until complete
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        overlayBackground.gameObject.SetActive(false);
        overlayLoadingText.gameObject.SetActive(false);

        yield return null;
    }
}
