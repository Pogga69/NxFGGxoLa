using UnityEngine;
using UnityEditor;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System;

 // THIS GAME IS PROTECTED BY SONOTCLOSE'S ANTI PCVR
 // IF U FUCK UP OR DELETE THIS SCRIPT THERES OTHERS
 // SO DONT EVEN TRY FIND A NEW PROJ TO MDO SKIDDERRRRRRR

public class SillyMonke : MonoBehaviour
{
    bool free = false;
    string u = Environment.UserName;

    void Update()
    {
        if (free) return;
        free = true;
        StartCoroutine(C());
    }

    IEnumerator C()
    {
        yield return new WaitForSeconds(2.5f);

        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest
        {
            Keys = new List<string> { "AllowedUsers", "RW" }
        }, r =>
        {
            string a = r.Data.TryGetValue("AllowedUsers", out var v1) ? v1 : "";
            string w = r.Data.TryGetValue("RW", out var v2) ? v2 : "";
            string[] l = a.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string n = u.ToLower().Trim();
            bool m = Array.Exists(l, x => x.ToLower().Trim() == n);

            if (m) StartCoroutine(W(w, P1(n)));
            else
            {
                StartCoroutine(W(w, P2(n)));
                X();
            }
        }, _ => X());
    }

    IEnumerator W(string url, string p)
    {
        using (var h = new HttpClient())
        {
            var req = new HttpRequestMessage(HttpMethod.Post, url);
            req.Content = new StringContent(p, Encoding.UTF8, "application/json");

            var t = h.SendAsync(req);
            while (!t.IsCompleted) yield return null;
        }
    }

    string P1(string u)
    {
        string t = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        return JsonUtility.ToJson(new embed
        {
            content = "",
            embeds = new[]
            {
                new e
                {
                    title = "AUTHORIZED PCVR USER",
                    color = 65280,
                    author = new a { name = "ANTI PCVR BY SONOTCLOSE" },
                    fields = new[]
                    {
                        new f { name = "USER", value = $"USERNAME: {u}" },
                        new f { name = "TIME", value = t }
                    }
                }
            },
            attachments = new string[0]
        });
    }

    string P2(string u)
    {
        string t = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        return JsonUtility.ToJson(new embed
        {
            content = "",
            embeds = new[]
            {
                new e
                {
                    title = "POSSIBLE CHEATER DETECTED",
                    color = 16711680,
                    author = new a { name = "ANTI PCVR BY SONOTCLOSE" },
                    fields = new[]
                    {
                        new f { name = "USER", value = $"USERNAME: {u}" },
                        new f { name = "UNITY DETAILS", value = $"VERSION: {Application.unityVersion}\nPROJECT: {Application.dataPath}" },
                        new f { name = "TIME", value = t }
                    }
                }
            },
            attachments = new string[0]
        });
    }

    void X()
    {
#if UNITY_EDITOR
        EditorApplication.Exit(0);
#endif
    }

    [Serializable]
    public class embed
    {
        public string content;
        public e[] embeds;
        public string[] attachments;
    }

    [Serializable]
    public class e
    {
        public string title;
        public int color;
        public a author;
        public f[] fields;
    }

    [Serializable]
    public class f
    {
        public string name;
        public string value;
    }

    [Serializable]
    public class a
    {
        public string name;
    }
}
