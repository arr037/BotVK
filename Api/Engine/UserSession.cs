using Api.Engine.Modifiers;
using Api.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Engine
{
    public class UserSession
    {
        private static readonly List<ModifierBase> Modifiers = new List<ModifierBase>
        {
            // Модификатор для отмены
            new ModifierCancel(),
            // Модификатор для создания группы
            new ModifierCreateGroup(),
            new ModifierEnterNameNewGroup(),

            new ModifierJoinGroup(),
            new ModifierEnterNameJoinGroup(),

            new ModifierAddAdminInGroup(),
            new ModifierUserList(),
            new ModifierEnterIDAdmin(),

            new ModifierHelp(),
            new ModifierUnknown()
        };

        private readonly State _state = new State();

        public UserSession(long userId)
        {
            using var db = new DatabaseContext();
            var user = db.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                user = new User
                {
                    Id = userId
                };
                db.Users.Add(user);
                db.SaveChanges();
            }           
            
            if (string.IsNullOrEmpty(user.Group.Name))
            {
                var CurentGroup = db.Users.Where(x => x.Id == userId).Select(x => x.Group).FirstOrDefault();

                if (CurentGroup.Admins == null)
                {
                    var admin = db.Users.Where(x => x.Group == CurentGroup).Select(x => x.Group.Admins).FirstOrDefault();
                    CurentGroup.Admins = admin;
                }

                user.Group = CurentGroup;
            }
            _state.User = user;
        }

        internal ApiResponse HandleRequest(SimleApiRequest apiRequest)
        {
            ApiResponse response = null;

            if(!Modifiers.Any(modifier=>modifier.Run(apiRequest,_state,out response)))
            {
                throw new NotSupportedException("No default modifier");
            }
            return response;
        }
    }
}
