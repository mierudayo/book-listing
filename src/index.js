import React from 'react';
import ReactDOM from 'react-dom';

import jquery from 'jquery';
//import './index.css';

class BookEdit extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      id: props.id,
      author: props.author,
      title: props.title
    };

    this.handleChange = this.handleChange.bind(this);

    this.handleAction = this.handleAction.bind(this);
    this.handleCancel = this.handleCancel.bind(this);
  }

  handleChange(event) {
    const name = event.target.name;
    const value = event.target.value;

    this.setState({[name]: value});
  }

  handleAction(event) {
    const book = {
      id: this.state.id,
      author: this.state.author,
      title: this.state.title
    };
    this.props.handleAction(book);
  }

  handleCancel(event) {
    this.props.handleCancel();
  }

  render() {
    return (
      <table>
        <thead>
        <tr>
          <td>ID</td>
          <td>著者名</td>
          <td>書籍名</td>
          <td></td>
        </tr>
        </thead>
        <tbody>
          <tr>
            <td>{this.state.id}</td>
            <td><input type="text"
                       name="author"
                       value={this.state.author}
                       onChange={this.handleChange}
                /></td>
            <td><input type="text"
                       name="title"
                       value={this.state.title}
                       onChange={this.handleChange}
                /></td>
            <td>
              <button onClick={this.handleAction}>登録</button>
              <button onClick={this.handleCancel}>キャンセル</button>
            </td>
           </tr>
        </tbody>
      </table>
    );
  }
}
 
function Book(props) {
  // console.log("Book");
  // console.log(props.handleEdit);
  return (
    <tr>
      <td>{props.id}</td>
      <td>{props.author}</td>
      <td>{props.title}</td>
      <td>
        <button onClick={() => props.handleEdit(props.id)}>編集</button>
         <button onClick={() => props.handleDelete(props.id)}> 削除</button>
      </td>
    </tr>
  );
}

class BookList extends React.Component {
  // constructor(props) {
  //   super(props);
  //   console.log("BookList.constructor");
  // }

  render() {
    // console.log("BookList.render");
    // console.log(this.props);
//    console.log(this.props.handleEdit(0));
    const books = this.props.booklist.map((book) => {
      return (
        <Book 
          id={book.id}
          key={book.id}
          author={book.author}
          title={book.title}
          handleEdit={this.props.handleEdit}
          handleDelete={this.props.handleDelete}
        />
      );
    });

    return (
      <table>
        <thead>
        <tr>
          <td>ID</td>
          <td>著者名</td>
          <td>書籍名</td>
          <td></td>
        </tr>
        </thead>
        <tbody>
        {books}
        </tbody>
      </table>
    );
  }
}


class App extends React.Component {
  constructor(props) {
    super(props);
    const booklist = [];

    this.state = {
      mode: "list",
      booklist: booklist
    };
  }

  /**
   * 書籍の編集
   * @param {Number} id 書籍ID
   */
  handleModeEdit(id) {
    this.setState({
      mode: "edit",
      editId: id
    });
  }

  /**
   * 書籍の削除
   * @param {Number} id 書籍ID
   */
  handleBookDelete(id) {
    console.log("handleBookDelete");
    if (window.confirm('削除してもよろしいですか？')) {
      // delete book
      // ajax call by jquery
      const my = this;
      jquery.ajax({
        url: "/books/" + id,
        method: "delete"
      })
      .done(function(data /*, textStatus, jqxhr*/) {
        console.log("done");
        my.setState({
          mode: "list"
        });
        my.changeBookList();
      })
      .fail(function(/*jqxhr, textStatus, errorThrown*/) {
        console.log("fail");
        my.setState({
          mode: "list"
        });
      });
    }
  }

