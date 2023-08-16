using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EPIUIController : MonoBehaviour
{
    [SerializeField] private Color equipmentTaken;
    [SerializeField] private Image[] equipmentPanels;
    [SerializeField] private TMP_Text titleTxt;


    private bool[] isEquipmentTaken;

    private void Start()
    {
        isEquipmentTaken = new bool[equipmentPanels.Length];
    }

    public void TakeEquipment(int equipNumber)
    {
        // os numeros relativos aos equipamentos seguem a mesma ordem que está no inspector (capacete 0, protetor auricular 1, oculos 2, luvas 3, botas 4)
        isEquipmentTaken[equipNumber] = true;
        ChangeUIColor();
    }

    private void ChangeUIColor()
    {
        int equipmentsAlreadyTaken = 0;
        for(int i = 0; i < equipmentPanels.Length; i++)
        {
            if (isEquipmentTaken[i])
            {
                equipmentPanels[i].color = equipmentTaken;
                equipmentsAlreadyTaken++;
            }
        }

        if (equipmentsAlreadyTaken == equipmentPanels.Length)
        {
            titleTxt.text = "Você vestiu todos os EPI's, está pronto para prosseguir para a manutenção";
        }
    }
}
