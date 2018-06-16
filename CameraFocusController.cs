using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CameraFocusController : MonoBehaviour {

    private bool mVuforiaStarted = false;
    private bool mFlashEnabled = false;
    // Use this for initialization
	void Start ()
    {
        VuforiaARController vuforia = VuforiaARController.Instance;

        if (vuforia != null)
            vuforia.RegisterVuforiaStartedCallback(StartAfterVuforia);
	}
	
    private void StartAfterVuforia()
    {
        mVuforiaStarted = true;
        SetAutoFocus();
    }

    void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            //App resumed
            if (mVuforiaStarted)
            {
                //App resumed and vuforia already started
                //but lets start it again...
                SetAutoFocus(); // This is done because some android devices lose the auto focus after resume
                // this was a bug in vuforia 4 and 5. I haven't checked 6, but the code is harmless anyway
            }
        }
    }

    //this script will switch automatically to Auto focus continous mode
    private void SetAutoFocus()
    {
        if (CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO))
        {
            Debug.Log("Autofocus set");
        }
        else
        {
            //never actually seem a device that doesn't support this, but just in case
            Debug.Log("this device doesn't support auto focus");
        }
    }
}
