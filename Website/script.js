const mongo = require('mongodb');
const MongoClient = mongo.MongoClient;

const url = 'mongodb://147.156.85.248:27017';

MongoClient.connect(url, { useNewUrlParser: true }, (err, client) => {

    if (err) throw err;

    console.log(client.topology.clientInfo);

    client.close();
});