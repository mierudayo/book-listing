﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
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

        private const string BaseUrl = "http://localhost:8080/books";

        public BookListClient()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初期表示 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BookListClient_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        /// <summary>
        /// データをロードする
        /// </summary>
        /// <returns></returns>
        internal async Task LoadData()
        {
            try
            {
                //_dataList = new List<Book>()
                //{
                //    new Book(){id = 1, author = "著者", title = "タイトル"}
                //};
                _dataList = await GetBooksAsync(BaseUrl);
                dataGridView1.DataSource = _dataList;
                // LoadDataIntoDataGridView(_dataList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error.",
                    MessageBoxButtons.AbortRetryIgnore,
                    MessageBoxIcon.Error);
            }
        }

        // 参考
        // https://learn.microsoft.com/ja-jp/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client

        static HttpClient client = new HttpClient();

        static void ShowBook(Book Book)
        {
            Console.WriteLine($"Id: {Book.id}\tAuthor: " +
                $"{Book.author}\tTitle: {Book.title}");
        }

        /// <summary>
        /// グリッドのセルがクリックされた場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gridView = (DataGridView)sender;
            var buttonName = gridView.Columns[e.ColumnIndex].Name;
            var book = (Book)gridView.Rows[e.RowIndex].DataBoundItem;
            Console.WriteLine($"book: {book}");
            if (buttonName == "Edit")
            {
                // 編集ボタン
                EditBookForm editForm = new EditBookForm();
                editForm.initForEdit(book, this);
                editForm.ShowDialog();

            } else if (buttonName == "Delete")
            {
                // 削除ボタン
                var statusCode = await DeleteBookAsync(book.id);
                await LoadData();
            }
        }

        /// <summary>
        /// 追加ボタンが押されたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            EditBookForm editForm = new EditBookForm();
            editForm.initForAdd(this);
            editForm.ShowDialog();
        }

        /// <summary>
        /// 本の一覧を取得する
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 本を登録する
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        internal async Task<HttpResponseMessage> CreateBookAsync(Book book)
        {
            JsonContent content = JsonContent.Create<Book>(book);
            string url = $"{BaseUrl}";
            HttpResponseMessage response = await client.PostAsync(
                url, content);
            return response;
        }

        /// <summary>
        /// 本を更新する
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        internal async Task<HttpResponseMessage> UpdateBookAsync(Book book)
        {
            JsonContent content = JsonContent.Create<Book>(book);
            string url = $"{BaseUrl}/{book.id}";
            HttpResponseMessage response = await client.PutAsync(
                url, content);
            return response;
        }

        /// <summary>
        /// 本を削除する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        static async Task<HttpStatusCode> DeleteBookAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"{BaseUrl}/{id}");
            return response.StatusCode;
        }
    }
}
