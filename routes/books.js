const express = require('express');
const router = express.Router();
const controller = require('../controllers/book.controller');

/**
 * 書籍一覧の取得
 */
router.get('/', controller.findAll);

/**
 * 書籍の取得
 * 
 * id 書籍ID
 */
router.get('/:id', controller.findOne);

/**
 * 書籍の登録
 * 
 * id 書籍ID
 */
router.post('/', controller.create);

/**
 * 書籍の更新
 * 
 * id 書籍ID
 */
router.put('/:id', controller.update);

/**
 * 書籍の削除
 * 
 * id 書籍ID
 */
router.delete('/:id', controller.delete);

module.exports = router;
