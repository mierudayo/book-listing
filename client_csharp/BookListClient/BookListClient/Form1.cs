using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * 参考
 * 
 * https://qiita.com/te-k/items/29a17f2c53f9fe1b85cc
 */
namespace BookListClient
{
    public partial class BookListClient : Form
    {
        private List<Book> _dataList;

        public BookListClient()
        {
            InitializeComponent();
        }

        //
        // 初期表示
        //
        private async void BookListClient_Load(object sender, EventArgs e)
        {
            try
            {
                //_dataList = new List<Book>()
                //{
                //    new Book(){id = 1, author = "藤子F不二雄", title = "ドラえもん"}
                //};
                _dataList = await GetBooksAsync("http://localhost:8080/books");

                LoadDataIntoDataGridView(_dataList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error.",
                    MessageBoxButtons.AbortRetryIgnore,
                    MessageBoxIcon.Error);
            }
        }

        //
        // データをグリッドにロードする
        //
        private void LoadDataIntoDataGridView(List<Book> dataList)
        {

            dataGridView1.DataSource = dataList;

            dataGridView1.Columns.Add(new DataGridViewButtonColumn()
            {
                Name = "Edit",
                Text = "編集",
                Width = 50,
                UseColumnTextForButtonValue = true
            });

            dataGridView1.Columns.Add(new DataGridViewButtonColumn()
            {
                Name = "Delete",
                Text = "削除",
                Width = 50,
                UseColumnTextForButtonValue = true
            });

        }


        // 参考
        // https://learn.microsoft.com/ja-jp/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client

        static HttpClient client = new HttpClient();

        static void ShowBook(Book Book)
        {
            Console.WriteLine($"Id: {Book.id}\tAuthor: " +
                $"{Book.author}\tTitle: {Book.title}");
        }

        //static async Task<Uri> CreateBookAsync(Book Book)
        //{
        //    HttpResponseMessage response = await client.PostAsync(
        //        "api/Books", Book);
        //    response.EnsureSuccessStatusCode();

        //    // return URI of the created resource.
        //    return response.Headers.Location;
        //}


        static async Task<List<Book>> GetBooksAsync(string path)
        {
            List<Book> books = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string s = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"response: {s}");

                books = JsonSerializer.Deserialize<List<Book>>(s);

                //Book = await response.Content.ReadAsAsync<Book>();
            }
            return books;
        }


        static async Task<Book> GetBookAsync(string path)
        {
            Book Book = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string s = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"response: {s}");

                var doc = JsonSerializer.Deserialize<List<Book>>(s);

                //Book = await response.Content.ReadAsAsync<Book>();
            }
            return Book;
        }

        //static async Task<Book> UpdateBookAsync(Book Book)
        //{
        //    HttpResponseMessage response = await client.PutAsJsonAsync(
        //        $"api/Books/{Book.Id}", Book);
        //    response.EnsureSuccessStatusCode();

        //    // Deserialize the updated Book from the response body.
        //    Book = await response.Content.ReadAsAsync<Book>();
        //    return Book;
        //}

        //static async Task<HttpStatusCode> DeleteBookAsync(string id)
        //{
        //    HttpResponseMessage response = await client.DeleteAsync(
        //        $"api/Books/{id}");
        //    return response.StatusCode;
        //}

    }
}
