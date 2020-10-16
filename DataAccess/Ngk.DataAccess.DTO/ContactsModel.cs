using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ngk.DataAccess.DTO
{
    public class ContactsModel
    {
        public Guid Id { get; set; }

        public string ChainCode { get; set; }

        public string Name { get; set; }

        public string Account { get; set; }

        public string Mobile { get; set; }

        public string Desc { get; set; }
    }

    public class AddContactsRequest
    {
        [Required]
        public string Name { get; set; }
        
        public string Mobile { get; set; }

        public string Desc { get; set; }

        public List<ContactsInfo> ContactAccounts { get; set; }
    }

    public class ContactsInfo
    {
        public string Account { get; set; }

        public string ChainCode { get; set; }
    }

    public class EditContactsRequest : AddContactsRequest
    {
        /// <summary>
        /// 联系人ID
        /// </summary>
        public Guid Id { get; set; }
    }
}
