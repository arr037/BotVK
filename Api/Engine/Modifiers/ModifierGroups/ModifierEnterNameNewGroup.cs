using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Engine.Modifiers
{
    public class ModifierEnterNameNewGroup : ModifierBase
    {
        protected override bool Check(SimleApiRequest request, State state)
        {
            return state.Step == Step.CreateNewGroup;
        }

        protected override ApiResponse Respont(SimleApiRequest request, State state)
        {
            using var db = new DatabaseContext();

            if (!db.Groups.Any(u => u.Name == request.Text))
            {
                var admin = new Admin{UserId = request.FromId};
                var group = new Group
                {
                    Admins = new List<Admin>{admin},
                    Name=request.Text,
                    
                };

                db.Groups.Add(group);     
                db.SaveChanges();

                state.User.Group = group;
                state.User.IsSignedGroup = true;
                db.Update(state.User);
                db.SaveChanges();

                state.Step = Step.None;
                return new ApiResponse
                {
                    Text = $"Группа \"{request.Text}\" успешно создана",
                    UserId = request.PeerId
                };
            }

            return new ApiResponse
            {
                Text = $"Группа \"{request.Text}\" уже существует\n" +
                       "Введите другое название группы",
                UserId = request.PeerId
            };
        }
    }
}
