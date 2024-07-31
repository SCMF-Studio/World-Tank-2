using UnityEngine;
using TMPro;

public class ManagerGame : MonoBehaviour
{
    public GameObject ts0001, th0001, ta0001, tl0001, ready, hud, choise;
    public GameObject tank_ts0001;
    public TextMeshProUGUI info;
    private bool isTankSelected = false;
    private GameObject selectedTank;

    void Start()
    {
        if (info == null)
        {
            info = GetComponent<TextMeshProUGUI>();
        }

        tank_ts0001.SetActive(false);
        hud.SetActive(false);
        choise.SetActive(true);
        info.text = "Your choice: No tank selected";
    }

    public void ChangeText(string newText)
    {
        info.text = newText;
    }

    public void Ready()
    {
        if (isTankSelected)
        {
            hud.SetActive(true);
            choise.SetActive(false);
        }
        else
        {
            info.text = "Your choice: No tank selected";
        }
    }

    private void SelectTank(string choiceText, GameObject selectedTank)
    {
        info.text = choiceText;
        isTankSelected = true;

        
        tank_ts0001.SetActive(false);
        

        selectedTank.SetActive(true);
        this.selectedTank = selectedTank;
    }
    public void TankStandart0001()
    {
        SelectTank("Your choice: TS-0001", tank_ts0001);
    }

    public void TankHuge0001()
    {
        SelectTank("Your choice: TH-0001", th0001);
    }

    public void TankAverage0001()
    {
        SelectTank("Your choice: TA-0001", ta0001);
    }

    public void TankLittle0001()
    {
        SelectTank("Your choice: TL-0001", tl0001);
    }

}
