'use strict';
module.exports = (sequelize, DataTypes) => {
  const book = sequelize.define('book', {
    title: DataTypes.STRING,
    author: DataTypes.STRING
  }, {
    underscored: true,
  });
  book.associate = function(models) {
    // associations can be defined here
  };
  return book;
};