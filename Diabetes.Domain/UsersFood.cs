using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diabetes.Domain
{
    public class UsersFood:Food
    {
        public Guid UserId { get; set; }
    }
}