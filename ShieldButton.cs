using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;

public class ShieldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Player Player{ get; set; }

    public void OnPointerDown(PointerEventData data) => Player.SetPlayerUnderSecurity();

    public void OnPointerUp(PointerEventData data) => Player.ExitFromSecurity();
}
