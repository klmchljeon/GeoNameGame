#if UNITY_EDITOR
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using static PlasticPipe.PlasticProtocol.Messages.Serialization.ItemHandlerMessagesSerialization;
using Codice.Client.Common;

public class OverpassQueryEditor : EditorWindow
{
    [MenuItem("Custom Tools/Fetch CountryTags")]
    public static void FetchAndSaveBoundaries()
    {
        var window = GetWindow<OverpassQueryEditor>();
        window.Show();
    }

    private async void OnGUI()
    {
        if (GUILayout.Button("Fetch CountryTags"))
        {
            await FetchBoundaries();
        }
        else if (GUILayout.Button("MakeOb"))
        {
            MakeOb();
        }
    }

    private static async Task FetchBoundaries()
    {
        // JSON ���� ���
        string path = Path.Combine("Assets", "Data", "CountryName.json");
        // JSON ���Ͽ��� ���ڿ� �б�
        Debug.Log(path);
        string jsonData = File.ReadAllText(path);
        // Json.NET�� ����� ������ȭ
        CountryNameJson countryName = JsonConvert.DeserializeObject<CountryNameJson>(jsonData);

        foreach(var e in countryName.elements)
        {
            if (e.tags.name_ko == null) continue;

            string overpassQuery = $"[out:json];relation[\"boundary\"=\"administrative\"][\"admin_level\"=\"2\"][\"name:ko\"=\"{e.tags.name_ko}\"];out geom;";
            string overpassUrl = "https://overpass-api.de/api/interpreter?data=" + System.Uri.EscapeDataString(overpassQuery);

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(overpassUrl);

                // JSON �����͸� ���Ϸ� ����
                string filePath = Path.Combine("Assets", "Data", e.tags.name_ko + ".json");
                File.WriteAllText(filePath, response);
                Debug.Log($"JSON saved to {filePath}");
            }
        }
    }

    private static void MakeOb()
    {
        // JSON ���� ���
        string namepath = Path.Combine("Assets", "Data", "CountryName.json");
        // JSON ���Ͽ��� ���ڿ� �б�
        //Debug.Log(namepath);
        string namejsonData = File.ReadAllText(namepath);
        // Json.NET�� ����� ������ȭ
        CountryNameJson countryName = JsonConvert.DeserializeObject<CountryNameJson>(namejsonData);
        foreach (var name in countryName.elements)
        {
            if (name.tags.name_ko == null) continue;



            string path = Path.Combine("Assets", "Data", name.tags.name_ko + ".json");
            string jsonData = File.ReadAllText(path);
            // Json.NET�� ����� ������ȭ
            CountryGeomJson countryBounderies = JsonConvert.DeserializeObject<CountryGeomJson>(jsonData);
            List<Vector2> locList = new List<Vector2>();
            foreach (var e in countryBounderies.elements)
            {
                foreach (var w in e.members)
                {
                    if (w.role != "outer") continue;
                    foreach (var loc in w.geometry)
                    {
                        locList.Add(new Vector2(loc.y, loc.x));
                    }
                }
            }

            List<Vector2> locList2 = new List<Vector2>();
            int num = locList.Count / 50;
            int cnt = 0;
            foreach (Vector2 v in locList)
            {
                cnt++;
                if (cnt == num)
                {
                    cnt = 0;
                    locList2.Add(v);
                }
            }

            Debug.Log(locList2.Count);
            GameObject country = new GameObject(name.tags.name_ko);
            country.transform.parent = Selection.transforms[0];
            PolygonCollider2D collider2D = country.AddComponent<PolygonCollider2D>();
            collider2D.points = locList2.ToArray();
        }
    }
}
#endif
