using System.Collections.Generic;
using UnityEngine;
using Base.Tools;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
    [SerializeField]
    private UIPanel[] uipanels = null;

    //private UIPanel[] runtimePanels;
    private List<UIPanel> _runtimePanels = new List<UIPanel>();

    public Transform defaultLayout;

    public T ShowPanel<T>() where T : UIPanel
    {
        T panel = GetPanel<T>();
                
        panel.Show();
        panel.transform.SetParent(defaultLayout, false);
        panel.transform.SetAsLastSibling();
        
        return panel;
    }

    public T HidePanel<T>() where T : UIPanel
    {
        T panel = GetPanel<T>();
        
        panel.Hide();

        return panel;
    }

    protected override void SingletonAwake()
    {
        //runtimePanels = new UIPanel[uipanels.Length];

        for (int i = 0; i < uipanels.Length; i++)
        {
            //Check.IsTrue(uipanels[i] != null, $"UIPanel at index {i} is null!");
            
            UIPanel runtimePanel = Instantiate(uipanels[i], defaultLayout).GetComponent<UIPanel>();
            runtimePanel.gameObject.SetActive(false);

            _runtimePanels.Add(runtimePanel);
        }
    }

    public void ResetPanels()
    {
        foreach (UIPanel panel in _runtimePanels)
        {
            panel.ResetPanel();
        }
    }

    public void HideAllPanel()
    {
        foreach (UIPanel panel in _runtimePanels)
        {
            panel.Hide();
        }
    }

    public T GetPanel<T>() where T : UIPanel
    {
        foreach (UIPanel panel in _runtimePanels)
        {
            if (panel is T uiPanel)
            {
                return uiPanel;
            }
        }

        return null;
    }
}