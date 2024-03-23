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
        // JSON 파일 경로
        string path = Path.Combine("Assets", "Data", "CountryName.json");
        // JSON 파일에서 문자열 읽기
        Debug.Log(path);
        string jsonData = File.ReadAllText(path);
        // Json.NET을 사용한 역직렬화
        CountryNameJson countryName = JsonConvert.DeserializeObject<CountryNameJson>(jsonData);

        foreach(var e in countryName.elements)
        {
            if (e.tags.name_ko == null) continue;

            string overpassQuery = $"[out:json];relation[\"boundary\"=\"administrative\"][\"admin_level\"=\"2\"][\"name:ko\"=\"{e.tags.name_ko}\"];out geom;";
            string overpassUrl = "https://overpass-api.de/api/interpreter?data=" + System.Uri.EscapeDataString(overpassQuery);

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(overpassUrl);

                // JSON 데이터를 파일로 저장
                string filePath = Path.Combine("Assets", "Data", e.tags.name_ko + ".json");
                File.WriteAllText(filePath, response);
                Debug.Log($"JSON saved to {filePath}");
            }
        }
    }

    private static void MakeOb()
    {
        // JSON 파일 경로
        string namepath = Path.Combine("Assets", "Data", "CountryName.json");
        // JSON 파일에서 문자열 읽기
        //Debug.Log(namepath);
        string namejsonData = File.ReadAllText(namepath);
        // Json.NET을 사용한 역직렬화
        CountryNameJson countryName = JsonConvert.DeserializeObject<CountryNameJson>(namejsonData);
        foreach (var name in countryName.elements)
        {
            if (name.tags.name_ko == null) continue;



            string path = Path.Combine("Assets", "Data", name.tags.name_ko + ".json");
            string jsonData = File.ReadAllText(path);
            // Json.NET을 사용한 역직렬화
            List<ElementGeom> countryBounderies = JsonConvert.DeserializeObject<List<ElementGeom>>(jsonData);
            List<Vector2> locList = new List<Vector2>();
            foreach (var e in countryBounderies)
            {
                foreach (var l in e.locs)
                {
                    locList.Add(new Vector2(l.x, l.y));
                }
            }

            GameObject country = new GameObject(name.tags.name_ko);
            country.transform.parent = Selection.transforms[0];
            PolygonCollider2D polygonCollider = country.AddComponent<PolygonCollider2D>();
            polygonCollider.points = locList.ToArray();
            
            LineRenderer lineRenderer = country.AddComponent<LineRenderer>();

            // LineRenderer 설정
            lineRenderer.useWorldSpace = false; // 오브젝트의 로컬 공간을 사용
            lineRenderer.positionCount = polygonCollider.points.Length + 1; // 시작점으로 돌아오기 위해 +1

            // 폴리곤 콜라이더의 꼭짓점을 따라 선을 그립니다.
            for (int i = 0; i < polygonCollider.points.Length; i++)
            {
                lineRenderer.SetPosition(i, polygonCollider.points[i]);
            }
            // 선의 시작점으로 돌아와서 폴리곤을 닫습니다.
            lineRenderer.SetPosition(polygonCollider.points.Length, polygonCollider.points[0]);
            
        }
    }
}
#endif
