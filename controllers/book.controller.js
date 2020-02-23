const db = require('../models/index.js');
const Book = db.book;

module.exports = {
    create:(req, res) => {
        console.log('create 1');
        Book.create({
          title: req.body.title,
          author: req.body.author
        }).then(book => {
            console.log('create 2');
            res.send(book);
        });
    },
    findAll:(req, res) => {
        console.log('findAll 1');
        Book.findAll({})
        .then(books => {
            console.log('findAll 2');
            console.log(books);
            res.send(books)
        });
    },
    findOne:(req, res) => {
        console.log('findOne 1');
        Book.findOne({
            where: {id: req.params.id}, 
        })
        .then(book => {
            console.log('findOne 2');
            res.send(book)
        });
    },
    update:(req, res) => {
        console.log('update 1');
        Book.update({
            title: req.body.title,
            author: req.params.author
        }, {
            where: {id: req.params.id}
        })
        .then(() => {
            console.log('update 2');
            res.status(200).send("Successfully updated a Book ID: " + req.params.id);
        });
    },
    delete:(req, res) => {
        console.log('delete 1');
        Book.destroy({
            where: { id: req.params.id }
        }).then(() => {
            console.log('delete 2');
            res.status(200).send("Successfully deleted a Book ID: " + req.params.id);
        });
    }
}
