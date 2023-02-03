using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodBankLoad : MonoBehaviour
{

    private void Awake()
    {
        FMODUnity.RuntimeManager.LoadBank("Master");
        FMODUnity.RuntimeManager.LoadBank("Master.strings");
    }
}
