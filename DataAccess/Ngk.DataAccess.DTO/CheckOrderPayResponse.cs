using System;

namespace Ngk.DataAccess.DTO
{
    public class CheckOrderPayResponse
    {
        public Guid OrderId { get; set; }

        public int BusinessState { get; set; }
    }
}
