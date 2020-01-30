using Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Engine.Modifiers
{
    public class ModifierLeaveGroupAnswerYes : ModifierBaseKeywords
    {
        protected override List<string> Keywords => new List<string>
        {
            "da",
            "да",
            "yes"
        };

        protected override bool CheckState(State state)
        {
            return state.Step == Step.AwaitLeaveGroup;
        }

        protected override ApiResponse Respont(SimleApiRequest request, State state)
        {
            using var db = new DatabaseContext();
            var uid = db.Admins.FirstOrDefault(x => x.UserId == request.FromId 
                                               && x.GroupId == state.User.GroupId);
            if (uid!=null)
            {
                db.Admins.Remove(uid);
            }
            state.User.GroupId = null;
            state.User.Group = null;
            state.User.IsSignedGroup = false;
            
            db.Users.Update(state.User);

            db.SaveChanges();

            state.Step = Step.None;
            return new ApiResponse
            {
                Text = "Все выполнено",
                UserId = request.PeerId
            };
        }
    }
}
