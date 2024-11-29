using System.Collections;
using UnityEngine;

public class SaveManager : SingletonManager<SaveManager>
{
    public static event System.Action save;
    public static event System.Action load;
    public float autoSaveInterval = 300f; 
    private bool isAutoSaveEnabled = true;

    protected override void Awake()
    {
        base.Awake();
        LoadGame();
    }

    private void Start()
    {
        // LoadGame();
        StartCoroutine(AutoSaveCoroutine()); 
    }

    private IEnumerator AutoSaveCoroutine()
    {
        while (isAutoSaveEnabled)
        {
            yield return new WaitForSeconds(autoSaveInterval); 
            SaveGame(); 
            Debug.Log("자동저장 되었습니다.");
        }
    }

    public void SaveGame()
    { 
        save?.Invoke();
    }

    public void LoadGame()
    {
        load?.Invoke();
    }
}
