using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Engine.Modifiers
{
    public class ModifierEnterNameJoinGroup : ModifierBase
    {
        protected override bool Check(SimleApiRequest request, State state)
        {
            return state.Step == Step.AwaitJoinGroup;
        }

        protected override ApiResponse Respont(SimleApiRequest request, State state)
        {
            using var db = new DatabaseContext();
            var CurrentGroup = db.Groups.Where(x => x.Name == request.Text).FirstOrDefault();
            if (CurrentGroup==null)
            {
                return new ApiResponse
                {
                    Text = "Такой группы не существует\n" +
                    "Повторите попытку...\n\n" +
                    "Для отмены напишите команду \"Отмена\"",
                    UserId = request.PeerId
                };
            }
            state.User.Group = CurrentGroup;
            state.User.IsSignedGroup = true;
            db.Update(state.User);
            db.SaveChanges();
            state.Step = Step.None;
            return new ApiResponse
            {
                Text = $"Вы вступили в группу \"{request.Text}\"",
                UserId = request.PeerId
            };
        }
    }
}
