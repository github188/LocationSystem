﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using BLL;
using DbModel.Location.AreaAndDev;
using DbModel.Tools;
using Location.BLL.ServiceHelpers;
using Location.Model.DataObjects.ObjectAddList;
using Location.TModel.FuncArgs;
using Location.TModel.Location;
using Location.TModel.Location.AreaAndDev;
using Location.TModel.Location.Data;
using Location.TModel.Location.Obsolete;
using Location.TModel.Location.Alarm;
using Location.TModel.Location.Person;
using Location.TModel.LocationHistory.Data;
using LocationServices.Converters;
using LocationServices.Tools;
using LocationWCFService;
using LocationWCFService.ServiceHelper;
using ConfigArg = Location.TModel.Location.AreaAndDev.ConfigArg;
using DevInfo = Location.TModel.Location.AreaAndDev.DevInfo;
using KKSCode = Location.TModel.Location.AreaAndDev.KKSCode;
using Post = Location.TModel.Location.AreaAndDev.Post;
using Dev_DoorAccess = Location.TModel.Location.AreaAndDev.Dev_DoorAccess;
using TModel.BaseData;

namespace LocationServices.Locations
{
    //人员相关的接口
    public partial class LocationService : ILocationService, IDisposable
    {
        #region 人员列表

        /// <summary>
        /// 获取人员信息
        /// </summary>
        /// <returns></returns>
        //[OperationContract]
        public List<Personnel> GetPersonList()
        {
            var list = db.Personnels.ToList();
            if (list == null) return null;
            var tagToPersons = db.LocationCardToPersonnels.ToList();
            var postList = db.Posts.ToList();//职位
            var tagList = db.LocationCards.ToList();//关联标签
            var departList = db.Departments.ToList();//部门
            var cardpositionList = db.LocationCardPositions.ToList();//卡位置
            var areaList = db.Areas.ToList();//区域
            var ps = list.ToTModel();
            var ps2 = new List<Personnel>();
            foreach (var p in ps)
            {
                var ttp = tagToPersons.Find(i => i.PersonnelId == p.Id);
                if (ttp != null)
                {
                    p.Tag = tagList.Find(i => i.Id == ttp.LocationCardId).ToTModel();
                    p.TagId = ttp.LocationCardId;
                    var lcp = cardpositionList.Find(i => i.CardId == p.TagId);
                    if (lcp != null && lcp.AreaId != null)
                    {
                        p.AreaId = lcp.AreaId;
                        var area = areaList.Find(i => i.Id == p.AreaId);
                        if (area != null)
                        {
                            p.AreaName = area.Name;
                        }
                    }
                    ps2.Add(p);
                }
                else
                {

                }
                //p.Tag = tagList.Find(i => i.Id == p.TagId).ToTModel();
                p.Parent = departList.Find(i => i.Id == p.ParentId).ToTModel();
            }
            return ps2.ToWCFList();
        }

        public List<Personnel> FindPersonList(string name)
        {
            return db.Personnels.GetListByName(name).ToWcfModelList();
        }

        public Personnel GetPerson(int id)
        {
            return db.Personnels.Find(id).ToTModel();
        }

        public int AddPerson(Personnel p)
        {
            bool r = db.Personnels.Add(p.ToDbModel());
            if (r == false)
            {
                return -1;
            }
            return p.Id;
        }

        public bool EditPerson(Personnel p)
        {
            return db.Personnels.Edit(p.ToDbModel());
        }

        public bool DeletePerson(int id)
        {
            return db.Personnels.DeleteById(id) != null;
        }


        public List<Post> GetPostList()
        {
            var posts = db.Posts.ToList();
            return posts.ToWcfModelList();
        }
        #endregion
    }
}
