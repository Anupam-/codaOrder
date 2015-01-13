﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace WebApplication3.Entity
{
    public class EntityClassSetting
    {
        public string ClassName { get; set; }
        public string Json { get; set; }
        public int Index { get; set; }
        public object Items { get; set; }
    }

    public class DocumentRepository<T, D> : BaseRepository<T, D>, IDocuments<T, D>
        where T : BaseEntity
        where D : codaJournal
    {
        public DocumentRepository() : base() { }
        public DocumentRepository(D dbContext) : base(dbContext) { }

        public string GetLinesJson()
        {
            string json = "";
            long OID = 8000010580984;
            string filter = "<Filter><ID>8000005797829</ID><ID>8000000241646</ID></Filter>";
            string docOID = "";
            long? objectID = null;
            DateTime? dateBegin = DateTime.Now.AddDays(-7);
            DateTime? dateEnd = DateTime.Now;
            dateBegin = new DateTime(2013, 5, 16);
            dateEnd = new DateTime(2013, 5, 23);
            string docFilterClasses = "<DocumentClasses><ClassName>DocSale</ClassName></DocumentClasses>";
            string statusFilter = "<Status/>";
            bool isExtended = true;
            bool showDeleted = false;
            bool? checkOperation = null;
            string securityUser = @"TRITON\kobernik.u";
            string securityGroup = "<Groups><ID>20</ID><ID>11</ID><ID>92</ID><ID>48</ID><ID>44</ID></Groups>";
            int perPage = 50;
            int pageNumber = 1;
            string fullTextFilter = "<Root/>";
            string orderFilter = "<Root/>";
            ObjectParameter totalRows = new ObjectParameter("TotalRows", typeof(int));
            ObjectParameter pages = new ObjectParameter("Pages", typeof(int));
            string whereQuery = "";
            string WhereQueryTableAlias = "_journalalias_";
            ObjectParameter tST = new ObjectParameter("TST", typeof(byte[]));

            var qDocs = dbContext.GetDocuments(OID, filter, docOID, objectID, dateBegin, dateEnd, docFilterClasses, statusFilter, isExtended, showDeleted,
                checkOperation, securityUser, securityGroup, perPage, pageNumber, fullTextFilter, orderFilter, totalRows, pages, whereQuery,
                WhereQueryTableAlias, tST);

            int index = 0;
            List<EntityClassSetting> items = new List<EntityClassSetting>();

            EntityClassSetting item = new EntityClassSetting() { ClassName = "JournalSale_Documents", Index = ++index, Items = qDocs.ToList() };
            items.Add(item);

            var qFirm = qDocs.GetNextResult<Firm>();
            item = new EntityClassSetting() { ClassName = "Firm", Index = ++index, Items = qFirm.ToList() };
            items.Add(item);

            var qFilial = qFirm.GetNextResult<Filial>();
            item = new EntityClassSetting() { ClassName = "Filial", Index = ++index, Items = qFilial.ToList() };
            items.Add(item);

            var qClassification = qFilial.GetNextResult<Classification>();
            item = new EntityClassSetting() { ClassName = "Classification", Index = ++index, Items = qClassification.ToList() };
            items.Add(item);

            var qEmployee = qClassification.GetNextResult<Employee>();
            item = new EntityClassSetting() { ClassName = "Employee", Index = ++index, Items = qEmployee.ToList() };
            items.Add(item);

            var qDirectory = qEmployee.GetNextResult<Directory>();
            item = new EntityClassSetting() { ClassName = "Directory", Index = ++index, Items = qDirectory.ToList() };
            items.Add(item);

            var qSubject = qDirectory.GetNextResult<Subject>();
            item = new EntityClassSetting() { ClassName = "Subject", Index = ++index, Items = qSubject.ToList() };
            items.Add(item);

            var qRevenue = qSubject.GetNextResult<Revenue>();
            item = new EntityClassSetting() { ClassName = "Revenue", Index = ++index, Items = qRevenue.ToList() };
            items.Add(item);

            var qContract = qRevenue.GetNextResult<Contract>();
            item = new EntityClassSetting() { ClassName = "Contract", Index = ++index, Items = qContract.ToList() };
            items.Add(item);

            var qDepartment = qContract.GetNextResult<Department>();
            item = new EntityClassSetting() { ClassName = "Department", Index = ++index, Items = qDepartment.ToList() };
            items.Add(item);

            var qAddress = qDepartment.GetNextResult<Address>();
            item = new EntityClassSetting() { ClassName = "Address", Index = ++index, Items = qAddress.ToList() };
            items.Add(item);

            var qTaxType = qAddress.GetNextResult<TaxType>();
            item = new EntityClassSetting() { ClassName = "TaxType", Index = ++index, Items = qTaxType.ToList() };
            items.Add(item);

            var qBusinessUnit = qTaxType.GetNextResult<BusinessUnit>();
            item = new EntityClassSetting() { ClassName = "BusinessUnit", Index = ++index, Items = qBusinessUnit.ToList() };
            items.Add(item);

            var qDocBoxLine = qBusinessUnit.GetNextResult<DocBoxLine>();
            item = new EntityClassSetting() { ClassName = "DocBoxLine", Index = ++index, Items = qDocBoxLine.ToList() };
            items.Add(item);

            var qBox = qDocBoxLine.GetNextResult<Box>();
            item = new EntityClassSetting() { ClassName = "Box", Index = ++index, Items = qBox.ToList() };
            items.Add(item);

            var qAction = qBox.GetNextResult<WebApplication3.Entity.Action>();
            item = new EntityClassSetting() { ClassName = "Action", Index = ++index, Items = qAction.ToList() };
            items.Add(item);

            foreach (EntityClassSetting cls in items.Select(i => i).OrderBy(i => i.Index))
                cls.Json = JsonConvert.SerializeObject(cls.Items);

            Dictionary<string, object> resultJson = new Dictionary<string, object>() {
                    {"Documents", items.Where( i => i.Index == 1).Select( i => i.Json).First()},
                    {"PageNumber", pageNumber},
                    {"PageCount", pages.Value},
                    {"Rows", totalRows.Value},
                    {"PerPage", perPage},
                };

            json = JsonConvert.SerializeObject(resultJson);

            return json;
        }
    }
}