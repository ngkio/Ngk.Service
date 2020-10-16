using System;

namespace Ngk.DataAccess.DTO.Manager
{
    public class TagModel
    {
        public string Name { get; set; }

        public int Order { get; set; }
    }

    public class TagEditModel : TagModel
    {
        public Guid Id { get; set; }
    }
}
