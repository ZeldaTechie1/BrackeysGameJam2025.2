using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TazoDescriptionDisplay : MonoBehaviour
{
    [SerializeField] GameObject tazoDescriptionContainer;
    [SerializeField] TextMeshProUGUI tazoDescription;
    [SerializeField] TextMeshProUGUI tazoName;
    [SerializeField] Camera aimCamera;

    private void Update()
    {
        Vector3 modifiedScreenSpace = Mouse.current.position.ReadValue();
        modifiedScreenSpace.x /= 2.25f;
        modifiedScreenSpace.x -= 106.666f;
        modifiedScreenSpace.y /= 2.25f;

        Ray ray = aimCamera.ScreenPointToRay(modifiedScreenSpace);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Tazo t;
            hit.collider.gameObject.TryGetComponent<Tazo>(out t);

            if (t != null)
            {
                tazoDescriptionContainer.SetActive(true);
                tazoDescription.text = t.GetDescription() + $"\n\nBase Score: {t.scoreValue}";
                tazoName.text = t.Name;
            }
            else
            {
                tazoDescriptionContainer.SetActive(false);
            }
        }
    }
}
