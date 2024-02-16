# sample_react_booklist
Reactを使用したサンプルプログラム(書籍リスト) / sample program using React (book list) 

# 目的

Reactの基本的な使用方法を学習し、簡単なアプリケーションを実際に作成する。

書籍一覧アプリは、書籍情報(著者名、書籍名)の一覧を管理する。
書籍一覧アプリは、書籍情報の登録、更新、削除を実現する。

# 主な使用ライブラリ

* react 18.2.0
* express 4.18.2
* jquery 3.6.4
* sqlite3 5.1.6
* sequelize 6.29.3

# 作業環境

本アプリの作成作業、および実行は以下の環境で行った。

```
$ uname -a
MINGW64_NT-10.0-22621 DESKTOP-87BENED 3.3.3-341.x86_64 2022-01-17 11:45 UTC x86_64 Msys

$ node -v
v18.14.2

$ npm -v
9.5.1
```

# 実行方法

nodejs、npmはインストールされている状態を前提としている。
本リポジトリをクローンしたあと、sample_react_booklistに移動して、以下を実行する。

* パッケージのインストール

```bash
$ npm install
$ npm install -g sequelize-cli
```

* アプリケーションのビルド

```bash
$ npm run build
```

* DB(sqlite)の準備

マイグレーション、およびデータのインポート

```
$ sequelize db:migrate --env development
$ sequelize db:seed:all
```

* アプリケーションの実行

```bash
$ npm run nodestart
```

アプリケーションの実行後、http://localhost:8080 にアクセスして動作を確認することができる。

## 開発のために、react-scriptsのサーバを起動する

開発時は、jsファイルを編集し、ブラウザで確認するという
作業を繰り返すが、このとき react-scripts が提供するサーバを
利用すると、jsファイルを保存したタイミングでブラウザの表示が
自動的に更新され、利便性が良くなる。

アプリケーションを実行した端末とは、別の端末を起動し、
React専用のサーバを起動する。自動的にブラウザが開き、
http://localhost:3000 にアクセスする。

```bash
$ npm run start
```

# アプリの構成

本アプリはフロントエンド側とバックエンド側で構成されている。

* フロントエンド
  - src/index.js
    - 画面表示の部品
* バックエンド
  - server.js
    - 静的コンテンツの提供
    - 書籍API(routes/books.js)の設定
  - routes/books.js
    - REST API(書籍の一覧、登録、更新、削除)を定義
    - DB操作(controllers/book.controller.js) を呼び出す
  - controllers/book.controller.js
    - DB 操作
* DB 関連
  - models/book.js
    - モデル book のモジュール
    - sequelize-cliで生成
      - ```sequelize model:create --name book --underscored --attributes title:string,author:string```
  - migrations/20200223023505-create-books.js
    - マイグレーション用データ
    - sequelize-cliでモデル作成時に生成
  - seeders/seeders/20200223045812-demo-book.js
    - 初期データ
    - sequelize-cliでテンプレート作成
      - ```sequelize seed:generate --name demo-book```

# 画面構成

本アプリでは以下の画面を持つ。

* 書籍一覧
* 書籍編集・登録

# 作業内容

本アプリは、以下のような流れで作成した。

* 初期ディレクトリの作成
* 書籍一覧の表示
    - REST APIの用意含む
* 書籍の編集
    - REST APIの用意含む
* 書籍の削除
    - REST APIの用意含む

# 作業履歴

* データベースを使用する。(20200223)
  - DB に Sqlite3 、 ORM にSequelizeを使用
* Dependabot アラート対応 (20230225)
  - Sequelize の古いバージョンに脆弱性があったため、その他ライブラリも含めて更新する。
  - 書籍の更新で、著者名が更新されないバグを修正する。

# 今後の課題

本アプリをより実践的なものにするために、以下のような課題を挙げる。

* ユーザ管理を行う。(ログイン、セッション管理)
* 画面をCSSで整形する。
* 書籍一覧画面でページネーションを実現する。
* 書籍情報から、著者情報を分離する。
* 書籍の画像を登録できるようにする。
* その他。

# 参考

* [Windowsフォームによるクライアント(C#)](./client_csharp/README.md)
    * 本リポジトリ client_csharp
* React
    * Tutorial: Intro to React
        - https://reactjs.org/tutorial/tutorial.html
    * React.Component
        - https://reactjs.org/docs/react-component.html
            - Avoid copying props into state! This is a common mistake:
    * A simple example of a confirm alert dialog in ReactJs / React.
        - https://gist.github.com/primaryobjects/aacf6fa49823afb2f6ff065790a5b402
* REST
    * Node.js + ExpressでREST API開発を体験しよう［作成編］
        - https://qiita.com/tamura_CD/items/e3abdab9b8c5aa35fa6b
    * REST – PUT vs POST
        - https://restfulapi.net/rest-put-vs-post/
* Express
    * res.sendStatus(statusCode)
        - https://expressjs.com/en/api.html#res.sendStatus
    * Body-ParserがExpressにexpress.json()として標準搭載されている話
        - https://qiita.com/atlansien/items/c587a0bf2f7f9022107c
    * Using Create-React-App with Express
        - https://dev.to/loujaybee/using-create-react-app-with-express
* Sequelize
    * Node.JSのSequelize ORM入門
        - https://qiita.com/markusveeyola/items/64875c9507d5fa32884e
    * Getting started
        - https://sequelize.org/v5/manual/getting-started
    * Node.jsのSequelizeでDBのmigrationを実行する
        - https://qiita.com/cobot00/items/0bc0da1095e09bcd0d5f
* SQLite
    * SQLite
      - https://www.sqlite.org/index.html
    * SQL As Understood By SQLite CREATE TABLE
      - https://www.sqlite.org/lang_createtable.html
        - rowid(または、oid、_rowid_)というカラムが自動的に付加される
        - "WITHOUT ROWID"をつけないテーブルは rowid table と呼ばれる
* その他
    * Tutorial: how to deploy a production React app to Heroku
        - https://medium.com/jeremy-gottfrieds-tech-blog/tutorial-how-to-deploy-a-production-react-app-to-heroku-c4831dfcfa08
