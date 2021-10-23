using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackController : MonoBehaviour
{
    UnityEngine.XR.InputDevice h;
    // Start is called before the first frame update
    void Start()
    {
        var leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);

        foreach (var device in leftHandedControllers)
        {
            Debug.Log(string.Format("Device name '{0}' has characteristics '{1}'", device.name, device.characteristics.ToString()));
        }
        h = leftHandedControllers[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Vector3.zero;
        h.TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out pos);
        transform.localPosition = pos;
    }
}
