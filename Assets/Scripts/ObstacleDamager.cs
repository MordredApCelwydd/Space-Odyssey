using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class ObstacleDamager : MonoBehaviour
{
    public void ChangeState(bool state)
    {
        this.GameObject().SetActive(state);
    }
}
