﻿using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Yuliya.DAL;
using Yuliya.DAL.Domain;

namespace Yuliya.BL
{
    public class NTicketRepository : ITicketRepository
    {
        public void Create(Ticket entity)
        {
            using (var dbSession = NSessionFactory.Instance.Create())
            {
                dbSession.Save(entity);
                dbSession.Flush();
            }
        }

        public Ticket Read(Guid key)
        {
            using (var dbSession = NSessionFactory.Instance.Create())
            {
                var criteria = dbSession.CreateCriteria<Ticket>();
                criteria.Add(Restrictions.Eq("Id", key));
                return criteria.UniqueResult<Ticket>();
            }
        }

        public void Update(Ticket entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Ticket entity)
        {
            using (var dbSession = NSessionFactory.Instance.Create())
            {
                dbSession.Delete(entity);
                dbSession.Flush();
            }
        }

        public IList<Ticket> ReadActiveTickets(Guid userToken)
        {
            using (var dbSession = NSessionFactory.Instance.Create())
            {
                var criteria = dbSession.CreateCriteria<Ticket>();
                criteria.CreateAlias("User", "user");
                criteria.Add(Restrictions.Eq("user.Token", userToken));
                return criteria.List<Ticket>();
            }
        }
    }
}