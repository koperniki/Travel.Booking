using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using LiteDB;
using Travel.Booking.Models;

namespace Travel.Booking.Services
{
    public class BookingRepository
    {
        protected readonly string connString;

        public BookingRepository()
        {
            this.connString = "Filename=./database/booking.db; Connection=shared";
            if (!Directory.Exists("./database/"))
            {
                Directory.CreateDirectory("./database/");
            }
        }

        public void Add(User entity)
        {
            using (var db = new LiteDatabase(connString))
            {
                var col = db.GetCollection<User>("data");
                col.Insert(entity);
            }
        }

        public User Get(string id)
        {
            using (var db = new LiteDatabase(connString))
            {
                var col = db.GetCollection<User>("data");
                return col.FindById(id);
            }
        }

        public IEnumerable<User> Get(Expression<Func<User, bool>> predicate, int? count = null, int? skip = null, Expression<Func<User, object>> orderPredicate = null)
        {
            using (var db = new LiteDatabase(connString))
            {
                var col = db.GetCollection<User>("data");
                var query = col.Query().Where(predicate);
                if (orderPredicate != null)
                {
                    query = query.OrderBy(orderPredicate);
                }
                if (count.HasValue && skip.HasValue)
                {
                    return query.Skip(skip.Value).Limit(count.Value).ToList();
                }
                return query.ToList();
            }
        }


        public void Update(User entity)
        {
            using (var db = new LiteDatabase(connString))
            {
                var col = db.GetCollection<User>("data");
                col.Update(entity);
            }

        }

        public virtual void AddOrUpdate(User entity)
        {
            using (var db = new LiteDatabase(connString))
            {

                var col = db.GetCollection<User>("data");
                if (!col.Update(entity))
                {
                    col.Insert(entity);
                }
            }

        }

        public void Update(IEnumerable<User> entities)
        {
            using (var db = new LiteDatabase(connString))
            {
                var col = db.GetCollection<User>("data");
                col.Update(entities);
            }
        }

        public void Delete(string id)
        {
            using (var db = new LiteDatabase(connString))
            {
                var col = db.GetCollection<User>("data");
                col.Delete(id);
            }
        }

        public long Count()
        {
            using (var db = new LiteDatabase(connString))
            {
                var col = db.GetCollection<User>("data");
                return col.LongCount();
            }
        }
    }
}