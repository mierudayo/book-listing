'use strict';

module.exports = {
  up: (queryInterface, Sequelize) => {
    /*
      Add altering commands here.
      Return a promise to correctly handle asynchronicity.

      Example:
      return queryInterface.bulkInsert('People', [{
        name: 'John Doe',
        isBetaMember: false
      }], {});
    */
    return queryInterface.bulkInsert('Books', [{
        title: 'ドラえもん',
        author: '藤子F不二雄',
        created_at: new Date(),
        updated_at: new Date()
    }, {
        title: '怪物くん',
        author: '藤子不二雄A',
        created_at: new Date(),
        updated_at: new Date()
    }, {
        title: 'ののちゃん',
        author: 'いしいひさいち',
        created_at: new Date(),
        updated_at: new Date()
    }], {});
  },

  down: (queryInterface, Sequelize) => {
    /*
      Add reverting commands here.
      Return a promise to correctly handle asynchronicity.

      Example:
      return queryInterface.bulkDelete('People', null, {});
    */
    return queryInterface.bulkDelete('Book', null, {});
  }
};
