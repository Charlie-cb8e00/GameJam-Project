using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoja_boton : MonoBehaviour
{
    public GameObject panelInfo;

    public void Cerrar()
    {
        panelInfo.SetActive(false);
    }
}
