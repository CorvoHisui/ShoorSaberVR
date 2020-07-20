using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibratinManager : MonoBehaviour
{
    public static VibratinManager instance;

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

    public void VibrateController(float duration, float frequency, float amplitude, OVRInput.Controller controller)
    {
        StartCoroutine(VibrateForSeconds(duration, frequency, amplitude, controller));
    }

    IEnumerator VibrateForSeconds(float duration, float frequency, float amplitude, OVRInput.Controller controller)
    {
        //start duration
        OVRInput.SetControllerVibration(frequency, amplitude, controller);

        //execute for seconds
        yield return new WaitForSeconds(duration);

        //stop vibration
        OVRInput.SetControllerVibration(0, 0, controller);
    }
}
