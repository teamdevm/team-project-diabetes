using System;

namespace Diabetes.Domain
{
    public class UsersFood:Food
    {
        public Guid UserId { get; set; }
    }
}