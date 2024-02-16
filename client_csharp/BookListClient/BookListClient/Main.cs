using System;
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
    public partial class Main : Form
    {
        /// <summary>
        /// 書籍リスト
        /// </summary>
        private List<Book> _dataList;

        public Main()
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
                // 次のようなデータがセットされる想定
                //_dataList = new List<Book>()
                //{
                //    new Book(){id = 1, author = "著者", title = "タイトル"}
                //};

                _dataList = await BookRestAPI.GetBooksAsync();
                dataGridView1.DataSource = _dataList;

                // 参考
                // https://learn.microsoft.com/ja-jp/dotnet/api/system.collections.generic.list-1.foreach?view=net-8.0
                // 取得したリストを出力する(その1)
                _dataList.ForEach(ShowBook);
                // 取得したリストを出力する(その2)
                _dataList.ForEach(delegate(Book book) {
                    ShowBook(book);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error.",
                    MessageBoxButtons.AbortRetryIgnore,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 本の内容を標準出力する
        /// </summary>
        /// <param name="Book">本</param>
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
                var statusCode = await BookRestAPI.DeleteBookAsync(book.id);
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
    }
}
