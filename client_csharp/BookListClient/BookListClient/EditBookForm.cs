using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookListClient
{
    public partial class EditBookForm : Form
    {
        public EditBookForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 編集対象となる本
        /// </summary>
        private Book toEdit;

        private Main parent;

        /// <summary>
        /// (編集のために)データをセットする
        /// </summary>
        /// <param name="book"></param>
        public void initForEdit(Book book, Main p)
        {
            toEdit = book;
            parent = p;

            if (toEdit != null)
            {
                this.textBox1.Text = $"{toEdit.id}";
                this.textBox1.Enabled = false;
                this.textBox2.Text = toEdit.author;
                this.textBox3.Text = toEdit.title;
            }
        }

        /// <summary>
        /// 追加の場合の初期化
        /// </summary>
        /// <param name="p"></param>
        public void initForAdd(Main p)
        {
            parent = p;
            this.textBox1.Text = "";
            this.textBox1.Enabled = false;
            this.textBox2.Text = "";
            this.textBox3.Text = "";
        }

        /// <summary>
        /// (キャンセルボタンで)フォームを閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// 登録または更新処理を行う
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            if (toEdit != null)
            {
                // 更新処理
                Book toUpdate = new Book()
                {
                    id = toEdit.id,
                    author = this.textBox2.Text,
                    title = this.textBox3.Text
                };

                HttpResponseMessage response = await BookRestAPI.UpdateBookAsync(toUpdate);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("更新しました",
                       "更新しました",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                    await parent.LoadData();
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("更新に失敗しました",
                       "更新に失敗しました",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                }
            } else
            {
                // 追加処理
                Book toAdd = new Book()
                {
                    id = 0,
                    author = this.textBox2.Text,
                    title = this.textBox3.Text
                };
                HttpResponseMessage response = await BookRestAPI.CreateBookAsync(toAdd);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("追加しました",
                       "追加しました",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                    await parent.LoadData();
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("追加に失敗しました",
                       "追加に失敗しました",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                }

            }
        }
    }
}
