﻿/*
' Copyright (c) 2013  Christoc.com Software Solutions
'  All rights reserved.
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
' and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
' 
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT 
' SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN 
' ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE 
' OR OTHER DEALINGS IN THE SOFTWARE.
' 
*/

namespace Christoc.Modules.DnnChat.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using DotNetNuke.Data;

    /*
     * This class provides the DAL2 access to the storing of Messages within the DnnChat module
     */
    public class RoomController
    {
        public void CreateRoom(Room r)
        {
            using (var ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Room>();
                rep.Insert(r);
            }
        }

        public void DeleteRoom(Guid roomId, int moduleId)
        {
            var t = this.GetRoom(roomId, moduleId);
            this.DeleteRoom(t);
        }

        public void DeleteRoom(Room r)
        {
            using (var ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Room>();
                r.Enabled=false;
                rep.Update(r);
            }
        }

        public IEnumerable<Room> GetRooms(int moduleId)
        {
            IEnumerable<Room> t;
            using (var ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Room>();
                t = rep.Get(moduleId).OrderByDescending(x => x.RoomName);
            }

            return t;
        }

        public Room GetRoom(Guid roomId, int moduleId)
        {
            Room t;
            using (var ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Room>();
                t = rep.GetById(roomId, moduleId);
            }

            return t;
        }

        public void UpdateRoom(Room r)
        {
            using (var ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Room>();
                rep.Update(r);
            }
        }

        public void JoinRoom(Room r, ConnectionRecord cr)
        {
            //TODO: implement Join Room
            //todo: write to the ConnectionRecordRooms table
        }

        public void DepartRoom(Room r, ConnectionRecord cr)
        {
            //TODO: implement Depart Room
            //todo: write to the ConnectionRecordRooms table
        }

        public bool UserInRoom(Guid roomId, ConnectionRecord cr)
        {
            //TODO: check if the user is in that Room
            // should ensure that they are still IN the room
            return true;
        }

        public void GetUserRooms(int UserId)
        {
            //based on the UserId lookup all the previous rooms for the user
            
            //return a list of Room objects

            var messages = (from a in this.GetMessages(moduleId) where a.MessageDate.Subtract(DateTime.UtcNow).TotalHours <= hoursBackInTime select a).Take(maxRecords).Reverse();

            return messages.Any() ? messages : null;

        }
    }
}