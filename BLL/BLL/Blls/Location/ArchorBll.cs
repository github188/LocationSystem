﻿using System.Collections.Generic;
using System.Linq;
using DAL;
using DbModel.Location.AreaAndDev;

namespace BLL.Blls.Location
{
    public class ArchorBll : BaseBll<Archor, LocationDb>
    {
        public ArchorBll():base()
        {

        }
        public ArchorBll(LocationDb db) : base(db)
        {

        }

        protected override void InitDbSet()
        {
            DbSet = Db.Archors;
        }

        public Archor FindByCode(string code)
        {
            return DbSet.FirstOrDefault(i => i.Code == code);
        }

        public List<Archor> FindByCodes(List<string> codes)
        {
            return DbSet.Where(i => codes.Contains(i.Code)).ToList();
        }
    }
}