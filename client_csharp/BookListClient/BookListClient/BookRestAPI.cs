using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookListClient
{
    /// <summary>
    /// Book API にアクセスする
    /// </summary>
    class BookRestAPI
    {
        // 参考
        // https://learn.microsoft.com/ja-jp/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client

        private const string BaseUrl = "http://localhost:8080/books";

        private static HttpClient client = new HttpClient();

        /// <summary>
        /// 本の一覧を取得する
        /// </summary>
        /// <returns>本のリスト</returns>
        internal static async Task<List<Book>> GetBooksAsync()
        {
            // 空のリスト
            List<Book> books = new List<Book>();
            string url = $"{BaseUrl}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string s = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"response: {s}");

                books = JsonSerializer.Deserialize<List<Book>>(s);
            }
            return books;
        }

        /// <summary>
        /// 本を登録する
        /// </summary>
        /// <param name="book"></param>
        /// <returns>httpレスポンス</returns>
        internal static async Task<HttpResponseMessage> CreateBookAsync(Book book)
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
        /// <returns>httpレスポンス</returns>
        internal static async Task<HttpResponseMessage> UpdateBookAsync(Book book)
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
        /// <param name="id">本のid</param>
        /// <returns>httpレスポンス</returns>
        internal static async Task<HttpResponseMessage> DeleteBookAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"{BaseUrl}/{id}");
            return response;
        }
    }
}
