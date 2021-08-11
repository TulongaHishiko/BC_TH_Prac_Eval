using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BC_TH_Prac_Eval.Core.Interfaces
{
    public interface ILinkRepository
    {
        Task<bool> Link(int id,int toLinkitemId);
        Task<bool> Unlink(int id,int linkedItemId);
    }
}
