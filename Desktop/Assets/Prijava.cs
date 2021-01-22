using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using UnityEngine;

public class Prijava : MonoBehaviour, ISwitchable
{
    public InputField i1, i2;

    public async System.Threading.Tasks.Task CloseAsync()
    {
        string ime = "";
        string lozinkaRaw = "";
        object userInfos = new { username = ime, password = lozinkaRaw };
        var jsonObj = JsonUtility.ToJson(userInfos);
        var httpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true
        };
        using (HttpClient client = new HttpClient(httpHandler))
        {
            Debug.Log(ime + " " + lozinkaRaw);
            StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("https://localhost:2.5001/api/User/Login"),
                Method = HttpMethod.Post,
                Content = content
            };

            var response = await client.SendAsync(request);
            var dataResult = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonUtility.FromJson<LoginModel>(dataResult);
                Application.Current.Properties["token"] = result.Token;
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                Username = string.Empty;
                Password = string.Empty;
                Toggle = false;
            }
            else
            {
                Username = string.Empty;
                Password = string.Empty;
                Toggle = true;
            }
        }
    }
    public void Close()
    {
        CloseAsync().Wait();
    }

    public void Open()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
