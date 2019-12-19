const express = require('express');
const router = express.Router();

const booklist = [
      {
        id: 1,
        title: "ののちゃん",
        author: "いしいひさいち"
      },
      {
        id: 2,
        title: "ドラえもん",
        author: "藤子F不二雄"
      },
      {
        id: 3,
        title: "パーマン",
        author: "藤子F不二雄"
      },
    ];

/**
 * 書籍一覧の取得
 */
router.get('/', function(req,res,next) {
  const param = booklist;

  res.header('Content-type', 'application/json; charset=utf-8');
  res.send(param);
});

/**
 * 書籍の取得
 * 
 * id 書籍ID
 */
router.get('/:id', function(req,res,next) {
  const id = parseInt(req.params.id);
  const param = booklist.filter((value) => {return value['id'] === id;});

  res.header('Content-type', 'application/json; charset=utf-8');
  res.send(param);
});

/**
 * 書籍の削除
 * 
 * id 書籍ID
 */
router.delete('/:id', function(req,res,next) {
  const id = parseInt(req.params.id);

  let toDelete = null;
  for (let i = 0;i < booklist.length;i++) {
    const book = booklist[i];
    if (book.id === id) {
      toDelete = i;
    }
  }

  let isDelete = false;
  if (toDelete !== null && toDelete >= 0) {
    booklist.splice(toDelete, 1);
    isDelete = true;
  }

  if (isDelete) {
    // OK
    console.log(`Deleting book.id=${id} successed.`);
    res.status(200).json({status: 'ok'});
  } else {
    // error
    console.log(`Deleting book.id=${id} failed.`);
    res.status(500).json({status: 'ng'});
  }
});

/**
 * 書籍の更新
 * 
 * id 書籍ID
 */
router.put('/:id', function(req,res,next) {
  console.log("update book");
  console.log(req.params);
  console.log(req.body);
  const id = parseInt(req.params.id);
  const targets = booklist.filter((value) => {return value['id'] === id;});

  if (targets.length === 1) {
    const target = targets[0];
    const author = req.body.author;
    const title = req.body.title;
    console.log('author:' + author);
    console.log('title:' + title);
    target["author"] = author;
    target["title"] = title;

    console.log(booklist);

    // OK
    res.status(200).json({status: 'ok'});
  } else {
    // error
    res.status(500).json({status: 'ng'});
  }
});
 
/**
 * 書籍の登録
 * 
 * id 書籍ID
 */
router.post('/', function(req,res,next) {
  console.log("add book");
  console.log(req.params);
  console.log(req.body);

  // TODO ID値の重複を避けるため、同期処理が必要

  // IDの最大値を求める
  const maxId = booklist.reduce((max, value) => {
    return value['id'] > max ? value['id'] : max ;
  }, 0);

  const author = req.body.author;
  const title = req.body.title;
  console.log('author:' + author);
  console.log('title:' + title);

  if (author != null && title != null) {
    const book = {
      id: maxId + 1,
      author: author,
      title: title
    };

    booklist.push(book);
    console.log(booklist);

    // OK
    res.status(200).json({status: 'ok'});
  } else {
    // error
    res.status(500).json({status: 'ng'});
  }
});

module.exports = router;
