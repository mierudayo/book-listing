var sqlite3 = require('sqlite3').verbose();
//var db = new sqlite3.Database(':memory:');
var db = new sqlite3.Database('../db.sqlite3');

// 参考 Node.js sqlite3 awaitで思考停止する
// https://note.kiriukun.com/entry/20190915-sqlite3-on-nodejs-with-await
function get(sql, params) {
	return new Promise((resolve, reject) => {
		db.get(sql, params, (err, row) => {
			if (err) reject(err);
			resolve(row);
		});
	});
}

function run(sql, params) {
	return new Promise((resolve, reject) => {
		db.run(sql, params, (err) => {
			if (err) reject(err);
			resolve();
		});
	});
}

// sql のパラメータは引き受けていない
function each(sql, callback) {
    console.log(`each sql:${sql} callback:${callback}`)
	return new Promise((resolve, reject) => {
		db.each(sql, callback, (err, count) => {
            if (err) reject(err);
			resolve(count);
		});
	});
}

function close() {
    return new Promise((resolve, reject) => {
		db.close((err) => {
            if (err) reject(err);
			resolve();
		});
	}); 
}

(async () => {
    await run("CREATE TABLE IF NOT EXISTS books (title TEXT, author)");

    let row = await get("SELECT count(*) AS count FROM books");
    console.log(row);
    if (row.count === 0) {
        await run("INSERT INTO books VALUES ('ドラえもん', '藤子F不二雄')")
        await run("INSERT INTO books VALUES ('怪物くん', '藤子不二雄A')")
        await run("INSERT INTO books VALUES ('ののちゃん', 'いしいひさいち')")
    }

    row = await get("SELECT count(*) AS count FROM books");
    console.log(row);
    if (row.count > 0) {
        await each("SELECT rowid AS id, title, author FROM books",  function(err, row) {
            console.log(row.id + ": " + row.title + " " + row.author);
        });
    }

    close()
})()



