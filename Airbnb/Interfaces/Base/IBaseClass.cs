using System;
using System.Collections.Generic;
using System.Text;

namespace Airbnb.Interfaces.Base
{/// <summary>
/// Rozhraní zajišťující automatické zvyšovaní ID
/// </summary>
    public interface IBaseClass
    {
        public int Id { get; set; }
    }
}