  /**
   * 書籍の更新
   * @param {object} book 書籍データ {id:,title:,author:} 
   */
  handleBookUpdate(book) {
    // console.log("handleBookUpdate");

    // ajax call by jquery
    const my = this;
    jquery.ajax({
      url: "/books/" + book.id,
      method: "put",
      data: book
    })
    .done(function(data /*, textStatus, jqxhr*/) {
      console.log("done");
      my.setState({
        mode: "list"
      });
      my.changeBookList();
    })
    .fail(function(/*jqxhr, textStatus, errorThrown*/) {
      console.log("fail");
      my.setState({
        mode: "list"
      });
    });

    // ajax call by fetch
    // fetch("/books/" + book.id, {
    //   method: 'PUT',
    //   cache: 'no-cache',
    //   headers: {
    //     'Content-Type': 'application/json'
    //   },
    //   body: JSON.stringify(book)
    // })
    // .then(res => res.json())
    // .then(
    //   (result) => {
    //     console.log("handleBookUpdate:result");
    //     console.log(result);
    //   },
    //   (error) => {
    //     console.log("handleBookUpdate:error");
    //     console.log(error);
    //   }
    // ).finally( ()=> {
    //   this.setState({
    //     mode: "list"
    //   });
    //   this.changeBookList();
    // });
  }

  /**
   * 書籍の登録
   * @param {object} book 書籍データ {id:,title:,author:} 
   */
  handleBookAdd(book) {
    // ajax call by jquery
    const my = this;
    jquery.ajax({
      url: "/books",
      method: "post",
      data: book
    })
    .done(function(data /*, textStatus, jqxhr*/) {
      console.log("done");
      my.setState({
        mode: "list"
      });
      my.changeBookList();
    })
    .fail(function(/*jqxhr, textStatus, errorThrown*/) {
      console.log("fail");
      my.setState({
        mode: "list"
      });
    });

  }

  /**
   * 書籍一覧画面に遷移する
   * 
   * 一覧の更新は行わない
   */
  handleModeList() {
    this.setState({
      mode: "list"
    });
  }

  /**
   * 書籍一覧を取得する
   */
  changeBookList() {
    // ajax call by jquery
    const my = this;
    jquery.ajax({
      url: "/books/",
      method: "get"
    })
    .done(function(data /*, textStatus, jqxhr*/) {
      console.log("done");
      console.log(data);
      // 書籍データを更新
      my.setState({
        booklist: data
      });
    })
    .fail(function(/*jqxhr, textStatus, errorThrown*/) {
      console.log("fail");
    });

    // ajax call by fetch
    // fetch("/books")
    //   .then(res => res.json())
    //   .then(
    //     (result) => {
    //       console.log(result);
    //       this.setState({
    //         booklist: result
    //       });
    //     },
    //     (error) => {
    //       this.setState({
    //         booklist: []
    //       });
    //     }
    //   );
  }

  /**
   * 登録画面に遷移する
   */
  handleModeAdd() {
    console.log("add book");
    this.setState({
      mode: "add"
    });
  }

  componentDidMount() {
    // console.log("componentDidMount");
    this.changeBookList();
  }

  render() {
    // mode毎の画面を作成する。
    const my = this;
    const elemFunc = {
      // 書籍一覧画面
      list: function() {
        return (
          <div>
            <button onClick={() => my.handleModeAdd()}>登録</button>
            <BookList 
              booklist={my.state.booklist}
              handleEdit={(id) => my.handleModeEdit(id)}
              handleDelete={(id) => my.handleBookDelete(id)}
            />
          </div>
        );
      },
      // 書籍編集画面
      edit: function() {
        const books = my.state.booklist.filter((v) => {
          return v.id === my.state.editId;
        });
     
        if (books.length === 1) {
          const book = books[0];
          return (
            <BookEdit 
              key={book.id}
              id={book.id}
              author={book.author}
              title={book.title}
              handleAction={(book) => my.handleBookUpdate(book)}
              handleCancel={() => my.handleModeList()}
            />
          );
        } else {
          return null;
        }
      },
      // 書籍登録画面
      add: function () {
        return (
          <BookEdit 
            id={""}
            author={""}
            title={""}
            handleAction={(book) => my.handleBookAdd(book)}
            handleCancel={() => my.handleModeList()}
          />
        );
      }
    };

    // 画面要素を出力する
    const f = elemFunc[my.state.mode];
    const appbody = f != null ? f() : [];

    return (
<div>
<h1>book list</h1>
{appbody}
</div>
    );
  }
}


ReactDOM.render(
  <App/>,
  document.getElementById('root')
);

