using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        private BookListClient parent;

        /// <summary>
        /// (編集のために)データをセットする
        /// </summary>
        /// <param name="book"></param>
        public void initForEdit(Book book, BookListClient p)
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
                Book toUpdate = new Book()
                {
                    id = toEdit.id,
                    author = this.textBox2.Text,
                    title = this.textBox3.Text
                };

                await parent.UpdateBookAsync(toUpdate);

            }
        }
    }
}
