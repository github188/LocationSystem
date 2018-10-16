﻿using System.Collections.Generic;
using TEntity = Location.TModel.Location.Data.TagPosition;
using BLL;
using BLL.Blls.Location;
using LocationServices.Converters;
using DbModel.Tools;
using TModel.Tools;
using System;
using Location.TModel.LocationHistory.Data;
using System.Linq;
using Location.TModel.Tools;

namespace LocationServices.Locations.Services
{
    public interface IPosService : IEntityService<TEntity>
    {
        IList<TEntity> GetListByPerson(string person);

        IList<TEntity> GetListByArea(string area);
    }
    public class PosService : IPosService
    {
        private Bll db;

        private LocationCardPositionBll dbSet;

        public PosService()
        {
            db = new Bll(false, true, false, true);
            dbSet = db.LocationCardPositions;
        }

        public PosService(Bll bll)
        {
            this.db = bll;
            dbSet = db.LocationCardPositions;
        }

        public TEntity Delete(string id)
        {
            var item = dbSet.DeleteById(id.ToInt());
            return item.ToTModel();
        }

        public TEntity GetEntity(string id)
        {
            var item = dbSet.Find(id.ToInt());
            return item.ToTModel();
        }

        public IList<TEntity> GetList()
        {
            try
            {
                var list = dbSet.ToList().ToTModel();
                foreach (var item in list)
                {
                    if (item.Archors != null && item.Archors.Count == 0)
                    {
                        item.Archors = null;
                    }
                }
                return list.ToWCFList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IList<TEntity> GetListByName(string name)
        {
            var devInfoList = dbSet.GetListByName(name).ToTModel();
            return devInfoList.ToWCFList();
        }

        public IList<TEntity> GetListByPerson(string person)
        {
            var devInfoList = dbSet.GetListByPerson(person.ToInt()).ToTModel();
            return devInfoList.ToWCFList();
        }

        public IList<TEntity> GetListByArea(string area)
        {
            var devInfoList = dbSet.GetListByArea(area.ToInt()).ToTModel();
            return devInfoList.ToWCFList();
        }

        public TEntity Post(TEntity item)
        {
            var dbItem = item.ToDbModel();
            var result = dbSet.Add(dbItem);
            return result ? dbItem.ToTModel() : null;
        }

        public TEntity Put(TEntity item)
        {
            var dbItem = item.ToDbModel();
            var result = dbSet.Edit(dbItem);
            return result ? dbItem.ToTModel() : null;
        }

        /// <summary>
        /// 获取标签实时位置
        /// </summary>
        /// <returns></returns>
        public IList<TEntity> GetRealPositonsByTags(List<string> tagCodes)
        {
            try
            {
                var list = dbSet.DbSet.Where(tag => tagCodes.Contains(tag.Code)).ToList();
                return list.ToWcfModelList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}