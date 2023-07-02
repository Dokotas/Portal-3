using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image[] crossHairs;
    [SerializeField] private Sprite orangeCrossHair, blueCrossHair, bothCrossHair;

    private void Start()
    {
        EventManager.Singleton.OnBluePortalActive += () => crossHairs[1].sprite = blueCrossHair;
        EventManager.Singleton.OnOrangePortalActive += () => crossHairs[1].sprite = orangeCrossHair;
        EventManager.Singleton.OnBothPortalActive += () => crossHairs[1].sprite = bothCrossHair;
        EventManager.Singleton.OnChangeGun += ChangeCrossHair;
    }

    private void ChangeCrossHair(int gunIndex)
    {
        for (int i = 0; i < crossHairs.Length; i++)
            crossHairs[i].gameObject.SetActive(i == gunIndex);
    }
}