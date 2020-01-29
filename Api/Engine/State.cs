using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Engine
{
    public class State
    {
        public User User { get; set; }
        public Step Step { get; set; }
    }

    public enum Step
    {
        None,
        CreateNewGroup,
        AwaitEnterGroupName,
        AwaitJoinGroup,
        AwaitAddNewAdmin
    }
}
