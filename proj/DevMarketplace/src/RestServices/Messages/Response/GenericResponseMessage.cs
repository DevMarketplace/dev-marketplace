#region License
// The Developer Marketplace is a web application that allows individuals, 
// teams and companies share KanBan stories, cards, tasks and items from 
// their KanBan boards and Scrum boards. 
// All shared stories become available on the Developer Marketplace website
//  and software engineers from all over the world can work on these stories. 
// 
// Copyright (C) 2016 Tosho Toshev
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 
// GitHub repository: https://github.com/cracker4o/dev-marketplace
// e-mail: cracker4o@gmail.com
#endregion
using System.Collections.Generic;
using System.Net;

namespace RestServices.Messages.Response
{
    /// <summary>
    /// A generic response message class used to serve as a common response message for API calls.
    /// </summary>
    /// <typeparam name="TBusinessObject"></typeparam>
    public class GenericResponseMessage<TBusinessObject> where TBusinessObject : class
    {
        public GenericResponseMessage()
        {   
        }

        /// <summary>
        /// Creates a new instance of the GenericResponseMessage with a data parameter
        /// </summary>
        /// <param name="data">A business object that contains the results</param>
        public GenericResponseMessage(TBusinessObject data)
        {
            Data = data;
        }

        /// <summary>
        /// A HTTP status code of the operation
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }


        /// <summary>
        /// The data object that is returned to the client
        /// </summary>
        public TBusinessObject Data { get; set; }

        /// <summary>
        /// A list of error messages
        /// </summary>
        public IEnumerable<string> Errors { get; set; }
    }
}
