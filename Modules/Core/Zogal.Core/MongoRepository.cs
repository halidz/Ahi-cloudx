using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zogal.Core
{
    public class MongoRepository :IRepository 
    {
        private readonly IMongoCollection<IEntity> mongoCollection;
        public MongoRepository(string mongoDBConnectionString, string dbName, string collectionName)
        {
            var client = new MongoClient(mongoDBConnectionString);
            var database = client.GetDatabase(dbName);
            mongoCollection = database.GetCollection<IEntity>(collectionName);
        }   
        public void Delete<T>(T entity) where T : class, IEntity
        {
            mongoCollection.DeleteOne(m => m.Id == entity.Id);
        }

        public T Get<T>(long id) where T : class, IEntity
        {
            
            return (T)mongoCollection.Find(x => x.Id == id).FirstOrDefault();
        }

        public IQueryable<T> Query<T>() where T : class, IEntity
        {
            throw new NotImplementedException();
        }

        public long Save<T>(T entity) where T : class, IEntity
        {
            mongoCollection.InsertOne(entity);
            return entity.Id;

        }
    }
}
