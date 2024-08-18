using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace WindowsFormsAppExperience
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5147");
            client.Timeout = new TimeSpan(0, 0, 5);
            HttpResponseMessage response = client.GetAsync("/api/database").Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("Fail");
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PostClass postClass = new PostClass();
            postClass.FirstName = "wnilnay";
            postClass.LastName = "Chen";
            postClass.LoginAccount = "wnilnay";
            postClass.Password = "Password";

            string jsonString = JsonSerializer.Serialize(postClass);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5147");
            client.Timeout = new TimeSpan(0, 0, 5);
            StringContent stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/api/database", stringContent).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("Fail");
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            PutClass putClass = new PutClass();
            putClass.LoginAccount = "wnilnay";
            putClass.Password = "Password";
            putClass.NewPassword = "NewPassword";
            string json_put = JsonSerializer.Serialize(putClass);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5147");
            client.Timeout = new TimeSpan(0, 0, 5);
            StringContent content = new StringContent(json_put, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync("/api/database", content).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DeleteClass deleteClass = new DeleteClass();
            deleteClass.LoginAccount = "wnilnay";
            deleteClass.Password = "NewPassword";

            string json_delete = JsonSerializer.Serialize(deleteClass);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete,
                "http://localhost:5147/api/database");
            request.Content = new StringContent(json_delete, Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = httpClient.SendAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5147");
            httpClient.Timeout = new TimeSpan(0, 0, 5);
            DeleteClass deleteClass = new DeleteClass();
            deleteClass.LoginAccount = "wnilnay";
            deleteClass.Password = "NewPassword";

            HttpResponseMessage response = httpClient.DeleteAsync
                ($"/api/databaseDelete/{deleteClass.LoginAccount}/{deleteClass.Password}").Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
