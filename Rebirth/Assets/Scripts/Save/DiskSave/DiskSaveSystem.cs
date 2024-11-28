using System.IO;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using System.Linq;

public class DiskSaveSystem
{
    private static string InventorySavePath => Application.persistentDataPath + "/inventory.json";
   
    #region Inventory
    public static void SaveInventoryDataToDisk(InventoryDataContainer inventoryData)
    {
        List<string> itemNames = new List<string>();
        foreach (var item in inventoryData.ThreeDimensionalItems)
        {
            itemNames.Add(item.itemName);
        }
        foreach (var item in inventoryData.TwoDimensionalItems)
        {
            itemNames.Add(item.itemName);
        }

        string json = JsonConvert.SerializeObject(itemNames, Formatting.Indented);

        File.WriteAllText(InventorySavePath, json);
    }
    
    public static async Task<InventoryDataContainer> LoadInventoryDataFromDiskAsync()
    {
        if (!File.Exists(InventorySavePath))
        {
            return null;
        }

        string json = File.ReadAllText(InventorySavePath);
        List<string> itemNames = JsonConvert.DeserializeObject<List<string>>(json);

        AsyncOperationHandle<IList<ItemData>> handle = Addressables.LoadAssetsAsync<ItemData>(
            "ItemData",
            null
        );

        await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("Failed to load ItemData from Addressables.");
            return null;
        }

        IList<ItemData> allItems = handle.Result;
        foreach (var itemData in allItems)
        {
            Debug.Log(itemData.name);
        }

        List<ItemData> loaded2DItems = new List<ItemData>();
        List<ItemData> loaded3DItems = new List<ItemData>();
        foreach (var itemName in itemNames)
        {
            ItemData foundItem = System.Array.Find(allItems.ToArray(), item => item.itemName == itemName);
            if (foundItem != null)
            {
                if (foundItem.dimension == Dimension.THREE_DIMENSION)
                    loaded3DItems.Add(foundItem);
                else
                    loaded2DItems.Add(foundItem);
            }
        }

        InventoryDataContainer inventoryData = new InventoryDataContainer(loaded2DItems, loaded3DItems);

        Addressables.Release(handle);

        return inventoryData;
    }

    #endregion

    public static string GetSceneDataPath(string sceneName) =>
        Path.Combine(Application.persistentDataPath, $"{sceneName}_sceneData.json");

    public static void SaveSceneDataToDisk(string path, SceneData sceneData)
    {
        var json = JsonUtility.ToJson(sceneData);
        File.WriteAllText(path, json);
    }

    public static async Task<Dictionary<string, SceneData>> LoadAllSceneDataFromDisk()
    {
        var sceneDatas = new Dictionary<string, SceneData>();
        var files = Directory.GetFiles(Application.persistentDataPath, "*_sceneData.json");

        var tasks = new List<Task>();
        foreach (var file in files)
        {
            tasks.Add(Task.Run(() =>
            {
                var sceneName = Path.GetFileNameWithoutExtension(file).Replace("_sceneData", "");
                var json = File.ReadAllText(file);
                var sceneData = JsonUtility.FromJson<SceneData>(json);
                lock (sceneDatas)
                {
                    sceneDatas[sceneName] = sceneData;
                }
            }));
        }
        await Task.WhenAll(tasks);

        return sceneDatas;
    }
}