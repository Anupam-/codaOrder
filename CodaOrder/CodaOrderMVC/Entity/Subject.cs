//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication3.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Subject : BaseEntity
    {
        public Subject()
        {
            this.IconIndex = -1;
        }
    
        public long OID { get; set; }
        public Nullable<int> CID { get; set; }
        public Nullable<long> AccountantID { get; set; }
        public Nullable<long> AddressID { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> DelDate { get; set; }
        public Nullable<long> DirectorID { get; set; }
        public string Email { get; set; }
        public string ExtraCode { get; set; }
        public string Fax { get; set; }
        public string FullName { get; set; }
        public string FullTextSearch { get; set; }
        public System.Guid GUID { get; set; }
        public int IconIndex { get; set; }
        public Nullable<long> IndividualID { get; set; }
        public bool IsCentralized { get; set; }
        public bool IsCustomer { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsImporter { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsSupplier { get; set; }
        public string Kind { get; set; }
        public Nullable<long> LawAddressID { get; set; }
        public string Name { get; set; }
        public Nullable<long> OwnerID { get; set; }
        public string PaperRegistrationCode { get; set; }
        public string PaperVATCode { get; set; }
        public string PaperVATExtraNO { get; set; }
        public Nullable<long> ParentID { get; set; }
        public Nullable<long> PaymentTypeID { get; set; }
        public string Phone { get; set; }
        public int RemindTime { get; set; }
        public string ShortName { get; set; }
        public Nullable<long> SID { get; set; }
        public Nullable<long> SpecificationID { get; set; }
        public Nullable<long> TaxTypeID { get; set; }
        public byte[] TST { get; set; }
        public bool WithoutStamp { get; set; }
    }
}
