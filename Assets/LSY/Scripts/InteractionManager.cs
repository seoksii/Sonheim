using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IInteractable
{
    void OnInteract();
}

public class InteractionManager : MonoBehaviour
{
    public float checkRate = 0.05f;
}
